using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VDCA.SendEmails;

namespace VDCA.Ask;

public partial class AskViewPortrait : ContentView, IAskView
{
	public AskViewPortrait()
	{
		InitializeComponent();
        //AskConstants.askText[0].StandardFont = 14;
        //AskConstants.askText[0].MessageFont = 16;
        //AskConstants.askText[0].GlyphSize = 16;
        BindingContext = this;
        dont_ask_button_portrait.IsVisible = dont_visisble;
        SetupAskView();
    }

    public ObservableCollection<AskText> PortriatAskText
    {
        set => _ = AskConstants.askText;
        get => AskConstants.askText;
    }
    public bool dont_visisble = Constants.DONT_ASK;

    //Set up the text for the button and text field and send user to rating page or send feedback or hide the page mdail 8-18-21
    public void SetupAskView()
    {
        if (!Constants.SHOW_ASK || Constants.DONTSHOW)
        {
            IsVisible = false;
            return;
        }
        if (AskConstants.ASK_COUNT == -1)
        {
            Constants.SHOW_ASK = false;
            ask_view_portrait.IsVisible = false;
        }
        else if (AskConstants.ASK_COUNT == 0)
        {
            PortriatAskText[0].ResetAskText();
        }
        else if (AskConstants.ASK_COUNT == 1)
        {
            PortriatAskText[0].SetAskTextYes();
        }
        else if (AskConstants.ASK_COUNT == 2)
        {
            PortriatAskText[0].SetAskTextNo();
        }
    }
    public void SetIsVisible(bool isVisible)
    {
        IsVisible = isVisible;
    }
    private void OnAskCloseButtonClicked(object sender, EventArgs e)
    {
        SetAskGone();
    }
    private async void OnAskYesButtonClicked(object sender, EventArgs e)
    {
        if (AskConstants.ASK_COUNT == 0)
        {
            //if ask count is 0 this is the first click, set count to 1 and reset the view to positive messages mdail 8-18-21
            AskConstants.ASK_COUNT = 1;
            SetupAskView();
        }
        else if (AskConstants.ASK_COUNT == 1)
        {
            //if ask count is one yes was clicked first time so direct to rate app mdail 8-18-21
            await Constants.SendToRatingPage();
            SetDontAsk();
        }
        else if (AskConstants.ASK_COUNT == 2)
        {
            //if ask count is two no was clicked first time, do send feedback mdail 8-18-21
            if (AskConstants.mpForAsk is not null)
            {
                //if ask count is two no was clicked first time, do send feedback mdail 8-18-21
                SendFeedbackEmails sfe = new();
                await sfe.SendAppFeedback();
                SetDontAsk();
            }
        }
    }

    private void OnAskNoButtonClicked(object sender, EventArgs e)
    {
        if (AskConstants.ASK_COUNT == 0)
        {
            //if ask count is 0 this is the first click, set count to 2 and reset the view to negative messages mdail 8-18-21
            AskConstants.ASK_COUNT = 2;
            SetupAskView();
        }
        else
        {
            //if ask count isn't 0, then this is the second response close the ask view and reset ask count to -1 mdail 8-18-21                
            SetDontAsk();
            AppPreferences ap = new();
            ap.SetRejected();
        }
    }

    private void OnAskDontButtonClicked(object sender, EventArgs e)
    {
        SetDontAsk();
    }

    private void SetAskGone()
    {
        AppPreferences ap = new();
        ap.SetTempOff();
        Constants.SHOW_ASK = false;
        if (ask_view_portrait is not null)
        {
            ask_view_portrait.IsVisible = Constants.SHOW_ASK;
        }
        AskConstants.ASK_COUNT = -1;
        AskConstants.mpForAsk?.CloseAskViews();
    }
    //set to not ask anymore for this version mdail 9-9-21
    private void SetDontAsk()
    {
        SetAskGone();
        AppPreferences ap = new();
        ap.SetDontAsk();
    }
}