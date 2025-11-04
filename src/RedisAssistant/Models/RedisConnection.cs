namespace RedisAssistant.Models;

public class RedisConnection
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 6379;
    public string? Password { get; set; }
    public int Database { get; set; } = 0;
    public bool UseSsl { get; set; } = false;

    public string ConnectionString
    {
        get
        {
            var parts = new List<string>
            {
                $"{Host}:{Port}"
            };

            if (!string.IsNullOrEmpty(Password))
            {
                parts.Add($"password={Password}");
            }

            if (UseSsl)
            {
                parts.Add("ssl=true");
            }

            parts.Add($"defaultDatabase={Database}");

            return string.Join(",", parts);
        }
    }
}
