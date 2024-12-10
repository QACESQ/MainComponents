using System.Windows;
using System.Windows.Controls;

namespace MainComponents.Components;

public class RepeatingMediaElement:MediaElement
{
    public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register(
        nameof(StartTime), typeof(TimeSpan), typeof(RepeatingMediaElement), new PropertyMetadata(TimeSpan.Zero));

    public TimeSpan StartTime
    {
        get { return (TimeSpan)GetValue(StartTimeProperty); }
        set { SetValue(StartTimeProperty, value); }
    }

    public RepeatingMediaElement()=>MediaEnded += RepeatingMediaElement_MediaEnded;
    private void RepeatingMediaElement_MediaEnded(object sender, System.Windows.RoutedEventArgs e)=>Position = StartTime;
}