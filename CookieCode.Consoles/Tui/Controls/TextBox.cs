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

        private int _textIndex = 0; 
        private BindSource<string?> _text = string.Empty;
        public BindSource<string?> Text
        {
            get => _text;
            set
            {
                _text = value;
                _textIndex = _text.ToString().Length;
            }
        }

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
                    if (_textIndex > 0)
                    {
                        _textIndex--;
                        var charList = Text.ToString().ToCharArray().ToList();
                        charList.RemoveAt(_textIndex);
                        _text = new string(charList.ToArray());
                        e.IsHandled = true;
                    }
                    break;
                        
                case ConsoleKey.Delete:
                    if (_textIndex < Text.ToString().Length)
                    {
                        var charList = Text.ToString().ToCharArray().ToList();
                        charList.RemoveAt(_textIndex);
                        _text = new string(charList.ToArray());
                        e.IsHandled = true;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    _textIndex = Math.Min(_textIndex + 1, Text.ToString().Length);
                    e.IsHandled = true;
                    break;

                case ConsoleKey.LeftArrow:
                    _textIndex = Math.Max(_textIndex - 1, 0);
                    e.IsHandled = true;
                    break;

                case ConsoleKey.Home:
                    _textIndex = 0;
                    e.IsHandled = true;
                    break;

                case ConsoleKey.End:
                    _textIndex = Text.ToString().Length;
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
                            charList.Insert(_textIndex, e.Key.KeyChar);
                            _text = new string(charList.ToArray());
                            _textIndex++;
                            e.IsHandled = true;
                        }
                    }
                    break;
            }
        }

        public override void Render(RenderContext context)
        {
            // todo: find a better way to do this
            var cursor = context.GetCursorPosition();

            var fore = ForeColor;
            var back = context.Focus == this ? BackColor.Brightness(.3f) : BackColor;
            var text = Text.ToString();
            if (string.IsNullOrEmpty(text))
            {
                text = Placeholder.ToString();
                fore = PlaceholderColor;
            }

            context.FillRectangle(new Rectangle(0, 0, context.Size.Width, 1), Color.Transparent, back);

            (string renderText, int renderIndex) = GetSurroundingText(text, _textIndex, context.Size.Width);
            context.Write(renderText, fore, back);

            cursor.Offset(renderIndex, 0);
            Cursor = cursor;
        }

        private static (string result, int resultIndex) GetSurroundingText(string input, int index, int maxCharacters)
        {
            if (input == null)
            {
                // Handle invalid input
                return (string.Empty, -1);
            }

            if (index < 0 || index > input.Length || maxCharacters <= 0)
            {
                // Handle out-of-bounds index or non-positive maxCharacters
                return (string.Empty, -1);
            }

            int startIndex = Math.Max(0, index - maxCharacters / 2);
            int endIndex = Math.Min(input.Length - 1, startIndex + maxCharacters - 1);

            // Adjust startIndex if needed to ensure maxCharacters characters are selected
            if (endIndex - startIndex + 1 < maxCharacters)
            {
                startIndex = Math.Max(0, endIndex - maxCharacters + 1);
            }

            string result = input.Substring(startIndex, endIndex - startIndex + 1);

            // Calculate the translation of index into the result string
            int resultIndex = index - startIndex;

            return (result, resultIndex);
        }
    }
}
