namespace CookieCode.Consoles.Tui
{
    public static class ConsoleModifiersExtensions
    {
        public static bool HasAlt(this ConsoleModifiers modifiers)
        {
            var result = (modifiers & ConsoleModifiers.Alt) != 0;
            return result;
        }

        public static bool HasControl(this ConsoleModifiers modifiers)
        {
            var result = (modifiers & ConsoleModifiers.Control) != 0;
            return result;
        }

        public static bool HasShift(this ConsoleModifiers modifiers)
        {
            var result = (modifiers & ConsoleModifiers.Shift) != 0;
            return result;
        }
    }
}
