# RedisAssistant - Project Summary

## Overview

RedisAssistant is a cross-platform desktop GUI application for visual Redis management and monitoring, built with .NET MAUI.

**Chinese Description**: 用于可视化管理和监控的桌面 Redis GUI基于.NET MAUI

## Quick Facts

- **Language**: C# with XAML
- **Framework**: .NET 9.0 MAUI
- **Platforms**: Windows, macOS, iOS, Android
- **Architecture**: MVVM
- **License**: MIT

## Core Features

### 1. Connection Management
- Add, save, and manage multiple Redis connections
- Support for standard and SSL/TLS connections
- Password authentication
- Database selection
- Connection status monitoring

### 2. Key Management
- Search keys with pattern matching (e.g., `user:*`)
- View key details (type, size, TTL)
- Add new keys with values
- Delete existing keys
- Real-time key listing

### 3. Server Monitoring
- Redis version information
- Memory usage statistics
- Total keys count
- Connected clients count
- Total commands processed
- Server uptime
- Real-time monitoring with 2-second auto-refresh

## Technology Stack

### Core
- **.NET 9.0**: Latest .NET framework
- **.NET MAUI**: Multi-platform App UI framework

### NuGet Packages
- **StackExchange.Redis** (v2.8.16): High-performance Redis client
- **CommunityToolkit.Mvvm** (v8.3.2): MVVM helpers and utilities
- **Microsoft.Extensions.Logging.Debug** (v9.0.0): Debug logging

### Development Tools
- **Visual Studio 2022** or **Visual Studio Code**
- **.NET MAUI Workload**: Required for building
- **Xcode**: Required for iOS/macOS builds (Mac only)

## Project Statistics

- **Total Files**: 47 source files
- **C# Files**: 19
- **XAML Files**: 10
- **Documentation**: 5 markdown files
- **Lines of Code**: ~2,500+ lines

## Project Structure

```
RedisAssistant/
├── .github/
│   ├── ISSUE_TEMPLATE/        # Bug and feature request templates
│   └── workflows/             # CI/CD workflows
├── src/RedisAssistant/
│   ├── Converters/            # XAML value converters
│   ├── Models/                # Data models
│   ├── ViewModels/            # MVVM ViewModels
│   ├── Views/                 # XAML pages
│   ├── Services/              # Business logic
│   ├── Resources/             # App resources
│   │   ├── AppIcon/          # Application icon
│   │   ├── Fonts/            # Custom fonts
│   │   ├── Images/           # Images
│   │   ├── Splash/           # Splash screen
│   │   └── Styles/           # XAML styles
│   ├── Platforms/             # Platform-specific code
│   │   ├── Android/
│   │   ├── iOS/
│   │   ├── MacCatalyst/
│   │   └── Windows/
│   ├── App.xaml               # Application definition
│   ├── AppShell.xaml          # Shell navigation
│   └── MauiProgram.cs         # App entry point
├── ARCHITECTURE.md             # Architecture documentation
├── BUILDING.md                 # Build instructions
├── CONTRIBUTING.md             # Contribution guidelines
├── LICENSE                     # MIT License
└── README.md                   # Project README
```

## Key Design Patterns

### MVVM (Model-View-ViewModel)
- **Models**: Data structures (`RedisConnection`, `RedisKey`, `RedisServerInfo`)
- **ViewModels**: Business logic with `ObservableObject` base
- **Views**: XAML UI with data binding

### Dependency Injection
- Services registered in `MauiProgram.cs`
- Constructor injection for dependencies
- Singleton lifetime for services and ViewModels

### Async/Await
- All I/O operations are asynchronous
- Proper exception handling
- User-friendly error messages

## Build Requirements

### Windows
- Windows 10 version 1809+
- .NET 9.0 SDK
- MAUI workload: `dotnet workload install maui`

### macOS
- macOS 11+
- .NET 9.0 SDK
- Xcode 14+
- MAUI workload: `dotnet workload install maui`

### Linux
**Not supported** for building MAUI apps. Use Windows or macOS.

## Quick Start

```bash
# Clone repository
git clone https://github.com/JimmyKodu/RedisAssistant.git
cd RedisAssistant

# Install MAUI workload
dotnet workload install maui

# Restore packages
dotnet restore

# Build for Windows
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-windows10.0.19041.0

# Build for macOS
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-maccatalyst

# Build for Android
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-android

# Build for iOS (macOS only)
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-ios
```

## Security

### Dependency Security
✅ All NuGet packages scanned - **No vulnerabilities found**

### Code Security
✅ CodeQL analysis completed - **Issues addressed**
- Fixed GitHub Actions permissions
- No C# security vulnerabilities

### Application Security
- SSL/TLS support for Redis connections
- Secure password handling
- No credentials stored (in-memory only)

## CI/CD

GitHub Actions workflow included for automated builds:
- Windows builds (Windows + Android)
- macOS builds (macOS + iOS)
- Runs on push and pull requests

## Documentation

- **README.md**: Project overview and usage
- **BUILDING.md**: Detailed build instructions
- **CONTRIBUTING.md**: Contribution guidelines
- **ARCHITECTURE.md**: System architecture details
- **This file**: Project summary

## Future Enhancements

Potential features for future versions:
1. Secure connection storage
2. Advanced data type support (Lists, Sets, Hashes, etc.)
3. Pub/Sub monitoring
4. Redis Cluster support
5. Performance charts and graphs
6. Import/Export functionality
7. CLI commands execution
8. Connection pooling optimization

## License

MIT License - See [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## Support

- **Issues**: [GitHub Issues](https://github.com/JimmyKodu/RedisAssistant/issues)
- **Discussions**: [GitHub Discussions](https://github.com/JimmyKodu/RedisAssistant/discussions)

## Acknowledgments

- Built with [.NET MAUI](https://dotnet.microsoft.com/apps/maui)
- Redis client: [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis)
- MVVM toolkit: [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)

---

**Version**: 1.0.0  
**Last Updated**: November 2025  
**Status**: ✅ Complete and Ready for Use
