using System.Windows;

namespace MainComponents.Components
{
    public class HeaderedPlaceholderTextBox:PlaceholderTextBox
    {
        static HeaderedPlaceholderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderedPlaceholderTextBox),
                new FrameworkPropertyMetadata(typeof(HeaderedPlaceholderTextBox)));
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header), typeof(string), typeof(HeaderedPlaceholderTextBox), new PropertyMetadata(default(string)));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }


        public static readonly DependencyProperty HeaderStyleProperty = DependencyProperty.Register(
            nameof(HeaderStyle), typeof(Style), typeof(HeaderedPlaceholderTextBox), new PropertyMetadata(default(Style)));

        public Style HeaderStyle
        {
            get { return (Style)GetValue(HeaderStyleProperty); }
            set { SetValue(HeaderStyleProperty, value); }
        }

        
        
    }
}
