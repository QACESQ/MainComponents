using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ListBox = System.Windows.Controls.ListBox;
using Size = System.Windows.Size;

namespace MainComponents.Components
{
    [TemplatePart(Name = "PART_SwitchBorder",Type=typeof(Border))]
    public class MultiSwitchButton:ListBox
    {
        private Border? _switchBorder;
        private int _oldIndex;

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius), typeof(CornerRadius), typeof(MultiSwitchButton), new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            nameof(AnimationDuration), typeof(Duration), typeof(MultiSwitchButton), new PropertyMetadata(new Duration(TimeSpan.Zero)));

        public Duration AnimationDuration
        {
            get { return (Duration)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        static MultiSwitchButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiSwitchButton),
                new FrameworkPropertyMetadata(typeof(MultiSwitchButton)));
        }

        public MultiSwitchButton()
        {
            Loaded += MultiSwitchButton_Loaded;
            
        }

        public override void OnApplyTemplate()
        {
            var border = Template.FindName("PART_SwitchBorder", this);
            if (border is Border b)
                _switchBorder =  b;
            base.OnApplyTemplate();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            
            var bounds = base.ArrangeOverride(arrangeBounds);
            BoundAnimation(AnimationDuration);
            return bounds;
        }

        private void MultiSwitchButton_Loaded(object sender, RoutedEventArgs e)
        {
            var zeroDuration = new Duration(TimeSpan.Zero);
            BoundAnimation(zeroDuration);
            TranslateAnimation(zeroDuration);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            TranslateAnimation(AnimationDuration);
            BoundAnimation(AnimationDuration);
            _oldIndex = SelectedIndex;
        }

        private void BoundAnimation(Duration duration)
        {
            if(_switchBorder == null) return;
            var animBounds = new DoubleAnimation(_switchBorder.ActualWidth, GetContainer(SelectedIndex)?.ActualWidth ?? 0.0,
                duration);
            _switchBorder.BeginAnimation(WidthProperty, animBounds);
        }

        private void TranslateAnimation(Duration duration)
        {
            if(_switchBorder is null) return;
            var xOffset = GetOffset(SelectedIndex);
            var startOffset = GetOffset(_oldIndex);
            
            var animTranslate = new DoubleAnimation(startOffset, xOffset, duration);
            var translateTransform = new TranslateTransform();
            _switchBorder.RenderTransform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.XProperty, animTranslate);
        }

        private double GetOffset(int index)
        {
            double offset = 0;
            for (var i = 0; i < index; i++)
            {
                var container = GetContainer(i);
                offset += container?.ActualWidth ?? 0.0;
            }
            return offset;
        }

        private FrameworkElement? GetContainer(int index)
        {
            if(index<0 ||  index>=Items.Count) return null;
            var container = ItemContainerGenerator.ContainerFromIndex(index);
            return container is not FrameworkElement element ? null : element;
        }
    }
}
