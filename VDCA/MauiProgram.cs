using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using PanCardView;
namespace VDCA;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseCardsView()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup();
#if IOS || MACCATALYST
builder.ConfigureMauiHandlers(handlers =>
{
    handlers.AddHandler<CollectionView, Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2>();
});
#endif
        if (!(DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst))
        {
            AppCenter.Start("android=33b3da51-def2-48b0-bc38-03af086b761c;" +
                      "windowsdesktop=2070bd03-f595-4311-879d-f89187caf565;" +
                     "ios=0953c04f-a693-44d7-95cd-bf765b241822;" +
                      "macos=35a1bbb9-0c83-451d-8873-8c449a9813e6;",
                      typeof(Crashes));
        }
        return builder.Build();
	}
}
