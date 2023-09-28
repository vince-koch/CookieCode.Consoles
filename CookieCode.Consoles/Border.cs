namespace CookieCode.Core.Consoles
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

    public static class Borders
    {
        public static IBorder Simple { get; } = new SimpleBorder();

        public static IBorder Single { get; } = new SingleBorder();
            
        public static IBorder Double { get; } = new DoubleBorder();
    }
}
