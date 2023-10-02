using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    public class GroupBox : Container
    {
        public IBorder Border { get; set; } = CookieCode.Consoles.Border.Single;

        public Color ForeColor { get; set; } = Color.Gray;

        public BindSource<string?> Text { get; set; } = "Group";

        public GroupBox(BindSource<string?> text)
        {
            Text = text;
        }

        public GroupBox SetBorder(IBorder border)
        {
            Border = border;
            return this;
        }

        public GroupBox SetColor(Color foreColor)
        {
            ForeColor = foreColor;
            return this;
        }

        public override void Render(RenderContext context)
        {
            context.DrawBorder(foreColor: ForeColor);

            var text = Text.ToString();
            if (!string.IsNullOrWhiteSpace(text))
            {
                context.Write($" {text} ", 1, 0, ForeColor);
            }

            var childRectangle = new Rectangle(Point.Empty, context.Size);
            childRectangle.Inflate(-1, -1);

            var childContext = new RenderContext(context, childRectangle);

            foreach (var child in Children)
            {
                child.Render(childContext);
            }
        }
    }
}
