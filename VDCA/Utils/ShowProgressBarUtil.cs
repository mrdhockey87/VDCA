using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDCA.Views;

namespace VDCA.Utils
{
    public class ShowProgressBarUtil
    {
        public static async Task ShowProgressBar(ProgressBarOverlay ProgressBar)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await ProgressBar.ShowAsync();
            });
            await Task.Delay(5);
        }
        public static async Task HideProgressBar(ProgressBarOverlay ProgressBar)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                _ = ProgressBar.Hide();
            });
        }
    }
}
