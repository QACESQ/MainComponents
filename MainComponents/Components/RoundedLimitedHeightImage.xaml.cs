using System.Windows;
using System.Windows.Media;
using Size = System.Windows.Size;

namespace MainComponents.Components;

/// <summary>
/// Логика взаимодействия для RoundedLimitedHeightImage.xaml
/// </summary>
public partial class RoundedLimitedHeightImage : RoundedUserControl
{
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
        nameof(ImageSource), typeof(Uri), typeof(RoundedLimitedHeightImage), new PropertyMetadata(default(Uri)));


    public Uri ImageSource
    {
        get { return (Uri)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }

    public RoundedLimitedHeightImage()
    {
        InitializeComponent();
        SizeChanged += RoundedImage_SizeChanged;
    }

    private void RoundedImage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (Clip is not RectangleGeometry geometry) return;
        geometry.Rect = new Rect(new Size(ActualWidth, ActualHeight));
    }
}