using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedisAssistant.Models;
using RedisAssistant.Services;
using System.Collections.ObjectModel;

namespace RedisAssistant.ViewModels;

public partial class KeysViewModel : ObservableObject
{
    private readonly IRedisService _redisService;

    [ObservableProperty]
    private ObservableCollection<RedisKey> _keys = new();

    [ObservableProperty]
    private RedisKey? _selectedKey;

    [ObservableProperty]
    private string _searchPattern = "*";

    [ObservableProperty]
    private string? _keyValue;

    [ObservableProperty]
    private string _newKeyName = string.Empty;

    [ObservableProperty]
    private string _newKeyValue = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public KeysViewModel(IRedisService redisService)
    {
        _redisService = redisService;
    }

    [RelayCommand]
    private async Task LoadKeysAsync()
    {
        if (!_redisService.IsConnected)
        {
            StatusMessage = "Not connected to Redis";
            return;
        }

        StatusMessage = "Loading keys...";
        var keys = await _redisService.GetKeysAsync(SearchPattern);
        Keys.Clear();
        foreach (var key in keys)
        {
            Keys.Add(key);
        }
        StatusMessage = $"Loaded {keys.Count} keys";
    }

    [RelayCommand]
    private async Task ViewKeyAsync(RedisKey? key)
    {
        if (key == null)
            return;

        StatusMessage = "Loading key value...";
        var value = await _redisService.GetValueAsync(key.Name);
        KeyValue = value;
        SelectedKey = key;
        StatusMessage = "Key loaded";
    }

    [RelayCommand]
    private async Task AddKeyAsync()
    {
        if (string.IsNullOrWhiteSpace(NewKeyName))
        {
            StatusMessage = "Please enter a key name";
            return;
        }

        StatusMessage = "Adding key...";
        var success = await _redisService.SetValueAsync(NewKeyName, NewKeyValue);

        if (success)
        {
            StatusMessage = "Key added successfully";
            NewKeyName = string.Empty;
            NewKeyValue = string.Empty;
            await LoadKeysAsync();
        }
        else
        {
            StatusMessage = "Failed to add key";
        }
    }

    [RelayCommand]
    private async Task DeleteKeyAsync(RedisKey? key)
    {
        if (key == null)
            return;

        StatusMessage = "Deleting key...";
        var success = await _redisService.DeleteKeyAsync(key.Name);

        if (success)
        {
            Keys.Remove(key);
            StatusMessage = "Key deleted successfully";
            if (SelectedKey == key)
            {
                SelectedKey = null;
                KeyValue = null;
            }
        }
        else
        {
            StatusMessage = "Failed to delete key";
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadKeysAsync();
    }
}
