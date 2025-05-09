using Microsoft.Maui.Controls;
using VDCA.Ask;
using VDCA.CustomControl;

namespace VDCA.Views;

public partial class MainContentPortrait : ContentView
{
    public Grid GridPort;
    public IAskView AskView;
    public ContentView AskViewContainer;
    private readonly AskViewPortrait AskPortrait;
    public RowDefinition AskRow;
    public Button CloseAsk;
    public Button Flashcard;
    public Button Practice;
    public Button Quiz;
    public Button Review;
    public MainPage MP;
    public MainContentPortrait MPP;

    public MainContentPortrait()
	{
		InitializeComponent();
        GridPort = GirdPortrait;
        AskViewContainer = ask_view_holder;
        AskRow = ask_row;
        Flashcard = flash_button;
        Practice = practice_button;
        Quiz = quiz_button;
        Review = review_button;
        AskPortrait = new AskViewPortrait();
        AskView = AskPortrait;
        AskViewContainer.Content = AskView as ContentView;
    }
}