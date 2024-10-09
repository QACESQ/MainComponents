using System.Windows;
using System.Windows.Controls;
using Size = System.Windows.Size;
using Orientation = System.Windows.Controls.Orientation;

namespace MainComponents.Panels
{
    public class OverlappingWrapPanel:WrapPanel
    {
        public static readonly DependencyProperty ItemInitialMarginProperty = DependencyProperty.Register(
            nameof(ItemInitialMargin), typeof(double), typeof(OverlappingWrapPanel), new PropertyMetadata(default(double)));

        public double ItemInitialMargin
        {
            get { return (double)GetValue(ItemInitialMarginProperty); }
            set { SetValue(ItemInitialMarginProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            foreach (UIElement uiElement in Children)
            {
                uiElement.Measure(constraint);
            }
            return Orientation == Orientation.Horizontal ? MeasureHorizontal(constraint) : MeasureVertical(constraint);
        }

        private Size MeasureVertical(Size constraint)
        {
            if(Children.Count == 0) return new Size(constraint.Width, 0);
            var result = new Size
            {
                Height = constraint.Height,
                Width = Children[0].DesiredSize.Width
            };
            return result;
        }

        private Size MeasureHorizontal(Size constraint)
        {
            if (Children.Count == 0) return new Size(0, constraint.Height);
            var result = new Size
            {
                Width = constraint.Width,
                Height = Children[0].DesiredSize.Height
            };
            return result;
        }

        private Size GetAllChildrenSize()
        {
            var size = new Size();
            foreach (UIElement uiElement in Children)
            {
                size.Width+= uiElement.DesiredSize.Width;
                size.Height+= uiElement.DesiredSize.Height;
            }

            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0;
            double y = 0;
            double margin;
            if (Orientation == Orientation.Horizontal)
                margin = (finalSize.Width - GetAllChildrenSize().Width) / (Children.Count - 1);
            else
                margin = (finalSize.Height - GetAllChildrenSize().Height) / (Children.Count - 1);
            if (margin > ItemInitialMargin) margin = ItemInitialMargin;
            foreach (UIElement uiElement in Children)
            {
                uiElement.Arrange(new Rect(x, y, uiElement.DesiredSize.Width, uiElement.DesiredSize.Height));
                if (Orientation == Orientation.Horizontal)
                    x += uiElement.DesiredSize.Width + margin;
                else
                    y += uiElement.DesiredSize.Height + margin;
            }
            return finalSize;
        }
    }
}
