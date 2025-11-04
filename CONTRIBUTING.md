# Contributing to RedisAssistant

Thank you for your interest in contributing to RedisAssistant! This document provides guidelines and instructions for contributing.

## Code of Conduct

Please be respectful and constructive in all interactions. We aim to foster an open and welcoming environment.

## How to Contribute

### Reporting Bugs

If you find a bug:

1. Check if the issue already exists in the [Issues](https://github.com/JimmyKodu/RedisAssistant/issues)
2. If not, create a new issue with:
   - A clear title
   - Detailed description of the problem
   - Steps to reproduce
   - Expected vs actual behavior
   - Platform/OS information
   - Screenshots if applicable

### Suggesting Features

Feature suggestions are welcome! Please:

1. Check existing issues to avoid duplicates
2. Create a new issue with:
   - Clear description of the feature
   - Use cases and benefits
   - Possible implementation approach (optional)

### Pull Requests

1. **Fork the repository** and create a new branch from `main`
2. **Make your changes** following the coding guidelines below
3. **Test thoroughly** on your target platform(s)
4. **Update documentation** if needed
5. **Submit a pull request** with:
   - Clear description of changes
   - Related issue numbers
   - Screenshots for UI changes

## Development Setup

See [BUILDING.md](BUILDING.md) for detailed build instructions.

Quick start:
```bash
git clone https://github.com/JimmyKodu/RedisAssistant.git
cd RedisAssistant
dotnet workload install maui
dotnet restore
```

## Coding Guidelines

### C# Style

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and concise

### XAML Style

- Use consistent indentation (4 spaces)
- Group related attributes
- Use data binding where appropriate
- Follow MVVM pattern

### MVVM Architecture

This project uses the MVVM (Model-View-ViewModel) pattern:

- **Models**: Data structures in `Models/`
- **ViewModels**: Business logic in `ViewModels/` using CommunityToolkit.Mvvm
- **Views**: UI in `Views/` with XAML

### Naming Conventions

- **Files**: PascalCase (e.g., `ConnectionsViewModel.cs`)
- **Classes**: PascalCase (e.g., `RedisService`)
- **Methods**: PascalCase (e.g., `ConnectAsync`)
- **Private fields**: _camelCase (e.g., `_redisService`)
- **Properties**: PascalCase (e.g., `IsConnected`)

### Async/Await

- Use async/await for I/O operations
- Suffix async methods with `Async`
- Handle exceptions appropriately

Example:
```csharp
public async Task<bool> ConnectAsync(RedisConnection connection)
{
    try
    {
        // Implementation
        return true;
    }
    catch (Exception ex)
    {
        // Log error
        return false;
    }
}
```

### Dependency Injection

Use constructor injection for dependencies:

```csharp
public class ConnectionsViewModel : ObservableObject
{
    private readonly IRedisService _redisService;

    public ConnectionsViewModel(IRedisService redisService)
    {
        _redisService = redisService;
    }
}
```

## Project Structure

```
src/RedisAssistant/
├── Models/              # Data models
├── ViewModels/          # MVVM ViewModels
├── Views/               # XAML pages
├── Services/            # Business logic services
├── Converters/          # Value converters for XAML
├── Resources/           # Images, fonts, styles
└── Platforms/           # Platform-specific code
```

## Testing

Currently, the project doesn't have automated tests. When contributing:

1. Manually test your changes on target platforms
2. Verify existing functionality still works
3. Test edge cases and error conditions

Future contributions for unit/integration tests are welcome!

## Adding Dependencies

When adding NuGet packages:

1. Use stable versions when possible
2. Update the project file (.csproj)
3. Document why the dependency is needed
4. Ensure it's compatible with all target platforms

## Documentation

Update documentation for:

- New features
- API changes
- Breaking changes
- Configuration options

## Commit Messages

Write clear commit messages:

```
[Component] Brief description

Detailed explanation if needed:
- What changed
- Why it changed
- Any breaking changes
```

Examples:
- `[UI] Add search functionality to Keys page`
- `[Service] Implement connection pooling for Redis`
- `[Fix] Handle connection timeout errors gracefully`

## Review Process

Pull requests will be reviewed for:

- Code quality and style
- Functionality and correctness
- Test coverage
- Documentation
- Platform compatibility

## Need Help?

- Open an issue with the `question` label
- Reach out to maintainers

## License

By contributing, you agree that your contributions will be licensed under the project's MIT License.

Thank you for contributing to RedisAssistant! 🎉
