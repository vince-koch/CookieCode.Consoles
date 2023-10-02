using CookieCode.Consoles.Drivers;
using System.Drawing;

namespace CookieCode.Consoles
{
    public interface IBorder
    {
        char Horizontal { get; }
        char Vertical { get; }

        char NW { get; }
        char NE { get; }
        char SW { get; }
        char SE { get; }

        char N { get; }
        char S { get; }
        char E { get; }
        char W { get; }

        char Cross { get; }
    }

    public class SimpleBorder : IBorder
    {
        public char Vertical { get; } = '│';
        public char Horizontal { get; } = '─';

        public char NW { get; } = '+';
        public char SW { get; } = '+';
        public char NE { get; } = '+';
        public char SE { get; } = '+';

        public char N { get; } = '+';
        public char S { get; } = '+';
        public char E { get; } = '+';
        public char W { get; } = '+';

        public char Cross { get; } = '+';
    }

    public class SingleBorder : IBorder
    {
        public char Vertical { get; } = '│';
        public char Horizontal { get; } = '─';

        public char NW { get; } = '┌';
        public char SW { get; } = '└';
        public char NE { get; } = '┐';
        public char SE { get; } = '┘';

        public char N { get; } = '┬';
        public char S { get; } = '┴';
        public char E { get; } = '┤';
        public char W { get; } = '├';

        public char Cross { get; } = '┼';
    }

    public class DoubleBorder : IBorder
    {
        public char Vertical { get; } = '║';
        public char Horizontal { get; } = '═';

        public char NW { get; } = '╔';
        public char SW { get; } = '╚';
        public char NE { get; } = '╗';
        public char SE { get; } = '╝';

        public char N { get; } = '╦';
        public char S { get; } = '╩';
        public char E { get; } = '╣';
        public char W { get; } = '╠';

        public char Cross { get; } = '╬';
    }

    public static class Border
    {
        public static IBorder Simple { get; } = new SimpleBorder();

        public static IBorder Single { get; } = new SingleBorder();
            
        public static IBorder Double { get; } = new DoubleBorder();
    }

    public static class IBorderExtensions
    {
        public static IConsole Write(this IConsole console, string text)
        {
            console.Write(text, Color.Transparent, Color.Transparent);
            return console;
        }

        public static IConsole DrawBorder(this IConsole console, Rectangle rect, IBorder border)
        {           
            for (int y = rect.Top; y <= rect.Bottom; y++)
            {
                console.SetCursorPosition(new Point(rect.Left, y));

                if (y == rect.Top)
                {
                    console.Write($"{border.NW}{new string(border.Horizontal, rect.Width - 2)}{border.NE}");
                }

                else if (y < rect.Bottom)
                {
                    console.Write(border.Vertical.ToString());
                    console.SetCursorPosition(new Point(rect.Right, y));
                    console.Write(border.Vertical.ToString());
                }

                if (y == rect.Bottom)
                {
                    console.Write($"{border.SW}{new string(border.Horizontal, rect.Width - 2)}{border.SE}");
                }
            }

            return console;
        }
    }
}
