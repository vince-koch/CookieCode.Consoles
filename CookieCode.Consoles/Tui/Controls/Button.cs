﻿using CookieCode.Consoles.Colors;
using System.Diagnostics;
using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    [DebuggerDisplay("Button [{Text}]")]
    public class Button : Control
    {
        public event EventHandler<Button>? Click;

        public override bool CanFocus => true;

        public Color BackColor { get; set; } = BootstrapColors.Primary;

        public BindSource<string?> Text { get; set; } = "Button";

        public Button()
        {
        }

        public Button(BindSource<string?> text, EventHandler<Button> click)
        {
            Text = text;
            Click += click;
        }

        public Button SetColor(Color backColor)
        {
            BackColor = backColor;
            return this;
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
            var back = context.Focus == this ? BackColor.Brightness(.3f) : BackColor;
            var fore = back.FgForBg();

            context.WriteText(
                text: $"[ {Text} ]",
                foreColor: back.FgForBg(),
                backColor: back);

            // todo: find a better way to do this
            var point = context.GetCursorPosition();
            point.Offset(-1, 0);
            Cursor = point;
        }
    }
}
