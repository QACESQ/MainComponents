using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MainComponents.Helpers;

public static class ImageCache
{

    private static readonly Dictionary<BitmapImage, Image> Images = [];
    private static readonly Dictionary<string, BitmapImage> BitmapImages = [];

    public static void Clear()
    {
        Images.Clear();
        BitmapImages.Clear();
    }

    public static async Task<BitmapImage?> LoadBitmap(string path, int width, int height, BitmapScalingMode scalingMode)
    {
        if (BitmapImages.TryGetValue(path, out var image)) return image;

        var bmp = await Task.Run(
            () =>
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.DecodePixelWidth = width;
                    bitmap.DecodePixelHeight = height;
                    bitmap.CacheOption = BitmapCacheOption.None;
                    bitmap.UriSource = new Uri(path);
                    RenderOptions.SetBitmapScalingMode(bitmap, scalingMode);
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return bitmap;
                }
                catch
                {
                    return null;
                }

            });

        if (bmp is null) return null;
        BitmapImages.TryAdd(path, bmp);
        return bmp;
    }

    public static Image? LoadImage(BitmapImage bmp, Stretch stretch, double scale)
    {
        if (Images.TryGetValue(bmp, out var image)) return image;

        var newImage = new Image
        {
            Source = bmp,
            Stretch = stretch,
            CacheMode = new BitmapCache(scale)
        };
        
        Images.TryAdd(bmp, newImage);
        return newImage;
    }
}
