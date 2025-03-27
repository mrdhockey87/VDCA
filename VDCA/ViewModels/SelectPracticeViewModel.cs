using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using VDCA.Data;
using VDCA.Models;
using VDCA.Utils;
using VDCA.Views;

namespace VDCA.ViewModels;

public partial class SelectPracticeViewModel : SelectViewModel
{    
    //private readonly string LOGTAG = "SelectPracticeViewModel";
    public SelectPracticeViewModel()
    {
        CategoriesCollection = Constants.Categories;
        SelectedCategories = [];
    }
    public override async Task CardsForSelectedSubjects()
    {
        if(SelectedCategories.Count == 0)
        {
            sv.ShowInformationAlert("Please Make a Selection", "No Subjects Selected, please make a selection and try again.");
            return;
        }
        await ShowProgressBarUtil.ShowProgressBar(sv.ProgressBarSelectOverlay);
        //build the part of the sql string to limit the results to the selections made on the page mdail 7-26-21
        String sqlPart = "";
        try
        {
            //have to use count int i; because the first part of the sqlPart string has to be different the other parts added as does the last mdail 4-1-23 
            for (int i = 0; i < SelectedCategories.Count; i++)
            {
                sqlPart = i == 0
                    ? " AND ((Category = '" + SelectedCategories[i].CategoryName + "')"
                    : sqlPart + " OR (Category = '" + SelectedCategories[i].CategoryName + "')";
            }
            if (sqlPart.Length > 0)
            {
                sqlPart += ")";
            }
            int Count = Constants.Questions.Count;
            if (Count > 0)
            {
                Constants.Questions.Clear();
            }
            QuestionsDatabase db = new();
            await db.GetSelectCategoryPracticeQuestions(sqlPart);
            if (Constants.Questions.Count > 0)
            {
                await SaveSelectedCats();
                await Shell.Current.GoToAsync("Practice");
            }
            else
            {
                string Information_Title = "Error No Question returned";
                string Information_Message = "Please insure selected question count is more than zero question and try again." + Constants.NEWLINE
                    + "If problem persists please close and restart the app." + Constants.NEWLINE
                    + "If problem still persists please send us feedback including the categories you had selected.";
                await sv.InformationSelectAlert.ShowAlertAsync(Information_Title, Information_Message);
                await SetAllDeselected();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
        finally
        {
            MainThread.BeginInvokeOnMainThread(async () => await ShowProgressBarUtil.HideProgressBar(sv.ProgressBarSelectOverlay));
        }
    }
}