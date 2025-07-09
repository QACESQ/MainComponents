using System.Globalization;
using System.Windows.Data;

namespace MainComponents.Converters;

public class StringRegisterConverter:IMultiValueConverter
{

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2) return values;
        if (values[0] is not string str) return values;
        if (values[1] is not bool boolean) return values;
        return boolean ? str.ToUpper() : str.ToLower();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}