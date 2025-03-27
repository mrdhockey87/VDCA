using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Storage;
using System;

namespace VDCA.CustomControl
{
    public class OnboardingSupportHelp
    {
        public static string VersionPreferenceKey = "AppVersion";
        public static string DontShowSupportPreferenceKey = "DontShowOnboardingSupport";
        public static int CurrentVersion { get; set; }
        public static int PreviousVersion { get; set; }

        public OnboardingSupportHelp()
        {
        }
        //If the support required is true return the inverted dont show value else return false mdail 11-12-24
        public static bool SupportReguiredThisPage(SupportedPages page)
        {
            if(OnboardingPreferenceService.GetSupportRequired(page))
            {
                return  !(OnboardingPreferenceService.GetDontShowSupport(page));
            }
            return false;
        }

        public static void GetVersions()
        {
            CurrentVersion = GetMajorVersion();
            PreviousVersion = GetPreviousVersion();       
        }
        public static int GetMajorVersion()
        {
            var versionString = AppVersion.AppVersionNo; // Get the full version string
            int version = Int32.Parse(versionString.Split('.')[0]);// Return the major version (e.g., '1' from '1.0.0') as an int
            return version;
        }
        public static int GetPreviousVersion()
        {
            return Preferences.Get(VersionPreferenceKey, 0);
        }
        public static void SetPreviousVersion(int version)
        {
            Preferences.Set(VersionPreferenceKey, version);
        }
        public static Rect GetAdjustedBounds(VisualElement control, VisualElement page, VisualElement onboardingOverlayControl)
        {
            var bounds = control.Bounds;
            var screenDensity = DeviceDisplay.MainDisplayInfo.Density;

            // Get the position of the control on the page
            var controlPosition = control.TranslateTo(0, 0, 0, Easing.Linear);
            var controlBounds = new Rect(bounds.X * screenDensity, bounds.Y * screenDensity, bounds.Width * screenDensity, bounds.Height * screenDensity);

            // Calculate the position of the OnboardingOverlayControl relative to the control
            var overlayWidth = onboardingOverlayControl.Width * screenDensity;
            var overlayHeight = onboardingOverlayControl.Height * screenDensity;

            // Get the page bounds
            var pageBounds = new Rect(0, 0, page.Width * screenDensity, page.Height * screenDensity);

            // Determine the best position for the overlay
            Rect adjustedBounds;

            if (controlBounds.Y + controlBounds.Height + overlayHeight <= pageBounds.Height)
            {
                // Place the overlay below the control
                adjustedBounds = new Rect(controlBounds.X, controlBounds.Y + controlBounds.Height, overlayWidth, overlayHeight);
            }
            else if (controlBounds.Y - overlayHeight >= 0)
            {
                // Place the overlay above the control
                adjustedBounds = new Rect(controlBounds.X, controlBounds.Y - overlayHeight, overlayWidth, overlayHeight);
            }
            else if (controlBounds.X + controlBounds.Width + overlayWidth <= pageBounds.Width)
            {
                // Place the overlay to the right of the control
                adjustedBounds = new Rect(controlBounds.X + controlBounds.Width, controlBounds.Y, overlayWidth, overlayHeight);
            }
            else if (controlBounds.X - overlayWidth >= 0)
            {
                // Place the overlay to the left of the control
                adjustedBounds = new Rect(controlBounds.X - overlayWidth, controlBounds.Y, overlayWidth, overlayHeight);
            }
            else
            {
                // Default to placing the overlay below the control
                adjustedBounds = new Rect(controlBounds.X, controlBounds.Y + controlBounds.Height, overlayWidth, overlayHeight);
            }

            return adjustedBounds;
        }

        public static ArrowDirection GetArrowDirection(Rect targetBounds, Rect customControlBounds)
        {
            if (customControlBounds.X < targetBounds.X && customControlBounds.Y < targetBounds.Y)
            {
                return ArrowDirection.UpLeft;
            }
            else if (customControlBounds.X > targetBounds.X && customControlBounds.Y < targetBounds.Y)
            {
                return ArrowDirection.UpRight;
            }
            else if (customControlBounds.X < targetBounds.X && customControlBounds.Y > targetBounds.Y)
            {
                return ArrowDirection.DownLeft;
            }
            else if (customControlBounds.X > targetBounds.X && customControlBounds.Y > targetBounds.Y)
            {
                return ArrowDirection.DownRight;
            }
            else if (customControlBounds.Y < targetBounds.Y)
            {
                return ArrowDirection.Up;
            }
            else if (customControlBounds.Y > targetBounds.Y)
            {
                return ArrowDirection.Down;
            }
            else if (customControlBounds.X < targetBounds.X)
            {
                return ArrowDirection.Left;
            }
            else
            {
                return ArrowDirection.Right;
            }
        }

    }
}
