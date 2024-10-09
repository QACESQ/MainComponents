using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MainComponents.Components;

public interface IScrollController
{
    void ScrollLeft();
    void ScrollRight();
}

public class AnimatedScrollViewer : ScrollViewer, IScrollController
{
    private readonly Storyboard _scrollStoryboard;

    public static readonly DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register(
        nameof(CurrentVerticalOffset), typeof(double), typeof(AnimatedScrollViewer), new PropertyMetadata(0.0, OnCurrentVerticalOffsetChanged));
    public double CurrentVerticalOffset
    {
        get { return (double)GetValue(CurrentVerticalOffsetProperty); }
        set { SetValue(CurrentVerticalOffsetProperty, value); }
    }


    public static readonly DependencyProperty TargetVerticalOffsetProperty = DependencyProperty.Register(
        nameof(TargetVerticalOffset), typeof(double), typeof(AnimatedScrollViewer), new PropertyMetadata(0.0, OnTargetVerticalOffsetChanged));
    public double TargetVerticalOffset
    {
        get { return (double)GetValue(TargetVerticalOffsetProperty); }
        set { SetValue(TargetVerticalOffsetProperty, value); }
    }


    public static readonly DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register(
        nameof(CurrentHorizontalOffset), typeof(double), typeof(AnimatedScrollViewer), new PropertyMetadata(0.0, OnCurrentHorizontalOffsetChanged));
    public double CurrentHorizontalOffset
    {
        get => (double)GetValue(CurrentHorizontalOffsetProperty);
        set => SetValue(CurrentHorizontalOffsetProperty, value);
    }


    public static readonly DependencyProperty TargetHorizontalOffsetProperty = DependencyProperty.Register(
        nameof(TargetHorizontalOffset), typeof(double), typeof(AnimatedScrollViewer), new PropertyMetadata(0.0, OnTargetHorizontalOffsetChanged));
    public double TargetHorizontalOffset
    {
        get => (double)GetValue(TargetHorizontalOffsetProperty);
        set => SetValue(TargetHorizontalOffsetProperty, value);
    }

    public IScrollController Controller
    {
        get => (IScrollController)GetValue(ControllerProperty);
        private set => SetValue(ControllerProperty, value);
    }



    public static readonly DependencyProperty ControllerProperty =
        DependencyProperty.Register("Controller", typeof(IScrollController), typeof(AnimatedScrollViewer));


    public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
        nameof(AnimationDuration), typeof(Duration), typeof(AnimatedScrollViewer), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5))));

    public Duration AnimationDuration
    {
        get => (Duration)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    private static void OnCurrentVerticalOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var viewer = sender as AnimatedScrollViewer;
        viewer?.ScrollToVerticalOffset((double)e.NewValue);
    }

    private static void OnTargetVerticalOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is not AnimatedScrollViewer viewer || e.NewValue == e.OldValue)
            return;

        viewer.VerticalScroll((double)e.NewValue);
    }

    private static void OnCurrentHorizontalOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var viewer = sender as AnimatedScrollViewer;
        viewer?.ScrollToHorizontalOffset((double)e.NewValue);
    }

    private static void OnTargetHorizontalOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is not AnimatedScrollViewer viewer || e.NewValue == e.OldValue)
            return;

        viewer.HorizontalScroll((double)e.NewValue);
    }



    static AnimatedScrollViewer(){ }

    public AnimatedScrollViewer()
    {
        Controller = this;
        _scrollStoryboard = new Storyboard();

        PreviewTouchDown += (_, _) =>
        {
            _scrollStoryboard.Stop();
            _scrollStoryboard.Children.Clear();
            CurrentHorizontalOffset = HorizontalOffset;
            CurrentVerticalOffset = VerticalOffset;
        };
    }

    public void HorizontalScroll(double horizontalOffset)=> Scroll(HorizontalOffset, horizontalOffset,new PropertyPath(CurrentHorizontalOffsetProperty));

    public void VerticalScroll(double verticalOffset) => Scroll(VerticalOffset,verticalOffset,new PropertyPath(CurrentVerticalOffsetProperty));

    private void Scroll(double from, double to,PropertyPath targetProperty)
    {
        _scrollStoryboard.Stop();
        _scrollStoryboard.Children.Clear();

        var animation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = AnimationDuration,
            AccelerationRatio = 0.3,
            DecelerationRatio = 0.7
        };

        Storyboard.SetTarget(animation, this);
        Storyboard.SetTargetProperty(animation, targetProperty);

        _scrollStoryboard.Children.Add(animation);
        _scrollStoryboard.Begin();
    }

    public void ScrollLeft()
    {
        const double minValue = 0.0;
        var newValue = TargetHorizontalOffset - ActualWidth;

        TargetHorizontalOffset = Math.Max(minValue, newValue);
    }

    public void ScrollRight()
    {
        if (Content is not FrameworkElement content)
            return;

        var maxValue = content.ActualWidth - ActualWidth;
        var newValue = TargetHorizontalOffset + ActualWidth;

        TargetHorizontalOffset = Math.Min(maxValue, newValue);
    }
}