using System.Windows;

namespace MainComponents.XamlBehaviorsExtensions;

public class CallMethodFunction : Microsoft.Xaml.Behaviors.TriggerAction<DependencyObject>
{
    public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
        nameof(TargetObject), typeof(FrameworkElement), typeof(CallMethodFunction), new PropertyMetadata(default(FrameworkElement)));

    public FrameworkElement TargetObject
    {
        get { return (FrameworkElement)GetValue(TargetObjectProperty); }
        set { SetValue(TargetObjectProperty, value); }
    }

    public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register(
        nameof(MethodName), typeof(string), typeof(CallMethodFunction), new PropertyMetadata(default(string)));

    public string MethodName
    {
        get { return (string)GetValue(MethodNameProperty); }
        set { SetValue(MethodNameProperty, value); }
    }

    /// <inheritdoc />
    protected override void Invoke(object parameter)
    {
        var method = TargetObject.GetType().GetMethod(MethodName);
        method?.Invoke(TargetObject, null);
    }
}