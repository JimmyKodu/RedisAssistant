using RedisAssistant.Models;
using StackExchange.Redis;

namespace RedisAssistant.Services;

public interface IRedisService
{
    Task<bool> ConnectAsync(RedisConnection connection);
    Task DisconnectAsync();
    bool IsConnected { get; }
    Task<List<RedisKey>> GetKeysAsync(string pattern = "*");
    Task<string?> GetValueAsync(string key);
    Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null);
    Task<bool> DeleteKeyAsync(string key);
    Task<RedisServerInfo> GetServerInfoAsync();
    RedisConnection? CurrentConnection { get; }
}

public class RedisService : IRedisService, IDisposable
{
    private ConnectionMultiplexer? _connection;
    private IDatabase? _database;
    private RedisConnection? _currentConnection;
    private bool _disposed;

    public bool IsConnected => _connection?.IsConnected ?? false;
    public RedisConnection? CurrentConnection => _currentConnection;

    public async Task<bool> ConnectAsync(RedisConnection connection)
    {
        try
        {
            if (_connection != null)
            {
                await DisconnectAsync().ConfigureAwait(false);
            }

            var configOptions = ConfigurationOptions.Parse(connection.ConnectionString);
            configOptions.AbortOnConnectFail = false;
            configOptions.SyncTimeout = 5000;
            configOptions.AsyncTimeout = 5000;
            configOptions.ConnectTimeout = 5000;

            _connection = await ConnectionMultiplexer.ConnectAsync(configOptions).ConfigureAwait(false);
            _database = _connection.GetDatabase(connection.Database);
            _currentConnection = connection;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task DisconnectAsync()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync().ConfigureAwait(false);
            _connection.Dispose();
            _connection = null;
            _database = null;
            _currentConnection = null;
        }
    }

    public async Task<List<RedisKey>> GetKeysAsync(string pattern = "*")
    {
        if (_database == null || _connection == null)
            return new List<RedisKey>();

        try
        {
            var keys = new List<RedisKey>();
            var server = _connection.GetServer(_connection.GetEndPoints().First());

            var asyncEnumerable = server.KeysAsync(pattern: pattern);
            await foreach (var key in asyncEnumerable)
            {
                var type = await _database.KeyTypeAsync(key).ConfigureAwait(false);
                var ttl = await _database.KeyTimeToLiveAsync(key).ConfigureAwait(false);
                
                long size = 0;
                try
                {
                    // StringLengthAsync only works for string types
                    if (type == RedisType.String)
                    {
                        size = await _database.StringLengthAsync(key).ConfigureAwait(false);
                    }
                }
                catch
                {
                    // Ignore size calculation errors for non-string types
                    size = 0;
                }

                keys.Add(new RedisKey
                {
                    Name = key.ToString(),
                    Type = type.ToString().ToLower(),
                    Size = size,
                    Ttl = ttl
                });
            }

            return keys;
        }
        catch (Exception)
        {
            return new List<RedisKey>();
        }
    }

    public async Task<string?> GetValueAsync(string key)
    {
        if (_database == null)
            return null;

        try
        {
            var value = await _database.StringGetAsync(key).ConfigureAwait(false);
            return value.ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null)
    {
        if (_database == null)
            return false;

        try
        {
            return await _database.StringSetAsync(key, value, expiry).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteKeyAsync(string key)
    {
        if (_database == null)
            return false;

        try
        {
            return await _database.KeyDeleteAsync(key).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<RedisServerInfo> GetServerInfoAsync()
    {
        if (_connection == null)
            return new RedisServerInfo();

        try
        {
            var server = _connection.GetServer(_connection.GetEndPoints().First());
            var info = await server.InfoAsync().ConfigureAwait(false);

            var serverInfo = new RedisServerInfo();

            foreach (var section in info)
            {
                foreach (var kvp in section)
                {
                    switch (kvp.Key.ToLower())
                    {
                        case "redis_version":
                            serverInfo.Version = kvp.Value;
                            break;
                        case "used_memory":
                            long.TryParse(kvp.Value, out var mem);
                            serverInfo.UsedMemory = mem;
                            break;
                        case "connected_clients":
                            int.TryParse(kvp.Value, out var clients);
                            serverInfo.ConnectedClients = clients;
                            break;
                        case "total_commands_processed":
                            long.TryParse(kvp.Value, out var commands);
                            serverInfo.TotalCommandsProcessed = commands;
                            break;
                        case "uptime_in_seconds":
                            double.TryParse(kvp.Value, out var uptime);
                            serverInfo.Uptime = uptime;
                            break;
                    }
                }
            }

            // Get total keys using DatabaseSize for better performance
            try
            {
                serverInfo.TotalKeys = await server.DatabaseSizeAsync(_currentConnection?.Database ?? 0).ConfigureAwait(false);
            }
            catch
            {
                // Fallback to 0 if DatabaseSize is not available
                serverInfo.TotalKeys = 0;
            }

            return serverInfo;
        }
        catch (Exception)
        {
            return new RedisServerInfo();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    try
                    {
                        _connection.Close();
                        _connection.Dispose();
                    }
                    catch
                    {
                        // Ignore exceptions during disposal
                    }
                    _connection = null;
                    _database = null;
                    _currentConnection = null;
                }
            }
            _disposed = true;
        }
    }
}
