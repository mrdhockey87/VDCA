using Microsoft.Maui.Devices;

namespace VDCA.Utils
{
    public static class VDCADeviceOrientation
    {

        public static DisplayOrientation DeviceOrientationChange()
        {
            DisplayOrientation Orientation = DisplayOrientation.Unknown;
            if (Orientation != DeviceDisplay.Current.MainDisplayInfo.Orientation)
            {
                //Orientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
                Orientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
                return Orientation;
            }
            else
            {
                return Orientation;
            }
        }
    }
}
