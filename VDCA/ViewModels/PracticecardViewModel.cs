using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VDCA.Models;
using VDCA.Views;

namespace VDCA.ViewModels;

public partial class PracticecardViewModel : CardViewModel
{
    //private string LOGTAG = "PracticeCardViewModel";
    public PracticecardView pcv;
    public PracticecardViewModel()
    {
        CardQuestions = [.. Constants.Questions];
    }
}