# RedisAssistant Architecture

## Overview

RedisAssistant is a cross-platform desktop application built using .NET MAUI (Multi-platform App UI) that provides visual management and monitoring capabilities for Redis servers.

## Technology Stack

### Core Framework
- **.NET 9.0**: Latest version of the .NET platform
- **.NET MAUI**: Cross-platform UI framework for building native apps

### Libraries
- **StackExchange.Redis** (v2.8.16): High-performance Redis client
- **CommunityToolkit.Mvvm** (v8.3.2): MVVM helpers and utilities
- **Microsoft.Extensions.Logging.Debug** (v9.0.0): Logging infrastructure

## Architecture Pattern

The application follows the **MVVM (Model-View-ViewModel)** architectural pattern, which provides:
- Clear separation of concerns
- Testability
- Maintainability
- Data binding support

### Architecture Layers

```
┌─────────────────────────────────────────┐
│              Views (XAML)                │
│  - ConnectionsPage                       │
│  - KeysPage                              │
│  - MonitorPage                           │
└────────────┬────────────────────────────┘
             │ Data Binding
             ▼
┌─────────────────────────────────────────┐
│           ViewModels                     │
│  - ConnectionsViewModel                  │
│  - KeysViewModel                         │
│  - MonitorViewModel                      │
└────────────┬────────────────────────────┘
             │ Service Calls
             ▼
┌─────────────────────────────────────────┐
│            Services                      │
│  - IRedisService / RedisService          │
└────────────┬────────────────────────────┘
             │ Data Access
             ▼
┌─────────────────────────────────────────┐
│        External Systems                  │
│  - Redis Server(s)                       │
└─────────────────────────────────────────┘
```

## Component Details

### Models

Located in `Models/` directory, these represent the data structures:

- **RedisConnection**: Connection configuration (host, port, password, SSL, etc.)
- **RedisKey**: Redis key information (name, type, size, TTL)
- **RedisServerInfo**: Server statistics (version, memory, clients, etc.)

### ViewModels

Located in `ViewModels/` directory, handle business logic and state:

- **ConnectionsViewModel**: Manages Redis connections
  - Add/delete connections
  - Connect/disconnect operations
  - Connection state tracking

- **KeysViewModel**: Handles key operations
  - Search and list keys
  - View key values
  - Add/delete keys
  - Pattern-based filtering

- **MonitorViewModel**: Server monitoring
  - Fetch server information
  - Real-time monitoring (2-second intervals)
  - Statistics formatting

All ViewModels inherit from `ObservableObject` and use:
- `[ObservableProperty]` for automatic property change notification
- `[RelayCommand]` for command implementations

### Views

Located in `Views/` directory, define the UI in XAML:

- **ConnectionsPage.xaml**: Connection management UI
  - Connection form
  - Saved connections list
  - Status display

- **KeysPage.xaml**: Key management UI
  - Search interface
  - Key list
  - Key details viewer
  - Add/delete operations

- **MonitorPage.xaml**: Monitoring dashboard
  - Server statistics grid
  - Real-time updates
  - Formatted metrics display

### Services

Located in `Services/` directory, implement business logic:

- **IRedisService**: Service interface defining Redis operations
- **RedisService**: Implementation using StackExchange.Redis
  - Connection management with `ConnectionMultiplexer`
  - Key operations (get, set, delete, search)
  - Server info retrieval
  - Error handling

### Dependency Injection

The application uses Microsoft's built-in DI container configured in `MauiProgram.cs`:

```csharp
// Services - Singleton
builder.Services.AddSingleton<IRedisService, RedisService>();

// ViewModels - Singleton
builder.Services.AddSingleton<ConnectionsViewModel>();
builder.Services.AddSingleton<KeysViewModel>();
builder.Services.AddSingleton<MonitorViewModel>();

// Views - Singleton
builder.Services.AddSingleton<ConnectionsPage>();
builder.Services.AddSingleton<KeysPage>();
builder.Services.AddSingleton<MonitorPage>();
```

## Data Flow

### Connection Flow
```
User Input → ConnectionsViewModel → RedisService → Redis Server
                    ↓
              Status Update
                    ↓
            UI Update (Binding)
```

### Key Operations Flow
```
User Action → KeysViewModel → RedisService → Redis Server
                   ↓
          Result Processing
                   ↓
   ObservableCollection Update
                   ↓
           UI Auto-Updates
```

### Monitoring Flow
```
Start Monitor → MonitorViewModel → Timer (2s)
                       ↓
              RedisService.GetServerInfoAsync()
                       ↓
                 Update Properties
                       ↓
              UI Auto-Updates (Binding)
```

## Platform-Specific Code

Located in `Platforms/` directory:

- **Windows**: WinUI 3 entry point and configuration
- **Android**: Android activity and application setup
- **iOS**: iOS app delegate and program entry
- **MacCatalyst**: Mac Catalyst app delegate and program entry

Each platform has its own:
- Application entry point
- Native UI initialization
- Platform-specific configuration

## Resources

### Styles (`Resources/Styles/`)
- **Colors.xaml**: Color palette and brushes
- **Styles.xaml**: Control styles, theme configurations, value converters

### Assets
- **AppIcon/**: Application icon (SVG format)
- **Splash/**: Splash screen (SVG format)
- **Images/**: App images
- **Fonts/**: OpenSans fonts

## Navigation

The app uses Shell-based navigation (`AppShell.xaml`):

```xml
<ShellContent Title="Connections" Route="ConnectionsPage" />
<ShellContent Title="Keys" Route="KeysPage" />
<ShellContent Title="Monitor" Route="MonitorPage" />
```

Features:
- Flyout menu navigation
- Route-based navigation
- Deep linking support

## Error Handling

The application handles errors at multiple levels:

1. **Service Level**: Try-catch blocks in RedisService methods
2. **ViewModel Level**: User-friendly status messages
3. **UI Level**: Disabled states for unavailable features

## Performance Considerations

- **Async Operations**: All I/O operations are asynchronous
- **Connection Pooling**: Single `ConnectionMultiplexer` instance per connection
- **Lazy Loading**: Keys loaded on-demand with pattern filtering
- **Efficient Updates**: Data binding minimizes manual UI updates

## Security

- **SSL/TLS Support**: Encrypted connections to Redis
- **Password Protection**: Secure authentication
- **No Credential Storage**: Connections stored in-memory (future: secure storage)

## Future Enhancements

Potential areas for expansion:

1. **Persistence**: Save connections to secure storage
2. **Advanced Key Types**: Support for Lists, Sets, Sorted Sets, Hashes
3. **Pub/Sub**: Real-time message monitoring
4. **CLI Support**: Command-line interface
5. **Export/Import**: Backup and restore functionality
6. **Multi-Database**: Switch between Redis databases
7. **Connection Clustering**: Support for Redis Cluster
8. **Performance Metrics**: Advanced monitoring with charts

## Testing Strategy

Currently manual testing. Recommended test structure:

```
Tests/
├── Unit/
│   ├── ViewModels/
│   ├── Services/
│   └── Models/
├── Integration/
│   └── Services/
└── UI/
    └── E2E/
```

## Build Targets

The application targets multiple platforms:

- **Windows**: `net9.0-windows10.0.19041.0`
- **Android**: `net9.0-android` (API 21+)
- **iOS**: `net9.0-ios` (iOS 11+)
- **macOS**: `net9.0-maccatalyst` (macOS 11+)

## Deployment

Each platform has specific deployment requirements:

- **Windows**: MSIX installer
- **Android**: APK or AAB (Google Play)
- **iOS**: IPA (App Store)
- **macOS**: DMG or PKG

See [BUILDING.md](BUILDING.md) for detailed instructions.

## References

- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [StackExchange.Redis Documentation](https://stackexchange.github.io/StackExchange.Redis/)
- [MVVM Toolkit Documentation](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)
