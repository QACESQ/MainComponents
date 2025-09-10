using MainComponents.Helpers;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MainComponents.Components;

public partial class AsyncGrayscaleImage : Image
{
    public static readonly DependencyProperty ImageScalingModeProperty = DependencyProperty.Register(nameof(ImageScalingMode),
        typeof(BitmapScalingMode), typeof(AsyncGrayscaleImage), new PropertyMetadata(default(BitmapScalingMode)));

    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch),
        typeof(Stretch), typeof(AsyncGrayscaleImage), new PropertyMetadata(default(Stretch)));

    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource),
        typeof(string), typeof(AsyncGrayscaleImage), new PropertyMetadata(default(string), SourceChanged));

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

    public static readonly DependencyProperty CacheOptionProperty = DependencyProperty.Register(
        nameof(CacheOption), typeof(BitmapCacheOption), typeof(AsyncGrayscaleImage), new PropertyMetadata(default(BitmapCacheOption)));

    public BitmapCacheOption CacheOption
    {
        get { return (BitmapCacheOption)GetValue(CacheOptionProperty); }
        set { SetValue(CacheOptionProperty, value); }
    }

    public static readonly DependencyProperty RenderAtScaleProperty = DependencyProperty.Register(
        nameof(RenderAtScale), typeof(double), typeof(AsyncGrayscaleImage), new PropertyMetadata(default(double)));

    public double RenderAtScale
    {
        get { return (double)GetValue(RenderAtScaleProperty); }
        set { SetValue(RenderAtScaleProperty, value); }
    }

    private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        ((AsyncGrayscaleImage)d).SourceChanged((string)e.NewValue);

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
            ImageScalingMode,
            CacheOption);

        if (bmp is null) return;

        var formatConvertedBitmap = new FormatConvertedBitmap();
        formatConvertedBitmap.BeginInit();
        formatConvertedBitmap.Source = bmp;
        formatConvertedBitmap.DestinationFormat = PixelFormats.Gray32Float;
        formatConvertedBitmap.EndInit();

        if (formatConvertedBitmap.CanFreeze) formatConvertedBitmap.Freeze();

        Source = formatConvertedBitmap;
        if (RenderAtScale == 0) return;
        CacheMode = new BitmapCache(RenderAtScale);
    }
}