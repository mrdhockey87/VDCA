using VDCA.Models;
using VDCA.Services;
using VDCA.Utils;

namespace VDCA.Views;

public partial class LicenseInfoView : ContentPage
{
    private readonly LicenseService _licenseService;

    public LicenseInfoView()
    {
        _licenseService = new LicenseService();
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ShowProgressBarUtil.ShowProgressBar(progressBarSelectOverlay);
        await LoadLicenses();
        await ShowProgressBarUtil.HideProgressBar(progressBarSelectOverlay);
    }
    private async Task LoadLicenses()
    {
        var licenses = await _licenseService.GetLicenseInfoAsync();
        // Display licenses in a horizontal CollectionView
        LicensesCollectionView.ItemsSource = licenses;
    }
    private async void OnLicenseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is LicenseInfo selectedLicense)
        {
            var route = $"LicenseView?FileName={Uri.EscapeDataString(selectedLicense.LicenseFileName)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}