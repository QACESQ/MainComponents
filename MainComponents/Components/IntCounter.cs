using System.Windows;
using System.Windows.Controls;

namespace MainComponents.Components;

[TemplatePart(Name = "PART_MinusButton",Type = typeof(Button))]
[TemplatePart(Name = "PART_PlusButton",Type = typeof(Button))]
[TemplatePart(Name = "PART_ValueText",Type = typeof(TextBlock))]
public class IntCounter:Control
{

    private Button? _minus;
    private Button? _plus;
    private TextBlock? _value;

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(int), typeof(IntCounter), new FrameworkPropertyMetadata(default(int),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ValueChanged));



    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
        nameof(Min), typeof(int), typeof(IntCounter), new PropertyMetadata(default(int), MinChanged));



    public int Min
    {
        get { return (int)GetValue(MinProperty); }
        set { SetValue(MinProperty, value); }
    }

    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
        nameof(Max), typeof(int), typeof(IntCounter), new PropertyMetadata(default(int), MaxChanged));



    public int Max
    {
        get { return (int)GetValue(MaxProperty); }
        set { SetValue(MaxProperty, value); }
    }

    static IntCounter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(IntCounter), new FrameworkPropertyMetadata(typeof(IntCounter)));
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _minus = GetTemplateChild("PART_MinusButton") as Button;
        _plus = GetTemplateChild("PART_PlusButton") as Button;
        if (_minus is not null) _minus.Click += MinusButton_OnClick;
        if (_plus is not null) _plus.Click += PlusButton_OnClick;
        _value = GetTemplateChild("PART_ValueText") as TextBlock;
        Validate();
    }


    private static void MaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as IntCounter)?.Validate();
    private static void MinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as IntCounter)?.Validate();
    private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as IntCounter)?.Validate();


    private void MinusButton_OnClick(object sender, RoutedEventArgs e) => Value--;
    private void PlusButton_OnClick(object sender, RoutedEventArgs e) => Value++;

    private void Validate()
    {
        if (_minus is not null) _minus.IsEnabled = Value > Min;
        if (_plus is not null) _plus.IsEnabled = Value < Max;
        if (_value is not null) _value.Text = Value.ToString();
    }
}