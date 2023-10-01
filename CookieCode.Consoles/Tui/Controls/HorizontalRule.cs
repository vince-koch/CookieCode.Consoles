using CookieCode.Consoles.Colors;
using CookieCode.Core.Consoles;

using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    public class HorizontalRule : Control
    {
        public Color ForeColor { get; set; } = DraculaColors.Comment;

        public Color BackColor { get; set; } = Color.Transparent;

        public override void Render(RenderContext context)
        {
            context.Write(
                new string(Borders.Single.Horizontal, context.Size.Width),
                ForeColor,
                BackColor);
        }
    }
}
