using System.Windows.Input;

namespace MainComponents.Models;

public record KeyRecord(string Title,Key Symbol,LanguageType Language = LanguageType.None,bool? IsShiftPressed = null);

public static class KeyRecordExtensions
{
    public static void PrintKeyWithParams(this KeyRecord keyRecord,bool isShiftPressed,LanguageType currentLanguage)
    {
        if (keyRecord.Language != LanguageType.None && keyRecord.Language != currentLanguage)
        {
            Utilities.Keyboard.Press(Key.LeftAlt);
            Utilities.Keyboard.Type(Key.LeftShift);
            Utilities.Keyboard.Release(Key.LeftAlt);
        }

        if ((isShiftPressed && keyRecord.IsShiftPressed != false) || keyRecord.IsShiftPressed == true) Utilities.Keyboard.Press(Key.LeftShift);

        Utilities.Keyboard.Type(keyRecord.Symbol);

        if ((isShiftPressed && keyRecord.IsShiftPressed != false) || keyRecord.IsShiftPressed == true) Utilities.Keyboard.Release(Key.LeftShift);

        if (keyRecord.Language != LanguageType.None && keyRecord.Language != currentLanguage)
        {
            Utilities.Keyboard.Press(Key.LeftAlt);
            Utilities.Keyboard.Type(Key.LeftShift);
            Utilities.Keyboard.Release(Key.LeftAlt);
        }
    }
}