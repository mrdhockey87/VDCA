
namespace VDCA.Views;

public partial class SelectFlashView : SelectView
{
    //private readonly string LOGTAG = "SelectCatFlashView";
    public SelectFlashView()
    {
        InitializeComponent();
        IsFlashcards = true;
        ViewModel = sfvm;
        SelectView.SetSelectView(flashcardCatCollection, SelecetFlashGrid);
        sfvm.SetView(this);
        SetContextViews(informationSelectFlashcardAlert, progressBarSelectFlashOverlay);
    }
}
