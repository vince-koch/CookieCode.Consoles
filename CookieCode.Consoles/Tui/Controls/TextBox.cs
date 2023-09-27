using System.Diagnostics;
using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    [DebuggerDisplay("TextBox [{Text}]")]
    public class TextBox : Control
    {
        public override bool CanFocus => true;

        public BindSource<string?> Placeholder { get; set; } = string.Empty;

        public BindSource<string?> Text { get; set; } = string.Empty;

        private int _cursorIndex = 0;

        public override void HandleKeyEvent(ConsoleKeyInfoEventArgs e)
        {
            HandleTabKey(e);
            if (e.IsHandled)
            {
                return;
            }

            switch (e.Key.Key)
            {
                case ConsoleKey.Backspace:
                    if (_cursorIndex > 0)
                    {
                        _cursorIndex--;
                        var charList = Text.ToString().ToCharArray().ToList();
                        charList.RemoveAt(_cursorIndex);
                        Text = new string(charList.ToArray());
                    }
                    break;
                        
                case ConsoleKey.Delete:
                    if (_cursorIndex < Text.ToString().Length)
                    {
                        var charList = Text.ToString().ToCharArray().ToList();
                        charList.RemoveAt(_cursorIndex);
                        Text = new string(charList.ToArray());
                    }
                    break;

                case ConsoleKey.RightArrow:
                    _cursorIndex = Math.Min(_cursorIndex + 1, Text.ToString().Length);
                    break;

                case ConsoleKey.LeftArrow:
                    _cursorIndex = Math.Max(_cursorIndex - 1, 0);
                    break;

                case ConsoleKey.Home:
                    _cursorIndex = 0;
                    break;

                case ConsoleKey.End:
                    _cursorIndex = Text.ToString().Length;
                    break;

                default:
                    if (e.Key.KeyChar != 0)
                    { 
                        var charList = Text.ToString().ToCharArray().ToList();
                        charList.Insert(_cursorIndex, e.Key.KeyChar);
                        Text = new string(charList.ToArray());
                        _cursorIndex++;
                    }
                    break;
            }
        }

        public override void Render(RenderContext context)
        {
            var (x, y) = Console.GetCursorPosition();
            Cursor = new Point(x + _cursorIndex, y);

            var isPlaceHolder = string.IsNullOrWhiteSpace(Text); 
            var textOrPlaceHolder = string.IsNullOrWhiteSpace(Text) ? Placeholder : Text;

            Console.ForegroundColor = isPlaceHolder
                ? ConsoleColor.DarkGray
                : context.Focus == this
                    ? ConsoleColor.Black
                    : ConsoleColor.Gray;

            Console.BackgroundColor = context.Focus == this
                ? ConsoleColor.Yellow
                : ConsoleColor.Black;

            Console.Write($"{textOrPlaceHolder}");

            Console.ResetColor();
        }
    }
}
