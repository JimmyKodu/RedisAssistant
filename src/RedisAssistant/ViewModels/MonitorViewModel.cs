using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedisAssistant.Models;
using RedisAssistant.Services;

namespace RedisAssistant.ViewModels;

public partial class MonitorViewModel : ObservableObject
{
    private readonly IRedisService _redisService;

    [ObservableProperty]
    private RedisServerInfo? _serverInfo;

    [ObservableProperty]
    private string _version = "N/A";

    [ObservableProperty]
    private string _usedMemory = "N/A";

    [ObservableProperty]
    private string _totalKeys = "N/A";

    [ObservableProperty]
    private string _connectedClients = "N/A";

    [ObservableProperty]
    private string _totalCommands = "N/A";

    [ObservableProperty]
    private string _uptime = "N/A";

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isMonitoring = false;

    private CancellationTokenSource? _monitoringCts;

    public MonitorViewModel(IRedisService redisService)
    {
        _redisService = redisService;
    }

    [RelayCommand]
    private async Task LoadServerInfoAsync()
    {
        if (!_redisService.IsConnected)
        {
            StatusMessage = "Not connected to Redis";
            return;
        }

        StatusMessage = "Loading server info...";
        ServerInfo = await _redisService.GetServerInfoAsync().ConfigureAwait(false);
        UpdateDisplayValues();
        StatusMessage = "Server info loaded";
    }

    [RelayCommand]
    private async Task StartMonitoringAsync()
    {
        if (IsMonitoring)
            return;

        IsMonitoring = true;
        _monitoringCts = new CancellationTokenSource();

        try
        {
            while (!_monitoringCts.Token.IsCancellationRequested)
            {
                await LoadServerInfoAsync().ConfigureAwait(false);
                await Task.Delay(2000, _monitoringCts.Token).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when monitoring is stopped
        }
        catch (Exception)
        {
            // Log or handle other exceptions
            IsMonitoring = false;
            StatusMessage = "Monitoring stopped due to error";
        }
    }

    [RelayCommand]
    private void StopMonitoring()
    {
        _monitoringCts?.Cancel();
        _monitoringCts?.Dispose();
        _monitoringCts = null;
        IsMonitoring = false;
        StatusMessage = "Monitoring stopped";
    }

    private void UpdateDisplayValues()
    {
        if (ServerInfo == null)
            return;

        Version = ServerInfo.Version;
        UsedMemory = FormatBytes(ServerInfo.UsedMemory);
        TotalKeys = ServerInfo.TotalKeys.ToString("N0");
        ConnectedClients = ServerInfo.ConnectedClients.ToString();
        TotalCommands = ServerInfo.TotalCommandsProcessed.ToString("N0");
        Uptime = FormatUptime(ServerInfo.Uptime);
    }

    private string FormatBytes(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    private string FormatUptime(double seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{timeSpan.Days}d {timeSpan.Hours}h {timeSpan.Minutes}m";
    }
}
