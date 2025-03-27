using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace VDCA.Popups;
public partial class CustomToastView : ContentView
{
    private readonly Label messageLabel; // Add a private field for the Label

    public CustomToastView(string message)
    {
        // Initialize the Label with the message
        messageLabel = new Label
        {
            Text = message,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            TextColor = Microsoft.Maui.Graphics.Colors.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
        };

        // Create a Frame as before
        var backgroundFrame = new Border
        {
            BackgroundColor = Colors.Black,
            Opacity = 0.8,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(20)
            },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 200,
            HeightRequest = 50,
            Padding = 0,
            Content = messageLabel // Use the messageLabel here
        };

        // Set the Frame as the content of the ContentView
        Content = backgroundFrame;
        HorizontalOptions = LayoutOptions.Center;
        VerticalOptions = LayoutOptions.Center;
    }

    // Public property to get or set the message
    public string Message
    {
        get => messageLabel.Text;
        set => messageLabel.Text = value;
    }
}
