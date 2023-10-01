using CookieCode.Consoles.Colors;
using CookieCode.Core.Consoles;

using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    public class HorizontalRule : Control
    {
        public Color Color { get; set; } = DraculaColors.Comment;

        public HorizontalRule SetColor(Color foreColor)
        {
            Color = foreColor;
            return this;
        }

        public override void Render(RenderContext context)
        {
            context.Write(
                new string(Borders.Single.Horizontal, context.Size.Width),
                Color,
                Color.Transparent);
        }
    }
}
