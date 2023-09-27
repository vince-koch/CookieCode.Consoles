using System.Diagnostics;

namespace CookieCode.Consoles.Tui.Controls
{
    [DebuggerDisplay("Label [{Text}]")]
    public class Label : Control
    {
        public Color ForeColor { get; set; } = new Color(127, 127, 127);

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
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(Text);
            Console.ResetColor();
        }
    }
}
