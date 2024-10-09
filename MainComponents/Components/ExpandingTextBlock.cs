using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Size = System.Windows.Size;

namespace MainComponents.Components
{
    public class ExpandingTextBlock:TextBlock
    {
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            nameof(IsExpanded), typeof(bool), typeof(ExpandingTextBlock), new PropertyMetadata(default(bool),IsExpandedChanged));

        
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty UseAnimationProperty = DependencyProperty.Register(
            nameof(UseAnimation), typeof(bool), typeof(ExpandingTextBlock), new PropertyMetadata(true));

        public bool UseAnimation
        {
            get { return (bool)GetValue(UseAnimationProperty); }
            set { SetValue(UseAnimationProperty, value); }
        }

        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            nameof(AnimationDuration), typeof(Duration), typeof(ExpandingTextBlock), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(250))));

        public Duration AnimationDuration
        {
            get { return (Duration)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        public static readonly DependencyProperty CollapsedHeightProperty = DependencyProperty.Register(
            nameof(CollapsedHeight), typeof(double), typeof(ExpandingTextBlock), new PropertyMetadata(default(double)));

        public double CollapsedHeight
        {
            get { return (double)GetValue(CollapsedHeightProperty); }
            set { SetValue(CollapsedHeightProperty, value); }
        }
        private static void IsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is not ExpandingTextBlock control) return;
            if(control.IsExpanded) control.Expand();
            else if(control.UseAnimation) control.Collapse(true);
            else control.Collapse(false);

        }


        public ExpandingTextBlock()
        {
            Loaded += ExpandingTextBlock_Loaded;
        }

        private void ExpandingTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            Collapse(false);
        }

        private void Collapse(bool useAnimation)
        {
            TextTrimming = TextTrimming.WordEllipsis;
            var animation = new DoubleAnimation(ActualHeight, CollapsedHeight, useAnimation? AnimationDuration:TimeSpan.Zero);
            BeginAnimation(HeightProperty,animation);
        }


        private void Expand()
        {
            TextTrimming = TextTrimming.None;
            var animation = new DoubleAnimation(ActualHeight, MeasureTextSize().Height, AnimationDuration);
            BeginAnimation(HeightProperty, animation);
        }

        private Size MeasureTextSize()
        {
            var formattedText = new FormattedText(
                Text,
                CultureInfo.InvariantCulture,
                FlowDirection,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                Foreground,1.0)
            {
                MaxTextWidth = ActualWidth
            };
            return new Size(formattedText.Width, formattedText.Height);
        }

    }
}
