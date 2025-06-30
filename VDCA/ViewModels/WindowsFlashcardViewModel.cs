using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VDCA.Models;

namespace VDCA.ViewModels;

public partial class WindowsFlashcardViewModel : WindowsCardViewModel
{
    //private static readonly string LOGTAG = "FlashcardViewModel";
    public WindowsFlashcardViewModel() : base()
    {
        CardQuestions = [.. Constants.Questions];
    }
}