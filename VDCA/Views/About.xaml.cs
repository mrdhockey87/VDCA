using VDCA.Models;

namespace VDCA.Views;

public partial class About : ContentPage
{
    private DBVersionNo _versionInfo;
    public DBVersionNo VersionInfo
    {
        get => _versionInfo;
        set
        {
            if (_versionInfo != value)
            {
                _versionInfo = value;
                OnPropertyChanged(nameof(VersionInfo));
            }
        }
    }
    private string _title = "VA Accredited Exam Study Guide";
    public string TitleString
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(TitleString));
        }
    }
    private string _statement = "Your resource for exam preparation.";
    public string StatementString
    {
        get => _statement;
        set
        {
            _statement = value;
            OnPropertyChanged(nameof(StatementString));
        }
    }
    private string NaneVdcaLocal { get; set; } = "Veteran's Disability Claims Assistance (VDCA)";
    public string NameVdca
    {
        get => NaneVdcaLocal;
        set
        {
            NaneVdcaLocal = value;
            OnPropertyChanged(nameof(NameVdca));
        }
    }
    public About()
	{
		InitializeComponent();
        BindingContext = this;
        Title = "About";
        NameVdca = "Veteran's Disability Claims Assistance (VDCA)";
        TitleString = "VA Accredited Exam Study Guide";
        StatementString = "Your resource for exam preparation.";
        VersionInfo = Constants.AppVersionNumberInfo;        
        OnPropertyChanged(nameof(TitleString));
        OnPropertyChanged(nameof(StatementString));
        OnPropertyChanged(nameof(VersionInfo));
        OnPropertyChanged(nameof(NameVdca));
    }
}