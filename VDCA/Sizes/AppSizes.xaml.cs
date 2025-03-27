using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace VDCA.Sizes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSizes : ResourceDictionary
    {
        public AppSizes()
        {
            //Visual Studio reports that InitializeComponent() is not found in the current context however if removed
            //the AppSizes.xaml file will not load and the app uses deault sizes that are wrong mdail 7-7-23
            InitializeComponent();
        }
    }
}
