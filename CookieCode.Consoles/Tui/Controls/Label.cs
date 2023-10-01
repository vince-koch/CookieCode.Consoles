using System.Diagnostics;
using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    [DebuggerDisplay("Label [{Text}]")]
    public class Label : Control
    {
        public Color ForeColor { get; set; } = Color.DarkGray;

        public Color BackColor { get; set; } = Color.Transparent;

        public BindSource<string?> Text { get; set; } = "Label";

        public Label()
        {
        }

        public Label(BindSource<string?> text)
        {
            Text = text;
        }

        public override void Render(RenderContext context)
        {
            context.Write(
                Text,
                ForeColor,
                BackColor);
        }
    }
}
