using CookieCode.Consoles.Colors;
using System.Diagnostics;
using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    [DebuggerDisplay("TextBox [{Text}]")]
    public class TextBox : Control
    {
        public override bool CanFocus => true;

        public Color ForeColor { get; set; } = BootstrapColors.Black;

        public Color BackColor { get; set; } = BootstrapColors.Gray500;

        public Color PlaceholderColor { get; set; } = BootstrapColors.Gray700;

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
                        e.IsHandled = true;
                    }
                    break;
                        
                case ConsoleKey.Delete:
                    if (_cursorIndex < Text.ToString().Length)
                    {
                        var charList = Text.ToString().ToCharArray().ToList();
                        charList.RemoveAt(_cursorIndex);
                        Text = new string(charList.ToArray());
                        e.IsHandled = true;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    _cursorIndex = Math.Min(_cursorIndex + 1, Text.ToString().Length);
                    e.IsHandled = true;
                    break;

                case ConsoleKey.LeftArrow:
                    _cursorIndex = Math.Max(_cursorIndex - 1, 0);
                    e.IsHandled = true;
                    break;

                case ConsoleKey.Home:
                    _cursorIndex = 0;
                    e.IsHandled = true;
                    break;

                case ConsoleKey.End:
                    _cursorIndex = Text.ToString().Length;
                    e.IsHandled = true;
                    break;

                default:
                    if (e.Key.KeyChar != 0)
                    {
                        if (char.IsLetterOrDigit(e.Key.KeyChar)
                            || char.IsPunctuation(e.Key.KeyChar)
                            || char.IsSymbol(e.Key.KeyChar)
                            || char.IsSeparator(e.Key.KeyChar))
                        {
                            var charList = Text.ToString().ToCharArray().ToList();
                            charList.Insert(_cursorIndex, e.Key.KeyChar);
                            Text = new string(charList.ToArray());
                            _cursorIndex++;
                            e.IsHandled = true;
                        }
                    }
                    break;
            }
        }

        public override void Render(RenderContext context)
        {
            //var cursor = context.Console.GetCursorPosition();
            //cursor.Offset(_cursorIndex, 0);
            //Cursor = cursor;

            var back = context.Focus == this ? BackColor.Brightness(.3f) : BackColor;

            context.FillRectangle(new Rectangle(0, 0, context.Size.Width, 1), Color.Transparent, BackColor);

            var text = Text.ToString();
            if (string.IsNullOrWhiteSpace(text))
            {
                context.Write(
                    Placeholder?.ToString(),
                    PlaceholderColor,
                    back);
            }
            else
            {
                context.Write(
                    text,
                    ForeColor,
                    back);
            }
        }
    }
}
