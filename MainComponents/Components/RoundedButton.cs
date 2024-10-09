using System.Windows;
using Button=System.Windows.Controls;

namespace MainComponents.Components;

public class RoundedButton:Button.Button
{
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(RoundedButton), new PropertyMetadata(default(CornerRadius)));

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    static RoundedButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundedButton),
            new FrameworkPropertyMetadata(typeof(RoundedButton)));
    }
}