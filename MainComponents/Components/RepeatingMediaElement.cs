using System.Windows.Controls;

namespace MainComponents.Components;

public class RepeatingMediaElement:MediaElement
{
    public RepeatingMediaElement()=>MediaEnded += RepeatingMediaElement_MediaEnded;
    private void RepeatingMediaElement_MediaEnded(object sender, System.Windows.RoutedEventArgs e)=>Position = TimeSpan.Zero;
}