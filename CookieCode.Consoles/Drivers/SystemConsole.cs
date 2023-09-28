using System.Drawing;

namespace CookieCode.Consoles.Drivers
{
    public class SystemConsole : IConsole
    {
        private bool _isCursorVisible = OperatingSystem.IsWindows() ? Console.CursorVisible : true;
        private string _title = string.Empty;

        public virtual IConsole Clear()
        {
            Console.Clear();
            return this;
        }

        public virtual IConsole ResetColor()
        {
            Console.ResetColor();
            return this;
        }

        public virtual string GetTitle()
        {
            return OperatingSystem.IsWindows()
                ? Console.Title
                : _title;
        }
        
        public virtual (int X, int Y) GetCursorPosition()
        {
            return  Console.GetCursorPosition();
        }

        public virtual bool GetCursorVisible()
        {
            return OperatingSystem.IsWindows()
                ? Console.CursorVisible
                : _isCursorVisible;
        }

        public virtual int GetWindowHeight()
        {
            return Console.WindowHeight;
        }

        public virtual int GetWindowWidth()
        {
            return Console.WindowWidth;
        }

        public virtual IConsole SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            return this;
        }

        public virtual IConsole SetCursorPosition(Point cursor)
        {
            Console.SetCursorPosition(cursor.X, cursor.Y);
            return this;
        }

        public virtual IConsole SetCursorVisible(bool visible)
        {
            Console.CursorVisible = visible;
            _isCursorVisible = visible;
            return this;
        }

        public virtual IConsole SetTitle(string title)
        {
            Console.Title = title;
            _title = title;
            return this;
        }

        public virtual IConsole Write(string text, Color fore, Color back)
        {
            if (fore != Color.Transparent)
            {
                Console.ForegroundColor = fore.ToConsoleColor();
            }

            if (back != Color.Transparent)
            {
                Console.BackgroundColor = back.ToConsoleColor();
            }

            Console.Write(text);
            return this;
        }

        public virtual IConsole Write(string text, int x, int y, Color fore, Color back)
        {
            Console.SetCursorPosition(x, y);
            Write(text, fore, back);
            return this;
        }

        public virtual IConsole WriteLine()
        {
            Console.WriteLine();
            return this;
        }

        public virtual IConsole WriteLine(string text, Color fore, Color back)
        {
            Write(text, fore, back);
            Console.WriteLine();
            return this;
        }
    }
}
