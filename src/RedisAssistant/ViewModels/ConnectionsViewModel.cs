using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedisAssistant.Models;
using RedisAssistant.Services;
using System.Collections.ObjectModel;

namespace RedisAssistant.ViewModels;

public partial class ConnectionsViewModel : ObservableObject
{
    private readonly IRedisService _redisService;

    [ObservableProperty]
    private ObservableCollection<RedisConnection> _connections = new();

    [ObservableProperty]
    private RedisConnection? _selectedConnection;

    [ObservableProperty]
    private string _connectionName = string.Empty;

    [ObservableProperty]
    private string _host = "localhost";

    [ObservableProperty]
    private int _port = 6379;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private int _database = 0;

    [ObservableProperty]
    private bool _useSsl = false;

    [ObservableProperty]
    private bool _isConnected = false;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public ConnectionsViewModel(IRedisService redisService)
    {
        _redisService = redisService;
        LoadSavedConnections();
    }

    [RelayCommand]
    private async Task AddConnectionAsync()
    {
        var connection = new RedisConnection
        {
            Name = ConnectionName,
            Host = Host,
            Port = Port,
            Password = Password,
            Database = Database,
            UseSsl = UseSsl
        };

        Connections.Add(connection);
        SaveConnections();
        ClearForm();
        StatusMessage = "Connection added successfully";
    }

    [RelayCommand]
    private async Task ConnectAsync(RedisConnection? connection)
    {
        if (connection == null)
            return;

        StatusMessage = "Connecting...";
        var success = await _redisService.ConnectAsync(connection);

        if (success)
        {
            IsConnected = true;
            StatusMessage = $"Connected to {connection.Name}";
            SelectedConnection = connection;
        }
        else
        {
            IsConnected = false;
            StatusMessage = "Failed to connect";
        }
    }

    [RelayCommand]
    private async Task DisconnectAsync()
    {
        await _redisService.DisconnectAsync();
        IsConnected = false;
        StatusMessage = "Disconnected";
        SelectedConnection = null;
    }

    [RelayCommand]
    private void DeleteConnection(RedisConnection? connection)
    {
        if (connection != null)
        {
            Connections.Remove(connection);
            SaveConnections();
            StatusMessage = "Connection deleted";
        }
    }

    private void ClearForm()
    {
        ConnectionName = string.Empty;
        Host = "localhost";
        Port = 6379;
        Password = null;
        Database = 0;
        UseSsl = false;
    }

    private void LoadSavedConnections()
    {
        // In a real app, load from preferences/storage
        // For now, add a default connection
        Connections.Add(new RedisConnection
        {
            Name = "Local Redis",
            Host = "localhost",
            Port = 6379
        });
    }

    private void SaveConnections()
    {
        // In a real app, save to preferences/storage
    }
}
