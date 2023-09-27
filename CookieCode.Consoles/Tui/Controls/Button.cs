using System.Diagnostics;
using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    [DebuggerDisplay("Button [{Text}]")]
    public class Button : Control
    {
        public event EventHandler<Button>? Click;

        public override bool CanFocus => true;

        public BindSource<string?> Text { get; set; } = "Button";

        public Button()
        {
        }

        public Button(BindSource<string?> text, EventHandler<Button> click)
        {
            Text = text;
            Click += click;
        }

        public override void HandleKeyEvent(ConsoleKeyInfoEventArgs e)
        {
            HandleTabKey(e);
            
            if (e.Key.Key == ConsoleKey.Enter)
            {
                Click?.Invoke(this, this);
            }
        }

        public override void Render(RenderContext context)
        {
            var (x, y) = Console.GetCursorPosition();
            Cursor = new Point(x + 2, y);

            Console.ForegroundColor = context.Focus == this
                ? ConsoleColor.Black
                : ConsoleColor.Gray;

            Console.BackgroundColor = context.Focus == this
                ? ConsoleColor.Yellow
                : ConsoleColor.Black;

            Console.Write($"[ {Text} ]");

            Console.ResetColor();
        }
    }
}
