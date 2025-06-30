using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VDCA.Models;
using VDCA.Views;

namespace VDCA.ViewModels;

public partial class WindowsPracticecardViewModel : WindowsCardViewModel
{
    //private string LOGTAG = "PracticeCardViewModel";
    public WindowsPracticecardView pcv;
    public WindowsPracticecardViewModel()
    {
        CardQuestions = [.. Constants.Questions];
    }
}