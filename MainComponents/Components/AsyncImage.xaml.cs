using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MainComponents.Helpers;

namespace MainComponents.Components;

/// <summary>
/// Логика взаимодействия для AsyncImage.xaml
/// </summary>
public partial class AsyncImage : Image
{
    public static readonly DependencyProperty ImageScalingModeProperty = DependencyProperty.Register(nameof(ImageScalingMode), typeof(BitmapScalingMode), typeof(AsyncImage), new PropertyMetadata(default(BitmapScalingMode)));
    

    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch),
        typeof(Stretch), typeof(AsyncImage), new PropertyMetadata(default(Stretch)));

    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource),
        typeof(string), typeof(AsyncImage), new PropertyMetadata(default(string), SourceChanged));

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public Stretch Stretch
    {
        get => (Stretch)GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }


    public BitmapScalingMode ImageScalingMode
    {
        get => (BitmapScalingMode)GetValue(ImageScalingModeProperty);
        set => SetValue(ImageScalingModeProperty, value);
    }

    public static readonly DependencyProperty RenderAtScaleProperty = DependencyProperty.Register(
        nameof(RenderAtScale), typeof(double), typeof(AsyncImage), new PropertyMetadata(default(double)));

    public double RenderAtScale
    {
        get { return (double)GetValue(RenderAtScaleProperty); }
        set { SetValue(RenderAtScaleProperty, value); }
    }


    private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        ((AsyncImage)d).SourceChanged((string)e.NewValue);

    private async void SourceChanged(string newSource)
    {
        if (string.IsNullOrEmpty(newSource)) return;
        if (!File.Exists(Path.GetFullPath(newSource)))
        {
            newSource = $"pack://application:,,,{newSource}";
        }
        var bmp = await ImageCache.LoadBitmap(
            newSource,
            Width is double.NaN ? 0 : (int)Width,
            Height is double.NaN ? 0 : (int)Height,
            ImageScalingMode);
        if (bmp is null) return;
        Source = bmp;
        if (RenderAtScale == 0) return;
        CacheMode = new BitmapCache(RenderAtScale);

    }
}