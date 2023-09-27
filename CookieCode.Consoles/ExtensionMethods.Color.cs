using System;
using System.Drawing;

namespace CookieCode.Consoles
{
    public static partial class ExtensionMethods
    {
        public static Color FgForBg(this Color back)
        {
            var fore = (back.R + back.B + back.G) / 3 > 128
                ? Color.Black
                : Color.White;

            return fore;
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="ColorRgb"/> structure.
        /// </returns>
        public static Color Brightness(this Color color, float correctionFactor)
        {
            if (correctionFactor < -1 || correctionFactor > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(correctionFactor), "Value must be between -1 and 1");
            }

            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb((int)red, (int)green, (int)blue);
        }

        public static ConsoleColor ToConsoleColor(this Color color)
        {
            int index = color.R > 128 | color.G > 128 | color.B > 128 ? 8 : 0; // Bright bit
            index |= color.R > 64 ? 4 : 0; // Red bit
            index |= color.G > 64 ? 2 : 0; // Green bit
            index |= color.B > 64 ? 1 : 0; // Blue bit

            return (ConsoleColor)index;
        }
    }
}
