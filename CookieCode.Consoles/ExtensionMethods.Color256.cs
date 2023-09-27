using System;
using System.Drawing;

namespace CookieCode.Consoles
{
    public static partial class ExtensionMethods
    {
        public static Color FgForBg(this Ansi.Color256 back)
        {
            var index = (int)back;

            switch (index)
            {
                case < 16:
                    {
                        // standard colors
                        var fg = index < 7 ? Color.White : Color.Black;
                        return fg;
                    }

                case >= 16 and < 232:
                    {
                        // 216 colors
                        var x = (index - 16) % 36;
                        var fg = x <= 17 ? Color.White : Color.Black;
                        return fg;
                    }

                case >= 232 and < 256:
                    {
                        // grayscale colors
                        var x = (index - 16) % 36;
                        var fg = x <= 17 ? Color.White : Color.Black;
                        return fg;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
