using System.Windows;
using Brush = System.Windows.Media.Brush;
using TextBox = System.Windows.Controls.TextBox;

namespace MainComponents.Components;

public class PlaceholderTextBox:TextBox
{
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(PlaceholderTextBox), new PropertyMetadata(default(CornerRadius)));

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(default(string)));

    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    public static readonly DependencyProperty PlaceholderBrushProperty = DependencyProperty.Register(
        nameof(PlaceholderBrush), typeof(Brush), typeof(PlaceholderTextBox), new PropertyMetadata(default(Brush)));

    public Brush PlaceholderBrush
    {
        get { return (Brush)GetValue(PlaceholderBrushProperty); }
        set { SetValue(PlaceholderBrushProperty, value); }
    }

    static PlaceholderTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox),
            new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
    }
}