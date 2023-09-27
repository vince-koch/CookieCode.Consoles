using System.Drawing;

namespace CookieCode.Consoles.Drivers
{
    public abstract class AbstractConsole
    {
        private bool _isCursorVisible = OperatingSystem.IsWindows() ? Console.CursorVisible : true;

        public bool IsCursorVisible
        {
            get { return OperatingSystem.IsWindows() ? Console.CursorVisible : _isCursorVisible; }
            set { Console.CursorVisible = value; _isCursorVisible = value; }
        }

        public Point Cursor
        {
            get => new Point(Console.CursorLeft, Console.CursorTop);
            set => Console.SetCursorPosition(value.X, value.Y);
        }
    }
}
