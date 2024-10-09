using System.Windows;
using UserControl = System.Windows.Controls.UserControl;


namespace MainComponents.Components;

public class RoundedUserControl:UserControl
{

    static RoundedUserControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundedUserControl),
            new FrameworkPropertyMetadata(typeof(RoundedUserControl)));
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(RoundedUserControl), new PropertyMetadata(default(CornerRadius)));

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }
}