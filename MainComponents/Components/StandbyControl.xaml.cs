using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using MainComponents.Models.Enums;
using MainComponents.Models.Interfaces;

namespace MainComponents.Components;

public partial class StandbyControl : UserControl
{
    #region Properties
    public List<IStandbyModel> Standbies
    {
        get => (List<IStandbyModel>)GetValue(StandbiesProperty);
        set => SetValue(StandbiesProperty, value);
    }
    public Uri? Media1
    {
        get => (Uri?)GetValue(Media1Property);
        set => SetValue(Media1Property, value);
    }
    public Uri? Media2
    {
        get => (Uri?)GetValue(Media2Property);
        set => SetValue(Media2Property, value);
    }
    public bool SwitchMedia
    {
        get => (bool)GetValue(SwitchMediaProperty);
        set => SetValue(SwitchMediaProperty, value);
    }
    public int UniversalDuration
    {
        get => (int)GetValue(UniversalDurationProperty);
        set => SetValue(UniversalDurationProperty, value);
    }
    public bool UseDurationToVideo
    {
        get => (bool)GetValue(UseDurationToVideoProperty);
        set => SetValue(UseDurationToVideoProperty, value);
    }
    public StandbySwitchAnimType AnimType
    {
        get => (StandbySwitchAnimType)GetValue(AnimTypeProperty);
        set => SetValue(AnimTypeProperty, value);
    }
    private readonly DispatcherTimer _timer = new(DispatcherPriority.Normal){Interval = TimeSpan.FromSeconds(1)};
    private int _currentIndex = -1;
    private int _sec = 0;
    #endregion

    #region Fields
    public static readonly DependencyProperty StandbiesProperty = DependencyProperty.Register(
        nameof(Standbies), typeof(List<IStandbyModel>), typeof(StandbyControl), new PropertyMetadata(new List<IStandbyModel>(),PropertyChangedCallback));
    public static readonly DependencyProperty Media1Property = DependencyProperty.Register(
        nameof(Media1), typeof(Uri), typeof(StandbyControl), new PropertyMetadata(null));
    public static readonly DependencyProperty Media2Property = DependencyProperty.Register(
        nameof(Media2), typeof(Uri), typeof(StandbyControl), new PropertyMetadata(null));
    public static readonly DependencyProperty SwitchMediaProperty = DependencyProperty.Register(
        nameof(SwitchMedia), typeof(bool), typeof(StandbyControl), new PropertyMetadata(false));
    public static readonly DependencyProperty UniversalDurationProperty = DependencyProperty.Register(
        nameof(UniversalDuration), typeof(int), typeof(StandbyControl), new PropertyMetadata(5));
    public static readonly DependencyProperty UseDurationToVideoProperty = DependencyProperty.Register(
        nameof(UseDurationToVideo), typeof(bool), typeof(StandbyControl), new PropertyMetadata(false));
    public static readonly DependencyProperty AnimTypeProperty = DependencyProperty.Register(
        nameof(AnimType), typeof(StandbySwitchAnimType), typeof(StandbyControl), new PropertyMetadata(default(StandbySwitchAnimType)));
    #endregion

    #region PropertyChangedCallbacks
    private static async void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        try
        {
            var control = (StandbyControl)d;
            var standbies = (List<IStandbyModel>)e.NewValue;
            if (standbies.Count != 0)
                await control.MediaLoaded();
        }
        catch
        {
            //ignored
        }
    } 
    #endregion

    #region Constructor
    public StandbyControl()
    {
        InitializeComponent();
        _timer.Tick += TimerOnTick;
    }
    #endregion

    #region Methods
    private async Task MediaLoaded()
    {
        switch (AnimType)
        {
            case StandbySwitchAnimType.Fade:
                MediaElement1.Opacity = 0;
                break;
            case StandbySwitchAnimType.Translate:
                MediaElement1.RenderTransform = new TranslateTransform { X = 1080 };
                MediaElement2.RenderTransform = new TranslateTransform { X = 0 };
                break;
            case StandbySwitchAnimType.Scale:
                MediaElement1.Opacity = 0;
                MediaElement1.RenderTransform = new ScaleTransform(1.2,1.2);
                MediaElement1.RenderTransformOrigin = new Point(0.5, 0.5);
                MediaElement2.RenderTransform = new ScaleTransform(1,1);
                MediaElement2.RenderTransformOrigin = new Point(0.5, 0.5);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        if (Standbies.Any(media => !media.Path.EndsWith(".mp4")))
        {
            Media2 = new Uri(Standbies.Last(media => !media.Path.EndsWith(".mp4")).Path,UriKind.Absolute);
            await Task.Delay(500);//Anim
        }
        await SwitchContent();
    }
    private void StandbyControlOnUnloaded(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
        _timer.Tick -= TimerOnTick;
        Media1 = null;
        Media2 = null;
    }

    private async Task SwitchContent()
    {
        _timer.Stop();
        _currentIndex++;
        if (_currentIndex > Standbies.Count - 1) _currentIndex = 0;
        SwitchMedia = !SwitchMedia;
        if (SwitchMedia)
        {
            Media1 = new Uri(Standbies[_currentIndex].Path, UriKind.Absolute);
            await AnimSwitch();//Anim
            Media2 = null;
        }
        else
        {
            Media2 = new Uri(Standbies[_currentIndex].Path, UriKind.Absolute);
            await AnimSwitch();//Anim
            Media1 = null;
        }
        if (!Standbies[_currentIndex].Path.EndsWith(".mp4") || UseDurationToVideo)
            _timer.Start();
    }
    private async Task AnimSwitch()
    {
        switch (AnimType)
        {
            case StandbySwitchAnimType.Fade:
                await AnimFadeSwitch();
                break;
            case StandbySwitchAnimType.Translate:
                await AnimTranslateSwitch();
                break;
            case StandbySwitchAnimType.Scale:
                await AnimScaleSwitch();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private async Task AnimFadeSwitch()
    {
        var storyboard1 = new Storyboard();
        var anim1 = new DoubleAnimation
        {
            To = 1,
            BeginTime = TimeSpan.FromMilliseconds(200),
            Duration = TimeSpan.FromMilliseconds(500),
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseInOut }
        };
        Storyboard.SetTargetProperty(anim1, new PropertyPath(OpacityProperty));
        storyboard1.Children.Add(anim1);

        var storyboard2 = new Storyboard();
        var anim2 = new DoubleAnimation
        {
            To = 0,
            BeginTime = TimeSpan.FromMilliseconds(500),
            Duration = TimeSpan.FromMilliseconds(500),
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseInOut }
        };
        Storyboard.SetTargetProperty(anim2, new PropertyPath(OpacityProperty));
        storyboard2.Children.Add(anim2);

        if (SwitchMedia)
        {
            MediaElement1.BeginStoryboard(storyboard1);
            MediaElement2.BeginStoryboard(storyboard2);
        }
        else
        {
            MediaElement1.BeginStoryboard(storyboard2);
            MediaElement2.BeginStoryboard(storyboard1);
        }
        await Task.Delay(1000);
    }
    private async Task AnimTranslateSwitch()
    {
        var storyboard1 = new Storyboard();
        var anim1 = new DoubleAnimation
        {
            To = 0,
            BeginTime = TimeSpan.FromMilliseconds(200),
            Duration = TimeSpan.FromMilliseconds(500),
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseInOut }
        };
        Storyboard.SetTargetProperty(anim1, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
        storyboard1.Children.Add(anim1);

        var storyboard2 = new Storyboard();
        var anim21 = new DoubleAnimation
        {
            To = -this.ActualWidth,
            BeginTime = TimeSpan.FromMilliseconds(200),
            Duration = TimeSpan.FromMilliseconds(500),
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseInOut }
        }; 
        Storyboard.SetTargetProperty(anim21, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
        var anim22 = new DoubleAnimation
        {
            To = this.ActualWidth,
            BeginTime = TimeSpan.FromMilliseconds(700),
            Duration = TimeSpan.Zero
        };
        Storyboard.SetTargetProperty(anim22, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
        storyboard2.Children.Add(anim21);
        storyboard2.Children.Add(anim22);

        if (SwitchMedia)
        {
            MediaElement1.BeginStoryboard(storyboard1);
            MediaElement2.BeginStoryboard(storyboard2);
        }
        else
        {
            MediaElement1.BeginStoryboard(storyboard2);
            MediaElement2.BeginStoryboard(storyboard1);
        }
        await Task.Delay(700);
    }
    private async Task AnimScaleSwitch()
    {
        var storyboard1 = new Storyboard();
        var anim11 = new DoubleAnimation
        {
            To = 1,
            BeginTime = TimeSpan.FromMilliseconds(200),
            Duration = TimeSpan.FromMilliseconds(500),
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseInOut }
        };
        Storyboard.SetTargetProperty(anim11, new PropertyPath("RenderTransform.(ScaleTransform.ScaleX)"));
        var anim12 = new DoubleAnimation
        {
            To = 1,
            BeginTime = TimeSpan.FromMilliseconds(200),
            Duration = TimeSpan.FromMilliseconds(500),
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseInOut }
        };
        Storyboard.SetTargetProperty(anim12, new PropertyPath("RenderTransform.(ScaleTransform.ScaleY)"));
        var anim13 = new DoubleAnimation
        {
            To = 1,
            BeginTime = TimeSpan.FromMilliseconds(200),
            Duration = TimeSpan.FromMilliseconds(500),
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseInOut }
        };
        Storyboard.SetTargetProperty(anim13, new PropertyPath(OpacityProperty));
        storyboard1.Children.Add(anim11);
        storyboard1.Children.Add(anim12);
        storyboard1.Children.Add(anim13);

        var storyboard2 = new Storyboard();
        var anim21 = new DoubleAnimation
        {
            To = 1.2,
            BeginTime = TimeSpan.FromMilliseconds(700),
            Duration = TimeSpan.Zero
        };
        Storyboard.SetTargetProperty(anim21, new PropertyPath("RenderTransform.(ScaleTransform.ScaleX)"));
        var anim22 = new DoubleAnimation
        {
            To = 1.2,
            BeginTime = TimeSpan.FromMilliseconds(700),
            Duration = TimeSpan.Zero
        };
        Storyboard.SetTargetProperty(anim22, new PropertyPath("RenderTransform.(ScaleTransform.ScaleY)"));
        var anim23 = new DoubleAnimation
        {
            To = 0,
            BeginTime = TimeSpan.FromMilliseconds(700),
            Duration = TimeSpan.Zero
        };
        Storyboard.SetTargetProperty(anim23, new PropertyPath(OpacityProperty));
        storyboard2.Children.Add(anim21);
        storyboard2.Children.Add(anim22);
        storyboard2.Children.Add(anim23);

        if (SwitchMedia)
        {
            MediaElement1.BeginStoryboard(storyboard1);
            Panel.SetZIndex(MediaElement1, 10);

            MediaElement2.BeginStoryboard(storyboard2);
            Panel.SetZIndex(MediaElement2, 1);
        }
        else
        {
            MediaElement1.BeginStoryboard(storyboard2);
            Panel.SetZIndex(MediaElement1, 1);

            MediaElement2.BeginStoryboard(storyboard1);
            Panel.SetZIndex(MediaElement2, 10);
        }
        await Task.Delay(700);
    }

    private async void TimerOnTick(object? sender, EventArgs e)
    {
        try
        {
            _sec++;
            if ((Standbies[_currentIndex].Duration == 0 ? UniversalDuration : Standbies[_currentIndex].Duration) >= _sec) return;
            _sec = 0;
            await SwitchContent();
        }
        catch
        {
            // ignored
        }
    }
    private async void OnMediaEnded(object sender, RoutedEventArgs e)
    {
        try
        {
            await SwitchContent();
        }
        catch
        {
            //ignored
        }
    }
    #endregion


}