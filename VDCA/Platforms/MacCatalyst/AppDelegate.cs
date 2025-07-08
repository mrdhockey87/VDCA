#if MACCATALYST
using Foundation;
using UIKit;
using VDCA.Platforms.MacCatalyst;

namespace VDCA;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    private MacMenuBarService _menuBarService;

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override void BuildMenu(IUIMenuBuilder builder)
    {
        base.BuildMenu(builder);

        // Create and configure the menu bar service
        _menuBarService = new MacMenuBarService();
        _menuBarService.SetMenuBuilder(builder);

        // Store the service instance globally for access
        MenuBarServiceProvider.SetInstance(_menuBarService);

        // Remove default menu items before building custom menu
        _menuBarService.RemoveDefaultMenuItems();
        
        System.Diagnostics.Debug.WriteLine("AppDelegate BuildMenu completed - MenuBarService initialized");
    }

    public MacMenuBarService GetMenuBarService()
    {
        return _menuBarService;
    }
}

// Helper class to provide global access to the MenuBar service
public static class MenuBarServiceProvider
{
    private static MacMenuBarService _instance;
    
    public static void SetInstance(MacMenuBarService instance)
    {
        _instance = instance;
        System.Diagnostics.Debug.WriteLine("MenuBarServiceProvider instance set");
    }
    
    public static MacMenuBarService GetInstance()
    {
        return _instance;
    }
    
    public static bool IsAvailable => _instance != null;
}

#else
using Foundation;

namespace VDCA;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

// Dummy implementations for other platforms
public static class MenuBarServiceProvider
{
    public static object GetInstance() => null;
    public static bool IsAvailable => false;
}
#endif
