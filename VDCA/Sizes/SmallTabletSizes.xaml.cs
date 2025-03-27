using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace VDCA.Sizes
{
    // Fix error reported during build on mac for maccatalyst mdail 6-12-24
    [XamlCompilationAttribute(XamlCompilationOptions.Skip)]
    public partial class SmallTabletSizes : ResourceDictionary
    {
        public SmallTabletSizes()
        {
            InitializeComponent();
        }
    }
}