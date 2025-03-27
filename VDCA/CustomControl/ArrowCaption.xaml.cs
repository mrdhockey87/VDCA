using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace VDCA.CustomControl;

public partial class ArrowCaption : ContentView
{
    public static readonly BindableProperty BubbleTextProperty =
        BindableProperty.Create(nameof(BubbleText), typeof(string), typeof(ArrowCaption), string.Empty);

    public string BubbleText
    {
        get => (string)GetValue(BubbleTextProperty);
        set => SetValue(BubbleTextProperty, value);
    }
    public ArrowCaption()
	{
		InitializeComponent();
        BindingContext = this;
    }
    public void ShowBubble(string text, VisualElement targetControl)
    {
        // Set the BubbleText
        BubbleText = text;
        // Calculate the position and set the arrow
        CalculatePositionAndSetArrow(targetControl);
        // Make the ArrowCaption visible
        this.IsVisible = true;
    }
    public void SetExplaination(string explaination)
    {
        BubbleText = explaination;
    }
    public void SetVisibleImage(int topLeftImageIndex, int topMiddleImageIndex, int topRightImageIndex, int rightImageIndex,
                                int bottomLeftImageIndex, int bottomMiddleImageIndex, int bottomRightImageIndex, int leftImageIndex)
    {
        // Control visibility
        TopLeftImage.IsVisible = topLeftImageIndex == 1;
        TopMiddleImage.IsVisible = topMiddleImageIndex == 1;
        TopRightImage.IsVisible = topRightImageIndex == 1;
        RightImage.IsVisible = rightImageIndex == 1;
        LeftImage.IsVisible = leftImageIndex == 1;
        BottomLeftImage.IsVisible = bottomLeftImageIndex == 1;
        BottomMiddleImage.IsVisible = bottomMiddleImageIndex == 1;
        BottomRightImage.IsVisible = bottomRightImageIndex == 1;

        // Reset the Margin for the bubble
        BubbleFrame.Margin = new Thickness(0);

        // Adjust Bubble position based on which image is visible
        if (topLeftImageIndex == 1 || topMiddleImageIndex == 1 || topRightImageIndex == 1)
        {
            // Adjust bubble position for left image
            BubbleFrame.Margin = new Thickness(0, -20, 0, 0); // Pulls up on the bubble
        }
        else if (bottomLeftImageIndex == 1 || bottomMiddleImageIndex == 1 || bottomRightImageIndex == 1)
        {
            // Adjust bubble position for below image
            BubbleFrame.Margin = new Thickness(0, 0, 0, -20); // Pulls down on the bubble
        }
        else if (rightImageIndex == 1)
        {
            // Adjust bubble position for right image
            BubbleFrame.Margin = new Thickness(0, 0, -20, 0); // Pulls right on the bubble
        }
        else if (leftImageIndex == 1)
        {
            // Adjust bubble position for above image
            BubbleFrame.Margin = new Thickness(-20, 0, 0, 0); // Pulls left on the bubble
        }
    }

    public void CalculatePositionAndSetArrow(VisualElement targetControl)
    {
        Rect targetBounds = targetControl.Bounds;
        Rect arrowCaptionBounds = this.Bounds;

        // Calculate the position of the ArrowCaption relative to the target control
        double deltaX = arrowCaptionBounds.Center.X - targetBounds.Center.X;
        double deltaY = arrowCaptionBounds.Center.Y - targetBounds.Center.Y;

        // Determine which arrow to show based on the position
        int topLeftImageIndex = 0;
        int topMiddleImageIndex = 0;
        int topRightImageIndex = 0;
        int rightImageIndex = 0;
        int bottomLeftImageIndex = 0;
        int bottomMiddleImageIndex = 0;
        int bottomRightImageIndex = 0;
        int leftImageIndex = 0;

        if (deltaY < 0)
        {
            if (deltaX < -targetBounds.Width / 2)
            {
                topLeftImageIndex = 1;
            }
            else if (deltaX > targetBounds.Width / 2)
            {
                topRightImageIndex = 1;
            }
            else
            {
                topMiddleImageIndex = 1;
            }
        }
        else if (deltaY > 0)
        {
            if (deltaX < -targetBounds.Width / 2)
            {
                bottomLeftImageIndex = 1;
            }
            else if (deltaX > targetBounds.Width / 2)
            {
                bottomRightImageIndex = 1;
            }
            else
            {
                bottomMiddleImageIndex = 1;
            }
        }
        else
        {
            if (deltaX < 0)
            {
                leftImageIndex = 1;
            }
            else
            {
                rightImageIndex = 1;
            }
        }

        // Set the visibility of the arrows
        SetVisibleImage(topLeftImageIndex, topMiddleImageIndex, topRightImageIndex, rightImageIndex,
                        bottomLeftImageIndex, bottomMiddleImageIndex, bottomRightImageIndex, leftImageIndex);
    }
}