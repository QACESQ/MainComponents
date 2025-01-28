using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MainComponents.Popups.PopupContainers
{
    public class ScaleContainer : BasePopupContainer
    {
        protected override void ContainerLoaded(object sender, RoutedEventArgs e)
        {
            RenderTransform = new ScaleTransform(0.5, 0.5, ActualWidth / 2, ActualHeight / 2);
            var xAnim = new DoubleAnimation(1, Duration)
            {
                EasingFunction = EasingFunction
            };
            var yAnim = new DoubleAnimation(1, Duration)
            {
                EasingFunction = EasingFunction
            };
            var oAnim = new DoubleAnimation(1, Duration)
            {
                EasingFunction = EasingFunction
            };

            StartAnimation(xAnim,yAnim,oAnim);
        }

        public override void Close()
        {
            var xAnim = new DoubleAnimation(0.5, Duration)
            {
                EasingFunction = EasingFunction
            };
            var yAnim = new DoubleAnimation(0.5, Duration)
            {
                EasingFunction = EasingFunction
            };
            var oAnim = new DoubleAnimation(0.8, Duration)
            {
                EasingFunction = EasingFunction
            };

            
            xAnim.Completed += (o, args) => RaiseEvent(new RoutedEventArgs(ClosedEvent));
            StartAnimation(xAnim,yAnim,oAnim);
        }

        protected override void StartAnimation(params Timeline[] animations)
        {
            var sb = new Storyboard();
            foreach (var animation in animations)
            {
                sb.Children.Add(animation);
                Storyboard.SetTarget(animation, this);
            }

            Storyboard.SetTargetProperty(animations[0],
                new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(animations[1],
                new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            Storyboard.SetTargetProperty(animations[2], new PropertyPath("(UIElement.Opacity)"));

            sb.Begin();
        }
    }
}