#if MACCATALYST
using Foundation;
using UIKit;
using System;

namespace VDCA.Platforms.MacCatalyst
{
    public class MacMenuBarService
    {
        private IUIMenuBuilder _menuBuilder;

        public void SetMenuBuilder(IUIMenuBuilder builder)
        {
            _menuBuilder = builder;
        }

        public IUIMenuBuilder GetMenuBuilder()
        {
            return _menuBuilder;
        }

        public void RemoveDefaultMenuItems()
        {
            if (_menuBuilder == null)
                return;

            try
            {
                // Remove default menu items using their identifiers
                // These are the standard macOS application menu identifiers
                
                // Remove File menu completely
                _menuBuilder.RemoveMenu(new NSString("com.apple.menu.file"));
                
                // Remove individual File menu items as backup
                _menuBuilder.RemoveMenu(new NSString("UINSStandardAboutMenuItem"));
                _menuBuilder.RemoveMenu(new NSString("UINSStandardHideMenuItem"));
                _menuBuilder.RemoveMenu(new NSString("UINSStandardHideOthersMenuItem"));
                _menuBuilder.RemoveMenu(new NSString("UINSStandardShowAllMenuItem"));
                _menuBuilder.RemoveMenu(new NSString("UINSStandardQuitMenuItem"));
                
                // Remove Edit menu
                _menuBuilder.RemoveMenu(new NSString("com.apple.menu.edit"));
                
                // Remove View menu
                _menuBuilder.RemoveMenu(new NSString("com.apple.menu.view"));
                
                // Remove Window menu
                _menuBuilder.RemoveMenu(new NSString("com.apple.menu.window"));
                
                // Remove Help menu
                _menuBuilder.RemoveMenu(new NSString("com.apple.menu.help"));
                
                // Remove Format menu if present
                _menuBuilder.RemoveMenu(new NSString("com.apple.menu.format"));
                
                // Remove Services menu if present
                _menuBuilder.RemoveMenu(new NSString("com.apple.menu.services"));
                
                System.Diagnostics.Debug.WriteLine("Default menu items removal attempted - including File menu");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error removing default menu items: {ex.Message}");
            }
        }

        public void SetNeedsRebuild()
        {
            if (_menuBuilder == null)
                return;

            try
            {
                // Since IUIMenuBuilder doesn't have SetNeedsRebuild directly,
                // we'll use a workaround by accessing the system property
                var builder = _menuBuilder;
                
                // Access the native UIMenuSystem to trigger rebuild
                if (builder.System != null)
                {
                    builder.System.SetNeedsRebuild();
                    System.Diagnostics.Debug.WriteLine("Menu builder SetNeedsRebuild called via System property");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Menu builder System property is null");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calling SetNeedsRebuild: {ex.Message}");
            }
        }

        public void RebuildMenu()
        {
            if (_menuBuilder == null)
                return;

            try
            {
                // Force a complete rebuild
                SetNeedsRebuild();
                System.Diagnostics.Debug.WriteLine("Menu rebuild requested");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error rebuilding menu: {ex.Message}");
            }
        }
    }
}
#endif