# RedisAssistant

A cross-platform desktop Redis GUI application for visual management and monitoring, built with .NET MAUI.

## Features

- 🔌 **Connection Management**: Connect to multiple Redis instances with support for SSL/TLS
- 🔑 **Key Management**: View, search, add, and delete Redis keys with ease
- 📊 **Real-time Monitoring**: Monitor server statistics including memory usage, connected clients, and more
- 🖥️ **Cross-Platform**: Runs on Windows, macOS, iOS, and Android
- 🎨 **Modern UI**: Clean and intuitive interface built with .NET MAUI

## Technology Stack

- **.NET 9.0**: Latest .NET framework
- **.NET MAUI**: Multi-platform App UI for cross-platform development
- **StackExchange.Redis**: Industry-standard Redis client library
- **CommunityToolkit.Mvvm**: MVVM helpers and utilities
- **XAML**: Declarative UI markup

## Prerequisites

To build and run this application, you need:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- .NET MAUI workload installed:
  ```bash
  dotnet workload install maui
  ```

### Platform-Specific Requirements

#### Windows
- Windows 10 version 1809 or higher
- Visual Studio 2022 17.8 or later with MAUI workload

#### macOS
- macOS 11 or higher
- Xcode 14 or later
- Visual Studio 2022 for Mac or Visual Studio Code

#### Android
- Android 5.0 (API 21) or higher

#### iOS
- iOS 11 or higher

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/JimmyKodu/RedisAssistant.git
cd RedisAssistant
```

### Restore Dependencies

```bash
dotnet restore
```

### Build the Application

```bash
dotnet build src/RedisAssistant/RedisAssistant.csproj
```

### Run the Application

#### Windows
```bash
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-windows10.0.19041.0 -t:Run
```

#### macOS (Mac Catalyst)
```bash
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-maccatalyst -t:Run
```

#### Android
```bash
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-android -t:Run
```

#### iOS
```bash
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-ios -t:Run
```

## Project Structure

```
RedisAssistant/
├── src/
│   └── RedisAssistant/
│       ├── Models/              # Data models
│       │   ├── RedisConnection.cs
│       │   ├── RedisKey.cs
│       │   └── RedisServerInfo.cs
│       ├── ViewModels/          # MVVM ViewModels
│       │   ├── ConnectionsViewModel.cs
│       │   ├── KeysViewModel.cs
│       │   └── MonitorViewModel.cs
│       ├── Views/               # XAML UI pages
│       │   ├── ConnectionsPage.xaml
│       │   ├── KeysPage.xaml
│       │   └── MonitorPage.xaml
│       ├── Services/            # Business logic
│       │   └── RedisService.cs
│       ├── Resources/           # Images, fonts, styles
│       │   ├── AppIcon/
│       │   ├── Splash/
│       │   ├── Fonts/
│       │   └── Styles/
│       ├── Platforms/           # Platform-specific code
│       │   ├── Android/
│       │   ├── iOS/
│       │   ├── MacCatalyst/
│       │   └── Windows/
│       ├── App.xaml
│       ├── AppShell.xaml
│       └── MauiProgram.cs
└── README.md
```

## Usage

### Connecting to Redis

1. Launch the application
2. Navigate to the "Connections" page
3. Fill in the connection details:
   - **Name**: A friendly name for the connection
   - **Host**: Redis server hostname (default: localhost)
   - **Port**: Redis server port (default: 6379)
   - **Password**: Optional password for authentication
   - **Database**: Database number (default: 0)
   - **SSL**: Enable for secure connections
4. Click "Add Connection"
5. Click "Connect" to establish the connection

### Managing Keys

1. Navigate to the "Keys" page
2. Enter a search pattern (e.g., `user:*`) or use `*` for all keys
3. Click "Search" to load keys
4. Click "View" to see a key's value
5. Use "Add Key" to create new keys
6. Use "Delete" to remove keys

### Monitoring

1. Navigate to the "Monitor" page
2. Click "Load Info" to get current server statistics
3. Click "Start Monitoring" for real-time updates every 2 seconds
4. View metrics including:
   - Redis version
   - Memory usage
   - Total keys
   - Connected clients
   - Total commands processed
   - Server uptime

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is open source and available under the MIT License.

## Acknowledgments

- Built with [.NET MAUI](https://dotnet.microsoft.com/apps/maui)
- Uses [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis)
- Uses [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)