using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MainComponents.Models;

namespace MainComponents.Components
{
    /// <summary>
    /// Логика взаимодействия для Keyboard.xaml
    /// </summary>
    public partial class Keyboard : UserControl
    {
        #region Properties

        public new double Width
        {
            get => (double)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public new double Height
        {
            get => (double)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public Thickness ButtonBaseMargin
        {
            get => (Thickness)GetValue(ButtonBaseMarginProperty);
            set => SetValue(ButtonBaseMarginProperty, value);
        }

        public double ButtonBaseWidth
        {
            get => (double)GetValue(ButtonBaseWidthProperty);
            set => SetValue(ButtonBaseWidthProperty, value);
        }

        public double ButtonBaseHeight
        {
            get => (double)GetValue(ButtonBaseHeightProperty);
            set => SetValue(ButtonBaseHeightProperty, value);
        }

        public List<List<KeyRecord>> ShowedKeys
        {
            get => (List<List<KeyRecord>>)GetValue(ShowedKeysProperty);
            set => SetValue(ShowedKeysProperty, value);
        }

        public LanguageType CurrentLanguage
        {
            get => (LanguageType)GetValue(CurrentLanguageProperty);
            set => SetValue(CurrentLanguageProperty, value);
        }

        public bool ShiftPressed
        {
            get => (bool)GetValue(ShiftPressedProperty);
            set => SetValue(ShiftPressedProperty, value);
        }

        public bool IsDigit
        {
            get => (bool)GetValue(IsDigitProperty);
            set => SetValue(IsDigitProperty, value);
        }

        public bool IsEmailPrinting
        {
            get => (bool)GetValue(IsEmailPrintingProperty);
            set => SetValue(IsEmailPrintingProperty, value);
        }
        
        public List<KeyRecord> ShowedAdditionalKeys
        {
            get => (List<KeyRecord>)GetValue(ShowedAdditionalKeysProperty);
            set => SetValue(ShowedAdditionalKeysProperty, value);
        }

        public Style MainButtonStyle
        {
            get => (Style)GetValue(MainButtonStyleProperty);
            set => SetValue(MainButtonStyleProperty, value);
        }

        public Style AdditionalButtonStyle
        {
            get => (Style)GetValue(AdditionalButtonStyleProperty);
            set => SetValue(AdditionalButtonStyleProperty, value);
        }

        public Style EmailButtonStyle
        {
            get => (Style)GetValue(EmailButtonStyleProperty);
            set => SetValue(EmailButtonStyleProperty, value);
        }

        public List<List<KeyRecord>> FirstLangKeys
        {
            get => (List<List<KeyRecord>>)GetValue(FirstLangKeysProperty);
            set => SetValue(FirstLangKeysProperty, value);
        }

        public List<List<KeyRecord>> SecondLangKeys
        {
            get => (List<List<KeyRecord>>)GetValue(SecondLangKeysProperty);
            set => SetValue(SecondLangKeysProperty, value);
        }

        public List<List<KeyRecord>> FirstSpecKeys
        {
            get => (List<List<KeyRecord>>)GetValue(FirstSpecKeysProperty);
            set => SetValue(FirstSpecKeysProperty, value);
        }

        public List<List<KeyRecord>> EmailKeys
        {
            get => (List<List<KeyRecord>>)GetValue(EmailKeysProperty);
            set => SetValue(EmailKeysProperty, value);
        }

        public List<KeyRecord> AdditionalKeys
        {
            get => (List<KeyRecord>)GetValue(AdditionalKeysProperty);
            set => SetValue(AdditionalKeysProperty, value);
        }

        #endregion
        #region Fields

        public new static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            nameof(Width), typeof(double), typeof(Keyboard), new PropertyMetadata(0.0, WidthChanged));

        public new static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
            nameof(Height), typeof(double), typeof(Keyboard), new PropertyMetadata(0.0, HeightChanged));

        public static readonly DependencyProperty ButtonBaseMarginProperty = DependencyProperty.Register(
            nameof(ButtonBaseMargin), typeof(Thickness), typeof(Keyboard),
            new PropertyMetadata(new Thickness(0), ButtonBaseMarginChanged));

        public static readonly DependencyProperty ButtonBaseWidthProperty = DependencyProperty.Register(
            nameof(ButtonBaseWidth), typeof(double), typeof(Keyboard), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ButtonBaseHeightProperty = DependencyProperty.Register(
            nameof(ButtonBaseHeight), typeof(double), typeof(Keyboard), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ShowedKeysProperty = DependencyProperty.Register(
            nameof(ShowedKeys), typeof(List<List<KeyRecord>>), typeof(Keyboard),
            new PropertyMetadata(new List<List<KeyRecord>>(),KeysChanged));

        public static readonly DependencyProperty CurrentLanguageProperty = DependencyProperty.Register(
            nameof(CurrentLanguage), typeof(LanguageType), typeof(Keyboard), new PropertyMetadata(LanguageType.Ru,CurrentLanguageChanged));

        public static readonly DependencyProperty ShiftPressedProperty = DependencyProperty.Register(
            nameof(ShiftPressed), typeof(bool), typeof(Keyboard), new PropertyMetadata(false));

        public static readonly DependencyProperty IsDigitProperty = DependencyProperty.Register(
            nameof(IsDigit), typeof(bool), typeof(Keyboard), new PropertyMetadata(false,IsDigitChanged));

        public static readonly DependencyProperty IsEmailPrintingProperty = DependencyProperty.Register(
            nameof(IsEmailPrinting), typeof(bool), typeof(Keyboard), new PropertyMetadata(false,IsEmailPrintingChanged));

        public static readonly DependencyProperty ShowedAdditionalKeysProperty = DependencyProperty.Register(
            nameof(ShowedAdditionalKeys), typeof(List<KeyRecord>), typeof(Keyboard), new PropertyMetadata(default(List<KeyRecord>)));

        public static readonly DependencyProperty MainButtonStyleProperty = DependencyProperty.Register(
            nameof(MainButtonStyle), typeof(Style), typeof(Keyboard), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty AdditionalButtonStyleProperty = DependencyProperty.Register(
            nameof(AdditionalButtonStyle), typeof(Style), typeof(Keyboard), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty EmailButtonStyleProperty = DependencyProperty.Register(
            nameof(EmailButtonStyle), typeof(Style), typeof(Keyboard), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty FirstLangKeysProperty = DependencyProperty.Register(
            nameof(FirstLangKeys), typeof(List<List<KeyRecord>>), typeof(Keyboard), new PropertyMetadata(SetRuSymbols()));

        public static readonly DependencyProperty SecondLangKeysProperty = DependencyProperty.Register(
            nameof(SecondLangKeys), typeof(List<List<KeyRecord>>), typeof(Keyboard), new PropertyMetadata(SetEngSymbols()));

        public static readonly DependencyProperty FirstSpecKeysProperty = DependencyProperty.Register(
            nameof(FirstSpecKeys), typeof(List<List<KeyRecord>>), typeof(Keyboard), new PropertyMetadata(SetFirstSpecSymbols()));

        public static readonly DependencyProperty EmailKeysProperty = DependencyProperty.Register(
            nameof(EmailKeys), typeof(List<List<KeyRecord>>), typeof(Keyboard), new PropertyMetadata(SetEmailSymbols()));

        public static readonly DependencyProperty AdditionalKeysProperty = DependencyProperty.Register(
            nameof(AdditionalKeys), typeof(List<KeyRecord>), typeof(Keyboard), new PropertyMetadata(SetAdditionalSymbols()));

        #endregion

        #region PropertyChangedCallbacks

        private static void WidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;
            control.SetButtonBaseWidth();
        }

        private static void HeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;
            control.SetButtonBaseHeight();
        }

        private static void ButtonBaseMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;
            control.SetButtonBaseWidth();
            control.SetButtonBaseHeight();
        }

        private static void KeysChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;
            control.SetButtonBaseWidth();
        }

        private static void CurrentLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;
            control.ShowedKeys = control.GetSymbols();
        }

        private static void IsDigitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;
            control.ShowedKeys = control.GetSymbols();
        }

        private static void IsEmailPrintingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;

            control.ShowedAdditionalKeys = (bool)e.NewValue ? control.AdditionalKeys : [];
            control.SetButtonBaseHeight();
        }

        #endregion

        #region Constructor

        public Keyboard()
        {
            InitializeComponent();

            ShowedKeys = GetSymbols();
        }
        #endregion

        #region Methods

        private void PrintEmail(object sender, RoutedEventArgs e)
        {
            if (sender is not ButtonBase button) return;
            if (button.Tag is not EmailTypes emailType) return;

            foreach (var keyRecord in GetEmailKeys(emailType))
            {
                keyRecord.PrintKeyWithParams(ShiftPressed);
            }
        }

        private void PrintKey(object sender, RoutedEventArgs e)
        {
            if (sender is not ButtonBase button) return;
            switch (button.Tag)
            {
                case KeyRecord keyRecord:
                    keyRecord.PrintKeyWithParams(ShiftPressed);
                    break;
                case Key key:
                    Utilities.Keyboard.Type(key);
                    break;
            }
        }

        private void PressShift(object sender, RoutedEventArgs e)
        {
            ShiftPressed = !ShiftPressed;
        }

        private async void DeleteMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var count = 20;
                while (e.ButtonState == MouseButtonState.Pressed)
                {
                    Utilities.Keyboard.Type(Key.Back);
                    for (var i = 0; i < count; i++)
                    {
                        await Task.Delay(25);
                        if (e.ButtonState != MouseButtonState.Pressed) return;
                    }

                    if (count <= 1) continue;
                    count /= 2;
                    if (count < 1) count = 1;
                }
            }
            catch
            {
                //ignored
            }
        }

        private void OpenDigits(object sender, RoutedEventArgs e) => IsDigit = !IsDigit;

        private void ChangeLanguage(object sender, RoutedEventArgs e) => SetLanguage();

        private void SetLanguage() => CurrentLanguage = CurrentLanguage == LanguageType.Eng ? LanguageType.Ru : LanguageType.Eng;


        private List<List<KeyRecord>> GetSymbols() => IsDigit
            ? FirstSpecKeys
            : CurrentLanguage switch
            {
                LanguageType.Ru => FirstLangKeys,
                LanguageType.Eng => SecondLangKeys,
                _ => throw new ArgumentOutOfRangeException()
            };

        private void SetButtonBaseWidth()
        {
            var buttonsCount = ShowedKeys.Max(keys => keys.Count);
            var allHorizontalMargin = (ButtonBaseMargin.Left + ButtonBaseMargin.Right) * buttonsCount;

            ButtonBaseWidth = (Width - allHorizontalMargin) / buttonsCount;
        }

        private void SetButtonBaseHeight()
        {
            var buttonCount = (IsEmailPrinting ? 5.0 : 4.0);

            var allVerticalMargin = (ButtonBaseMargin.Top + ButtonBaseMargin.Bottom) * buttonCount;

            ButtonBaseHeight = (Height - allVerticalMargin) / buttonCount;
        }

        private List<KeyRecord> GetEmailKeys(EmailTypes emailTypes) => emailTypes switch
        {
            EmailTypes.Inbox => EmailKeys[0],
            EmailTypes.Mail => EmailKeys[1],
            EmailTypes.Yandex => EmailKeys[2],
            _ => throw new ArgumentOutOfRangeException(nameof(emailTypes), emailTypes, null)
        };

        private static List<List<KeyRecord>> SetRuSymbols()
        {
            List<List<KeyRecord>> keys =
            [
                [
                    new KeyRecord("й", Key.Q,LanguageType.Ru),
                    new KeyRecord("ц", Key.W,LanguageType.Ru),
                    new KeyRecord("у", Key.E,LanguageType.Ru),
                    new KeyRecord("к", Key.R,LanguageType.Ru),
                    new KeyRecord("е", Key.T,LanguageType.Ru),
                    new KeyRecord("н", Key.Y,LanguageType.Ru),
                    new KeyRecord("г", Key.U,LanguageType.Ru),
                    new KeyRecord("ш", Key.I,LanguageType.Ru),
                    new KeyRecord("щ", Key.O,LanguageType.Ru),
                    new KeyRecord("з", Key.P,LanguageType.Ru),
                    new KeyRecord("х", Key.OemOpenBrackets,LanguageType.Ru),
                    new KeyRecord("ъ", Key.OemCloseBrackets,LanguageType.Ru),
                ],
                [
                    new KeyRecord("ф", Key.A,LanguageType.Ru),
                    new KeyRecord("ы", Key.S,LanguageType.Ru),
                    new KeyRecord("в", Key.D,LanguageType.Ru),
                    new KeyRecord("а", Key.F,LanguageType.Ru),
                    new KeyRecord("п", Key.G,LanguageType.Ru),
                    new KeyRecord("р", Key.H,LanguageType.Ru),
                    new KeyRecord("о", Key.J,LanguageType.Ru),
                    new KeyRecord("л", Key.K,LanguageType.Ru),
                    new KeyRecord("д", Key.L,LanguageType.Ru),
                    new KeyRecord("ж", Key.Oem1,LanguageType.Ru),
                    new KeyRecord("э", Key.OemQuotes,LanguageType.Ru)
                ],
                [
                    new KeyRecord("я", Key.Z,LanguageType.Ru),
                    new KeyRecord("ч", Key.X,LanguageType.Ru),
                    new KeyRecord("с", Key.C,LanguageType.Ru),
                    new KeyRecord("м", Key.V,LanguageType.Ru),
                    new KeyRecord("и", Key.B,LanguageType.Ru),
                    new KeyRecord("т", Key.N,LanguageType.Ru),
                    new KeyRecord("ь", Key.M,LanguageType.Ru),
                    new KeyRecord("б", Key.OemComma,LanguageType.Ru),
                    new KeyRecord("ю", Key.OemPeriod,LanguageType.Ru)
                ]
            ];
            return keys;
        }

        private static List<List<KeyRecord>> SetEngSymbols()
        {
            List<List<KeyRecord>> keys =
            [
                [
                    new KeyRecord("q", Key.Q,LanguageType.Eng),
                    new KeyRecord("w", Key.W,LanguageType.Eng),
                    new KeyRecord("e", Key.E,LanguageType.Eng),
                    new KeyRecord("r", Key.R,LanguageType.Eng),
                    new KeyRecord("t", Key.T,LanguageType.Eng),
                    new KeyRecord("y", Key.Y,LanguageType.Eng),
                    new KeyRecord("u", Key.U,LanguageType.Eng),
                    new KeyRecord("i", Key.I,LanguageType.Eng),
                    new KeyRecord("o", Key.O,LanguageType.Eng),
                    new KeyRecord("p", Key.P,LanguageType.Eng)
                ],
                [
                    new KeyRecord("a", Key.A,LanguageType.Eng),
                    new KeyRecord("s", Key.S,LanguageType.Eng),
                    new KeyRecord("d", Key.D,LanguageType.Eng),
                    new KeyRecord("f", Key.F,LanguageType.Eng),
                    new KeyRecord("g", Key.G,LanguageType.Eng),
                    new KeyRecord("h", Key.H,LanguageType.Eng),
                    new KeyRecord("j", Key.J,LanguageType.Eng),
                    new KeyRecord("k", Key.K,LanguageType.Eng),
                    new KeyRecord("l", Key.L,LanguageType.Eng)
                ],
                [
                    new KeyRecord("z", Key.Z,LanguageType.Eng),
                    new KeyRecord("x", Key.X,LanguageType.Eng),
                    new KeyRecord("c", Key.C,LanguageType.Eng),
                    new KeyRecord("v", Key.V,LanguageType.Eng),
                    new KeyRecord("b", Key.B,LanguageType.Eng),
                    new KeyRecord("n", Key.N,LanguageType.Eng),
                    new KeyRecord("m", Key.M,LanguageType.Eng)
                ]
            ];
            return keys;
        }

        private static List<List<KeyRecord>> SetFirstSpecSymbols()
        {
            List<List<KeyRecord>> keys =
            [
                [
                    new KeyRecord("1", Key.D1,IsShiftPressed:false),
                    new KeyRecord("2", Key.D2,IsShiftPressed:false),
                    new KeyRecord("3", Key.D3,IsShiftPressed:false),
                    new KeyRecord("4", Key.D4,IsShiftPressed:false),
                    new KeyRecord("5", Key.D5,IsShiftPressed:false),
                    new KeyRecord("6", Key.D6,IsShiftPressed:false),
                    new KeyRecord("7", Key.D7,IsShiftPressed:false),
                    new KeyRecord("8", Key.D8,IsShiftPressed:false),
                    new KeyRecord("9", Key.D9,IsShiftPressed:false),
                    new KeyRecord("0", Key.D0,IsShiftPressed:false),
                ],
                [
                    new KeyRecord("-", Key.OemMinus,IsShiftPressed:false),
                    new KeyRecord("/", Key.OemBackslash,LanguageType.Ru,true),
                    new KeyRecord(":", Key.Oem1,LanguageType.Eng,true),
                    new KeyRecord(";", Key.Oem1,LanguageType.Eng,false),
                    new KeyRecord("(", Key.D9,IsShiftPressed:true),
                    new KeyRecord(")", Key.D0,IsShiftPressed:true),
                    new KeyRecord("&", Key.D7,LanguageType.Eng,true),
                    new KeyRecord("@", Key.D2,LanguageType.Eng,true),
                    new KeyRecord("\"", Key.OemQuotes,LanguageType.Eng, true)
                ],
                [
                    new KeyRecord(".", Key.OemPeriod, LanguageType.Eng, false),
                    new KeyRecord(",", Key.OemComma, LanguageType.Eng, false),
                    new KeyRecord("?", Key.OemQuestion, LanguageType.Eng, true),
                    new KeyRecord("!", Key.D1, IsShiftPressed:true),
                    new KeyRecord("'", Key.OemQuotes, LanguageType.Eng, false),
                    new KeyRecord("_", Key.OemMinus, IsShiftPressed:true)
                ]
            ];
            return keys;
        }

        private static List<KeyRecord> SetAdditionalSymbols()
        {
            List<KeyRecord> keys =
            [
                new("@", Key.D2, LanguageType.Eng, true),
                new(".", Key.OemPeriod, LanguageType.Eng, false),
            ];
            return keys;
        }

        private static List<List<KeyRecord>> SetEmailSymbols() =>
        [
            [
                new KeyRecord("@", Key.D2, LanguageType.Eng, true),
                new KeyRecord("i", Key.I, LanguageType.Eng, false),
                new KeyRecord("n", Key.N, LanguageType.Eng, false),
                new KeyRecord("b", Key.B, LanguageType.Eng, false),
                new KeyRecord("o", Key.O, LanguageType.Eng, false),
                new KeyRecord("x", Key.X, LanguageType.Eng, false),
                new KeyRecord(".", Key.OemPeriod, LanguageType.Eng, false),
                new KeyRecord("r", Key.R, LanguageType.Eng, false),
                new KeyRecord("u", Key.U, LanguageType.Eng, false)
            ],
            [
                new KeyRecord("@", Key.D2, LanguageType.Eng, true),
                new KeyRecord("m", Key.M, LanguageType.Eng, false),
                new KeyRecord("a", Key.A, LanguageType.Eng, false),
                new KeyRecord("i", Key.I, LanguageType.Eng, false),
                new KeyRecord("l", Key.L, LanguageType.Eng, false),
                new KeyRecord(".", Key.OemPeriod, LanguageType.Eng, false),
                new KeyRecord("r", Key.R, LanguageType.Eng, false),
                new KeyRecord("u", Key.U, LanguageType.Eng, false)
            ],
            [
                new KeyRecord("@", Key.D2, LanguageType.Eng, true),
                new KeyRecord("y", Key.Y, LanguageType.Eng, false),
                new KeyRecord("a", Key.A, LanguageType.Eng, false),
                new KeyRecord("n", Key.N, LanguageType.Eng, false),
                new KeyRecord("d", Key.D, LanguageType.Eng, false),
                new KeyRecord("e", Key.E, LanguageType.Eng, false),
                new KeyRecord("x", Key.X, LanguageType.Eng, false),
                new KeyRecord(".", Key.OemPeriod, LanguageType.Eng, false),
                new KeyRecord("r", Key.R, LanguageType.Eng, false),
                new KeyRecord("u", Key.U, LanguageType.Eng, false)
            ]
        ];
        #endregion
    }
}