
using Microsoft.Maui.Controls.Xaml;

namespace VDCA.Views;
// Fix error reported during build on mac for maccatalyst mdail 6-12-24
[XamlCompilationAttribute(XamlCompilationOptions.Skip)]
public partial class SelectPracticeView : SelectView
{
    //private readonly string LOGTAG = "SelectPracticeView";
    
    public SelectPracticeView()
    {
        InitializeComponent();
        IsFlashcards = false;
        ViewModel = spvm;
        SelectView.SetSelectView(practiceCatCollection, SelecetPracticeGrid);
        spvm.SetView(this);
        SetContextViews(informationSelectPracticeAlert, progressBarSelectPracticeOverlay);
    }
}
