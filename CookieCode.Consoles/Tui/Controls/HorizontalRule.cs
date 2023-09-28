using CookieCode.Consoles.Colors;
using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    public class HorizontalRule : Control
    {
        public Color ForeColor { get; set; } = DraculaColors.Comment;

        public Color BackColor { get; set; } = Color.Transparent;

        public override void Render(RenderContext context)
        {
            var pos = context.Console.GetCursorPosition();
            if (pos.X != 0)
            {
                context.Console.WriteLine();
            }

            context.Console.WriteLine(
                new string('-', context.Console.GetWindowSize().Width),
                ForeColor,
                BackColor);

            context.Console.ResetColor();
        }
    }
}
