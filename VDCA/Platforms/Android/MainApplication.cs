using Android.App;
using Android.Runtime;
using Android.Graphics;
//using VDCA.Droid;

namespace VDCA;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
	{
        //DependencyService.Register<TextMeasurement_Android>();
    }

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
