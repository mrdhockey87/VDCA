using VDCA.Models;
using VDCA.Services;
using VDCA.Utils;

namespace VDCA.Views;

public partial class LicenseView : ContentPage, IQueryAttributable
{
    private LicenseInfo _licenseInfo;
    private readonly LicenseService _licenseService = new();
    private string LicenseStringLocal { get; set; } = "";
    public string LicenseString
    {
        get => LicenseStringLocal;
        set
        {
            LicenseStringLocal = value;
            OnPropertyChanged(nameof(LicenseString));
        }
    }
    public LicenseView()
    {
        InitializeComponent();
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("PackageName", out var packageNameObj) && packageNameObj is string packageName)
        {
            LoadLicenseInfo(packageName);
        }
    }

    private async void LoadLicenseInfo(string packageName)
    {
        var licenses = await LicenseService.GetLicenseInfoAsync();
        _licenseInfo = licenses.FirstOrDefault(l => l.PackageName == packageName);
        if (_licenseInfo != null)
        {
            Title = $"{_licenseInfo.PackageName} License";
            await LoadLicenseText();
        }
        else
        {
            Title = "License Not Found";
        }
    }
    private async Task LoadLicenseText()
    {
        try
        {
            LicenseTextLabel.IsVisible = false;
            string textstring = await LicenseService.GetLicenseTextAsync(_licenseInfo.LicenseFileName);
            LicenseString = textstring.Replace("[Copyright]", _licenseInfo.Copyright);
            //LicenseTextLabel.Text = licenseText;
            LicenseTextLabel.IsVisible = true;
        }
        catch (Exception ex)
        {
            LicenseString = $"Error loading license text: {ex.Message}";
            LicenseTextLabel.IsVisible = true;
        }
    }
}