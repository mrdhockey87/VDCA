using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VDCA.Models;

namespace VDCA.ViewModels;

public partial class FlashcardViewModel : CardViewModel
{
    //private static readonly string LOGTAG = "FlashcardViewModel";
    public FlashcardViewModel() : base()
    {
        CardQuestions = [.. Constants.Questions];
    }
}