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

        public List<List<KeyRecord>> Keys
        {
            get => (List<List<KeyRecord>>)GetValue(KeysProperty);
            set => SetValue(KeysProperty, value);
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
        public List<KeyRecord> AdditionalSymbols
        {
            get => (List<KeyRecord>)GetValue(AdditionalSymbolsProperty);
            set => SetValue(AdditionalSymbolsProperty, value);
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

        public static readonly DependencyProperty KeysProperty = DependencyProperty.Register(
            nameof(Keys), typeof(List<List<KeyRecord>>), typeof(Keyboard),
            new PropertyMetadata(new List<List<KeyRecord>>()));

        public static readonly DependencyProperty CurrentLanguageProperty = DependencyProperty.Register(
            nameof(CurrentLanguage), typeof(LanguageType), typeof(Keyboard), new PropertyMetadata(LanguageType.Ru));

        public static readonly DependencyProperty ShiftPressedProperty = DependencyProperty.Register(
            nameof(ShiftPressed), typeof(bool), typeof(Keyboard), new PropertyMetadata(false));

        public static readonly DependencyProperty IsDigitProperty = DependencyProperty.Register(
            nameof(IsDigit), typeof(bool), typeof(Keyboard), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEmailPrintingProperty = DependencyProperty.Register(
            nameof(IsEmailPrinting), typeof(bool), typeof(Keyboard), new PropertyMetadata(false,IsEmailPrintingChanged));

        public static readonly DependencyProperty AdditionalSymbolsProperty = DependencyProperty.Register(
            nameof(AdditionalSymbols), typeof(List<KeyRecord>), typeof(Keyboard), new PropertyMetadata(new List<KeyRecord>()));

        public static readonly DependencyProperty MainButtonStyleProperty = DependencyProperty.Register(
            nameof(MainButtonStyle), typeof(Style), typeof(Keyboard), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty AdditionalButtonStyleProperty = DependencyProperty.Register(
            nameof(AdditionalButtonStyle), typeof(Style), typeof(Keyboard), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty EmailButtonStyleProperty = DependencyProperty.Register(
            nameof(EmailButtonStyle), typeof(Style), typeof(Keyboard), new PropertyMetadata(default(Style)));
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

        private static void IsEmailPrintingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Keyboard)d;

            control.AdditionalSymbols = (bool)e.NewValue ? control.SetAdditionalSymbols() : [];
            control.SetButtonBaseHeight();
        }

        #endregion

        #region Constructor

        public Keyboard()
        {
            InitializeComponent();

            if (InputLanguageManager.Current.CurrentInputLanguage.NativeName.Contains("English"))
            {
                CurrentLanguage = LanguageType.Eng;
                SetLanguage();
            }

            Keys = GetSymbols();
        }
        #endregion

        #region Methods

        private void PrintEmail(object sender, RoutedEventArgs e)
        {
            if (sender is not ButtonBase button) return;
            if (button.Tag is not EmailTypes emailType) return;

            foreach (var keyRecord in SetEmailSymbols(emailType))
            {
                keyRecord.PrintKeyWithParams(ShiftPressed, CurrentLanguage);
            }
        }

        private void PrintKey(object sender, RoutedEventArgs e)
        {
            if (sender is not ButtonBase button) return;
            switch (button.Tag)
            {
                case KeyRecord keyRecord:
                    keyRecord.PrintKeyWithParams(ShiftPressed,CurrentLanguage);
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

        private void OpenDigits(object sender, RoutedEventArgs e)
        {
            IsDigit = !IsDigit;
            Keys = GetSymbols();
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            SetLanguage();
            Keys = GetSymbols();
        }

        private void SetLanguage()
        {
            Utilities.Keyboard.Press(Key.LeftAlt);
            Utilities.Keyboard.Type(Key.LeftShift);
            Utilities.Keyboard.Release(Key.LeftAlt);
            CurrentLanguage = CurrentLanguage == LanguageType.Eng ? LanguageType.Ru : LanguageType.Eng;
        }

        private List<List<KeyRecord>> GetSymbols() => IsDigit
            ? SetDigitSymbols()
            : CurrentLanguage switch
            {
                LanguageType.Eng => SetEngSymbols(),
                LanguageType.Ru => SetRuSymbols(),
                _ => throw new ArgumentOutOfRangeException()
            };

        private List<List<KeyRecord>> SetEngSymbols()
        {
            List<List<KeyRecord>> keys =
            [
                [
                    new KeyRecord("q", Key.Q),
                    new KeyRecord("w", Key.W),
                    new KeyRecord("e", Key.E),
                    new KeyRecord("r", Key.R),
                    new KeyRecord("t", Key.T),
                    new KeyRecord("y", Key.Y),
                    new KeyRecord("u", Key.U),
                    new KeyRecord("i", Key.I),
                    new KeyRecord("o", Key.O),
                    new KeyRecord("p", Key.P)
                ],
                [
                    new KeyRecord("a", Key.A),
                    new KeyRecord("s", Key.S),
                    new KeyRecord("d", Key.D),
                    new KeyRecord("f", Key.F),
                    new KeyRecord("g", Key.G),
                    new KeyRecord("h", Key.H),
                    new KeyRecord("j", Key.J),
                    new KeyRecord("k", Key.K),
                    new KeyRecord("l", Key.L)
                ],
                [
                    new KeyRecord("z", Key.Z),
                    new KeyRecord("x", Key.X),
                    new KeyRecord("c", Key.C),
                    new KeyRecord("v", Key.V),
                    new KeyRecord("b", Key.B),
                    new KeyRecord("n", Key.N),
                    new KeyRecord("m", Key.M)
                ]
            ];
            SetButtonBaseWidth();
            return keys;
        }

        private List<List<KeyRecord>> SetRuSymbols()
        {
            List<List<KeyRecord>> keys =
            [
                [
                    new KeyRecord("й", Key.Q),
                    new KeyRecord("ц", Key.W),
                    new KeyRecord("у", Key.E),
                    new KeyRecord("к", Key.R),
                    new KeyRecord("е", Key.T),
                    new KeyRecord("н", Key.Y),
                    new KeyRecord("г", Key.U),
                    new KeyRecord("ш", Key.I),
                    new KeyRecord("щ", Key.O),
                    new KeyRecord("з", Key.P),
                    new KeyRecord("х", Key.OemOpenBrackets),
                    new KeyRecord("ъ", Key.OemCloseBrackets),
                ],
                [
                    new KeyRecord("ф", Key.A),
                    new KeyRecord("ы", Key.S),
                    new KeyRecord("в", Key.D),
                    new KeyRecord("а", Key.F),
                    new KeyRecord("п", Key.G),
                    new KeyRecord("р", Key.H),
                    new KeyRecord("о", Key.J),
                    new KeyRecord("л", Key.K),
                    new KeyRecord("д", Key.L),
                    new KeyRecord("ж", Key.Oem1),
                    new KeyRecord("э", Key.OemQuotes)
                ],
                [
                    new KeyRecord("я", Key.Z),
                    new KeyRecord("ч", Key.X),
                    new KeyRecord("с", Key.C),
                    new KeyRecord("м", Key.V),
                    new KeyRecord("и", Key.B),
                    new KeyRecord("т", Key.N),
                    new KeyRecord("ь", Key.M),
                    new KeyRecord("б", Key.OemComma),
                    new KeyRecord("ю", Key.OemPeriod)
                ]
            ];
            SetButtonBaseWidth();
            return keys;
        }

        private List<List<KeyRecord>> SetDigitSymbols()
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
                ]
            ];
            SetButtonBaseWidth();
            return keys;
        }

        private List<KeyRecord> SetAdditionalSymbols()
        {
            List<KeyRecord> keys =
            [
                new KeyRecord("@", Key.D2, LanguageType.Eng, true),
                new KeyRecord(".", Key.OemPeriod, LanguageType.Eng, false),
            ];
            return keys;
        }

        private List<KeyRecord> SetEmailSymbols(EmailTypes emailTypes) => emailTypes switch
            {
                EmailTypes.Gmail =>
                [
                    new KeyRecord("@", Key.D2, LanguageType.Eng, true),
                    new KeyRecord("g", Key.G, LanguageType.Eng, false),
                    new KeyRecord("m", Key.M, LanguageType.Eng, false),
                    new KeyRecord("a", Key.A, LanguageType.Eng, false),
                    new KeyRecord("i", Key.I, LanguageType.Eng, false),
                    new KeyRecord("l", Key.L, LanguageType.Eng, false),
                    new KeyRecord(".", Key.OemPeriod, LanguageType.Eng, false),
                    new KeyRecord("c", Key.C, LanguageType.Eng, false),
                    new KeyRecord("o", Key.O, LanguageType.Eng, false),
                    new KeyRecord("m", Key.M, LanguageType.Eng, false)
                ],
                EmailTypes.Mail =>
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
                EmailTypes.Yandex => [
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
                ],
                _ => throw new ArgumentOutOfRangeException(nameof(emailTypes), emailTypes, null)
            };

        private void SetButtonBaseWidth()
        {
            var buttonsCount = CurrentLanguage == LanguageType.Eng || IsDigit ? 10 : 12;
            var allHorizontalMargin = (ButtonBaseMargin.Left + ButtonBaseMargin.Right) * buttonsCount;

            ButtonBaseWidth = (Width - allHorizontalMargin) / buttonsCount;
        }

        private void SetButtonBaseHeight()
        {
            var allVerticalMargin = (ButtonBaseMargin.Top + ButtonBaseMargin.Bottom) * 5;

            ButtonBaseHeight = (Height - allVerticalMargin) / (IsEmailPrinting ? 5.0 : 4.0);
        }

        #endregion
    }
}