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
    private double _Width = 0;
    private double _Height = 0;
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
    private async void OnExit(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (_Width != width || _Height != height)
        {
            _Width = width;
            _Height = height;
            var isTablet = DeviceInfo.Idiom == DeviceIdiom.Tablet;
            if (_Width > _Height && !(DeviceInfo.Platform == DevicePlatform.WinUI) && !(DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst))
            {
                logoImage.Source = "vdca_solid_color_top.png";
            }
            else
            {
                logoImage.Source = "vdca_solid_color.png";
            }
        }
    }
}