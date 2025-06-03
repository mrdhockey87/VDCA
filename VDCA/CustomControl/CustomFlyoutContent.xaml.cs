using System.Windows.Input;

namespace VDCA.CustomControl;

public partial class CustomFlyoutContent : ContentView
{
    public static readonly BindableProperty ShellReferenceProperty =
        BindableProperty.Create(nameof(ShellReference), typeof(AppShell), typeof(CustomFlyoutContent));

    public AppShell ShellReference
    {
        get => (AppShell)GetValue(ShellReferenceProperty);
        set => SetValue(ShellReferenceProperty, value);
    }
    public ICommand TapGestureRecognizerCommand { get; private set; }
    public CustomFlyoutContent()
	{
		InitializeComponent();
        TapGestureRecognizerCommand = new Command<string>(OnTapped);
        BindingContext = this;
    }
    private async void OnTapped(string parameter)
    {
        if (int.TryParse(parameter, out int selected))
        {
            switch (selected)
            {
                case 0:
                    zero.IsEnabled = false;
                    await ShellReference?.OnHomeClicked();
                    zero.IsEnabled = true;
                    break;
                case 1:
                    one.IsEnabled = false;
                    await ShellReference?.OnFlashcardsClicked();
                    one.IsEnabled = true;
                    break;
                case 2:
                    two.IsEnabled = false;
                    await ShellReference?.OnPracticeClicked();
                    Shell.Current.FlyoutIsPresented = false;
                    two.IsEnabled = true;
                    break;
                case 3:
                    three.IsEnabled = false;
                    await ShellReference?.OnSelectQuizzesClicked();
                    Shell.Current.FlyoutIsPresented = false;
                    three.IsEnabled = true;
                    break;
                case 4:
                    four.IsEnabled = false;
                    await ShellReference?.OnReviewQuizzesClicked();
                    Shell.Current.FlyoutIsPresented = false;
                    four.IsEnabled = true;
                    break;
                case 5:
                    five.IsEnabled = false;
                    await ShellReference?.OnFlaggedOnlyClicked();
                    five.IsEnabled = true;
                    break;
                case 6:
                    six.IsEnabled = false;
                    await ShellReference?.OnHiddenOnlyClicked();
                    six.IsEnabled = true;
                    break;
                case 7:
                    seven.IsEnabled = false;
                    await ShellReference?.OnClearFlaggedClicked();
                    seven.IsEnabled = true;
                    break;
                case 8:
                    eight.IsEnabled = false;
                    await ShellReference?.OnClearHiddenClicked();
                    eight.IsEnabled = true;
                    break;
                case 9:
                    nine.IsEnabled = false;
                    await ShellReference?.OnClearReviewClicked();
                    nine.IsEnabled = true;
                    break;
                case 10:
                    ten.IsEnabled = false;
                    await ShellReference?.OnSendFeedbackClicked();
                    ten.IsEnabled = true;
                    break;
                case 11:
                    eleven.IsEnabled = false;
                    await ShellReference?.OnLincesesInfoClicked();
                    eleven.IsEnabled = true;
                    break;
                case 12:
                    twelve.IsEnabled = false;
                    await ShellReference?.OnRateAppClicked();
                    twelve.IsEnabled = true;
                    break;
                case 13:
                    thirteen.IsEnabled = false;
                    await ShellReference?.OnShowHelpClicked();
                    thirteen.IsEnabled = true;
                    break;
                case 14:
                    fourteen.IsEnabled = false;
                    await ShellReference?.OnAboutClicked();
                    fourteen.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}