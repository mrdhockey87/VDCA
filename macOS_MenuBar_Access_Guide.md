# macOS Menu Bar Access Guide

This implementation provides access to the macOS `IUIMenuBuilder` for customizing the native menu bar in your .NET MAUI application.

## Architecture

### 1. MacMenuBarService (`VDCA\Platforms\MacCatalyst\MacMenuBarService.cs`)
- Provides platform-specific access to `IUIMenuBuilder`
- Handles removal of default macOS menu items
- Provides `SetNeedsRebuild()` functionality through the System property

### 2. AppDelegate (`VDCA\Platforms\MacCatalyst\AppDelegate.cs`)
- Initializes the `MacMenuBarService` in the `BuildMenu` override
- Stores the service instance globally via `MenuBarServiceProvider`
- Automatically removes default menu items before building custom menus

### 3. MenuBarServiceProvider (Static Helper)
- Provides cross-platform access to the MacMenuBarService
- Returns null on non-macOS platforms for safe cross-platform code

## Usage

### From AppShell
```csharp
public void RefreshMenuBar()
{
    if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
    {
#if MACCATALYST
        var menuBarService = MenuBarServiceProvider.GetInstance();
        menuBarService?.SetNeedsRebuild();
#endif
    }
}
```

### From MainPage Constructor
The MainPage constructor automatically calls `RefreshMacMenuBar()` which:
- Accesses the MacMenuBarService
- Calls `SetNeedsRebuild()` to ensure proper menu display on first load
- Includes retry logic with delays for timing issues

### Direct IUIMenuBuilder Access
```csharp
#if MACCATALYST
var menuBarService = MenuBarServiceProvider.GetInstance();
var builder = menuBarService?.GetMenuBuilder();
if (builder != null)
{
    // Direct access to IUIMenuBuilder
    // Remove specific menu items, add custom menus, etc.
}
#endif
```

## Key Features

1. **Default Menu Removal**: Automatically removes standard macOS menus (File, Edit, View, Window, Help)
2. **SetNeedsRebuild Support**: Properly triggers menu rebuilding via the System property
3. **Cross-Platform Safe**: Uses conditional compilation to avoid issues on other platforms
4. **Retry Logic**: Handles timing issues with delayed retry attempts
5. **Debugging Support**: Extensive debug logging for troubleshooting

## Menu Rebuilding

The system provides multiple ways to trigger menu rebuilding:

1. **Automatic**: Called during app initialization in AppDelegate
2. **From MainPage**: Called in constructor for proper first-load display  
3. **From AppShell**: Available via `RefreshMenuBar()` method
4. **Manual**: Direct access via `MenuBarServiceProvider.GetInstance()`

## Error Handling

- All operations are wrapped in try-catch blocks
- Debug logging provides visibility into the process
- Graceful fallbacks for timing or availability issues
- Cross-platform compatibility checks

This implementation ensures your .NET MAUI app has full control over the macOS menu bar while maintaining compatibility with other platforms.