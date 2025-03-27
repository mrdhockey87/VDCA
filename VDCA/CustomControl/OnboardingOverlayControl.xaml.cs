using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Drawing;

namespace VDCA.CustomControl;

public partial class OnboardingOverlayControl : ContentView
{
    public OnboardingOverlayControl()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public static readonly BindableProperty InformationTextProperty =
         BindableProperty.Create(nameof(InformationText), typeof(string), typeof(OnboardingOverlayControl), string.Empty, propertyChanged: OnInformationTextChanged);

    public string InformationText
    {
        get { return (string)GetValue(InformationTextProperty); }
        set
        {
            SetValue(InformationTextProperty, value);
            OnPropertyChanged(nameof(InformationText));
        }
    }

    public static readonly BindableProperty VisibleArrowsProperty =
        BindableProperty.Create(nameof(VisibleArrows), typeof(ObservableCollection<ArrowDirection>), typeof(OnboardingOverlayControl), new ObservableCollection<ArrowDirection>(), propertyChanged: OnVisibleArrowsChanged);

    public ObservableCollection<ArrowDirection> VisibleArrows
    {
        get { return (ObservableCollection<ArrowDirection>)GetValue(VisibleArrowsProperty); }
        set
        {
            SetValue(VisibleArrowsProperty, value);
            OnPropertyChanged(nameof(VisibleArrows));
        }
    }

    private static void OnVisibleArrowsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (OnboardingOverlayControl)bindable;
        control.OnPropertyChanged(nameof(VisibleArrows));
    }

    private static void OnInformationTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (OnboardingOverlayControl)bindable;
        control.OnPropertyChanged(nameof(InformationText));
    }

}
