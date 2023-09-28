using System.Drawing;

namespace CookieCode.Consoles.Drivers
{
    public class AnsiConsole : SystemConsole
    {
        public override IConsole Write(string text, Color fore, Color back)
        {
            if (fore != Color.Transparent)
            {
                Console.Write(Ansi.Fg(fore));
            }
            
            if (back != Color.Transparent)
            {
                Console.Write(Ansi.Bg(back));
            }
            
            Console.Write(text);
            return this;
        }

        public override IConsole Write(string text, int x, int y, Color fore, Color back)
        {
            Console.SetCursorPosition(x, y);
            Write(text, fore, back);
            return this;
        }
    }
}
