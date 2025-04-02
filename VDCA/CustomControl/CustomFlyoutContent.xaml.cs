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
                    await ShellReference?.OnMenuItemClicked("///MainPage");
                    break;
                case 1:
                    await ShellReference?.OnMenuItemClicked("SelectFlash");
                    break;
                case 2:
                    await ShellReference?.OnMenuItemClicked("SelectPractice");
                    break;
                case 3:
                    await ShellReference?.OnMenuItemClicked("SelectQuiz");
                    break;
                case 4:
                    await ShellReference?.OnMenuItemClicked("ReviewQuizzes");
                    break;
                case 5:
                    await ShellReference?.OnFlaggedOnlyClicked();
                    break;
                case 6:
                    await ShellReference?.OnHiddenOnlyClicked();
                    break;
                case 7:
                    await ShellReference?.OnClearFlaggedClicked();
                    break;
                case 8:
                    await ShellReference?.OnClearHiddenClicked();
                    break;
                case 9:
                    await ShellReference?.OnClearReviewClicked();
                    break;
                case 10:
                    await ShellReference?.OnSendFeedbackClicked();
                    break;
                case 11:
                    await ShellReference?.OnRateAppClicked();
                    break;
                default:
                    break;
            }
        }
    }
}