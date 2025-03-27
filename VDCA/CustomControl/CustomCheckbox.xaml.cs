using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.ComponentModel;

namespace VDCA.CustomControl;


public partial class CustomCheckbox : ContentView, INotifyPropertyChanged
{
    public static readonly BindableProperty IsCheckedProperty =
        BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CustomCheckbox), false, propertyChanged: OnIsCheckedChanged);

    public bool IsChecked
    {
        get { return (bool)GetValue(IsCheckedProperty); }
        set
        {
            SetValue(IsCheckedProperty, value);
            OnPropertyChanged(nameof(IsChecked));
        }
    }

    public static readonly BindableProperty ColorProperty =
        BindableProperty.Create(nameof(Color), typeof(Color), typeof(CustomCheckbox), default(Color), propertyChanged: OnColorChanged);

    public Color Color
    {
        get { return (Color)GetValue(ColorProperty); }
        set
        {
            SetValue(ColorProperty, value);
            OnPropertyChanged(nameof(Color));
        }
    }



    public static readonly BindableProperty SizeProperty =
        BindableProperty.Create(nameof(Size), typeof(double), typeof(CustomCheckbox), 20.0, propertyChanged: OnSizeChanged);

    public double Size
    {
        get { return (double)GetValue(SizeProperty); }
        set
        {
            SetValue(SizeProperty, value);
            OnPropertyChanged(nameof(Size));
        }
    }

    public CustomCheckbox()
    {
        InitializeComponent();
    }

    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomCheckbox)bindable;
        control.OnPropertyChanged(nameof(IsChecked));
    }

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomCheckbox)bindable;
        control.OnPropertyChanged(nameof(Color));
    }

    private static void OnSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomCheckbox)bindable;
        control.OnPropertyChanged(nameof(Size));
    }
}
