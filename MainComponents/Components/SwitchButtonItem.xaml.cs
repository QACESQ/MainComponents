using System.Windows;
using System.Windows.Controls;

namespace MainComponents.Components
{
    /// <summary>
    /// Логика взаимодействия для SwitchButtonItem.xaml
    /// </summary>
    public partial class SwitchButtonItem : UserControl
    {
        
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(SwitchButtonItem), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextStyleProperty = DependencyProperty.Register(
            nameof(TextStyle), typeof(Style), typeof(SwitchButtonItem), new PropertyMetadata(default(Style)));

        public Style TextStyle
        {
            get { return (Style)GetValue(TextStyleProperty); }
            set { SetValue(TextStyleProperty, value); }
        }

        public SwitchButtonItem()
        {
            InitializeComponent();
        }

    }
}
