# Building RedisAssistant

## Overview

RedisAssistant is a .NET MAUI application that requires the MAUI workload to be installed. This document provides instructions for building the application on different platforms.

## Prerequisites

### All Platforms

1. Install [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
2. Install the .NET MAUI workload:
   ```bash
   dotnet workload install maui
   ```

### Windows

- Windows 10 version 1809 or higher (Build 17763 or higher)
- Visual Studio 2022 17.8 or later (recommended)
  - Workloads to install:
    - .NET Multi-platform App UI development
    - .NET desktop development

**Note:** On Windows, you can build for Windows and Android platforms.

### macOS

- macOS 11 (Big Sur) or higher
- Xcode 14 or later
- Visual Studio 2022 for Mac or Visual Studio Code with C# extension

**Note:** On macOS, you can build for macOS (Mac Catalyst), iOS, and Android platforms.

### Linux

**Important:** .NET MAUI does not fully support Linux as a build/target platform. While you can edit code on Linux, you need to build and run the application on Windows or macOS.

## Build Instructions

### Using Visual Studio

1. Open `RedisAssistant.sln` in Visual Studio 2022
2. Select your target platform from the debug target dropdown
3. Press F5 to build and run, or right-click the project and select "Build"

### Using Command Line

#### Windows
```bash
# Build for Windows
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-windows10.0.19041.0

# Run on Windows
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-windows10.0.19041.0 -t:Run
```

#### macOS (Mac Catalyst)
```bash
# Build for macOS
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-maccatalyst

# Run on macOS
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-maccatalyst -t:Run
```

#### Android
```bash
# Build for Android
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-android

# Run on Android (device or emulator must be connected)
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-android -t:Run
```

#### iOS (macOS only)
```bash
# Build for iOS
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-ios

# Run on iOS simulator
dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-ios -t:Run
```

## CI/CD Considerations

### GitHub Actions / Azure DevOps

For CI/CD pipelines, you'll need:

1. **Windows Agents** for building Windows and Android apps
2. **macOS Agents** for building iOS and macOS apps

Example GitHub Actions workflow snippet:

```yaml
jobs:
  build-windows:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'
      - run: dotnet workload install maui
      - run: dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-windows10.0.19041.0

  build-macos:
    runs-on: macos-latest
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'
      - run: dotnet workload install maui
      - run: dotnet build src/RedisAssistant/RedisAssistant.csproj -f net9.0-maccatalyst
```

## Common Build Issues

### "NETSDK1147: To build this project, the following workloads must be installed"

**Solution:** Install the MAUI workload:
```bash
dotnet workload install maui
```

### "The project doesn't know how to run the profile"

**Solution:** Ensure you're using the correct target framework for your platform:
- Windows: `net9.0-windows10.0.19041.0`
- macOS: `net9.0-maccatalyst`
- Android: `net9.0-android`
- iOS: `net9.0-ios`

### Android Build Errors

**Solution:** Ensure Android SDK is installed:
```bash
dotnet workload install android
# or
dotnet workload install maui-android
```

### iOS/macOS Build Errors

**Solution:** Ensure Xcode is installed and accepted:
```bash
sudo xcode-select --switch /Applications/Xcode.app
sudo xcodebuild -license accept
```

## Publishing

### Windows

To create a Windows installer:
```bash
dotnet publish src/RedisAssistant/RedisAssistant.csproj -f net9.0-windows10.0.19041.0 -c Release
```

### macOS

To create a macOS app bundle:
```bash
dotnet publish src/RedisAssistant/RedisAssistant.csproj -f net9.0-maccatalyst -c Release
```

### Android

To create an APK:
```bash
dotnet publish src/RedisAssistant/RedisAssistant.csproj -f net9.0-android -c Release
```

To create an AAB (for Google Play):
```bash
dotnet publish src/RedisAssistant/RedisAssistant.csproj -f net9.0-android -c Release -p:AndroidPackageFormat=aab
```

### iOS

To create an IPA (requires a Mac and Apple Developer account):
```bash
dotnet publish src/RedisAssistant/RedisAssistant.csproj -f net9.0-ios -c Release
```

## Development Tips

1. **Hot Reload**: MAUI supports hot reload for XAML and C# changes
2. **Multi-Platform Testing**: Use simulators/emulators for quick testing
3. **Debugging**: Use Visual Studio's built-in debugger with breakpoints
4. **XAML Preview**: Visual Studio provides real-time XAML preview

## Additional Resources

- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [MAUI GitHub Repository](https://github.com/dotnet/maui)
- [MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)
