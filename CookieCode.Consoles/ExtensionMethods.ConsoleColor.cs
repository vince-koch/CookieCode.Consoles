using System;

namespace CookieCode.Consoles
{
    public static partial class ExtensionMethods
    {
        public static ConsoleColor FgForBg(this ConsoleColor back)
        {
            var fore = (int)back < 7 ? ConsoleColor.White : ConsoleColor.Black;
            return fore;
        }
    }
}
