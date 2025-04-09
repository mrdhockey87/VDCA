using Microsoft.Maui.Controls;
using VDCA.Ask;
using VDCA.CustomControl;

namespace VDCA.Views;

public partial class MainContentLandscape : ContentView
{

    public Grid GridPort;
    public IAskView AskView;
    public ContentView AskViewContainer;
    private readonly AskViewLandscape AskLandscape;
    public RowDefinition AskRow;
    public Image Logo;
    public Button CloseAsk;
    public Button Flashcard;
    public Button Practice;
    public Button Quiz;
    public Button Review;
    public Label VersionNo;
    public MainPage MP;
    public MainContentLandscape MLP;

    public MainContentLandscape()
	{
		InitializeComponent();
        GridPort = GirdLandscape;
        Logo = logo;
        AskViewContainer = ask_view_holder;
        AskRow = ask_row;
        VersionNo = version_label;
        Flashcard = flash_button;
        Practice = practice_button;
        Quiz = quiz_button;
        Review = review_button;
        AskLandscape = new AskViewLandscape();
        AskView = AskLandscape;
        AskViewContainer.Content = AskView as ContentView;
    }
}
