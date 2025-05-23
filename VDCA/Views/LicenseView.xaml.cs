using VDCA.Models;
using VDCA.Services;
using VDCA.Utils;

namespace VDCA.Views;

public partial class LicenseView : ContentPage, IQueryAttributable
{
    private LicenseInfo _licenseInfo;
    private readonly LicenseService _licenseService = new();

    public LicenseView()
    {
        InitializeComponent();
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("FileName", out var fileNameObj) && fileNameObj is string fileName)
        {
            LoadLicenseInfo(fileName);
        }
    }

    private async void LoadLicenseInfo(string fileName)
    {
        var licenses = await _licenseService.GetLicenseInfoAsync();
        _licenseInfo = licenses.FirstOrDefault(l => l.LicenseFileName == fileName);
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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ShowProgressBarUtil.ShowProgressBar(progressBarSelectOverlay);
        await LoadLicenseText();
        await ShowProgressBarUtil.HideProgressBar(progressBarSelectOverlay);
    }
    private async Task LoadLicenseText()
    {
        try
        {
            LicenseTextLabel.IsVisible = false;
            var licenseText = await _licenseService.GetLicenseTextAsync(_licenseInfo.LicenseFileName);
            LicenseTextLabel.Text = licenseText;
            LicenseTextLabel.IsVisible = true;
        }
        catch (Exception ex)
        {
            LicenseTextLabel.Text = $"Error loading license text: {ex.Message}";
            LicenseTextLabel.IsVisible = true;
        }
    }
}