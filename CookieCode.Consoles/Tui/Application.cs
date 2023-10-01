using CookieCode.Consoles.Drivers;
using CookieCode.Core.Consoles;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace CookieCode.Consoles.Tui
{
    [DebuggerDisplay("Application")]
    public class Application : Container
    {
        private readonly IConsole _console;
        private readonly Color _foreColor;
        private readonly Color _backColor;
        private Image _screen;

        public bool IsExitRequested { get; set; }

        public Control? Focus { get; set; }

        public Size WindowSize { get; private set; }

        public Application(IConsole console, Color? foreColor = null, Color? backColor = null)
        {
            _console = console;
            _console.Clear();

            _foreColor = foreColor ?? Color.Gray;
            _backColor = backColor ?? Color.Black;

            var screen = new Image(
                console.GetWindowSize(),
                _foreColor,
                _backColor,
                ' ');

            RenderImage(screen);

            _screen = screen;
        }

        public void Run()
        {
            IsExitRequested = false;

            Console.TreatControlCAsInput = true;

            WindowSize = _console.GetWindowSize();
            Focus = this.Flatten().FirstOrDefault(control => control.CanFocus);
            Render();

            RunMessageLoop();

            _console.ResetColor();
            _console.Clear();
        }

        protected virtual void RunMessageLoop()
        {
            while (!IsExitRequested)
            {
                Thread.Sleep(100);

                // process key events
                while (Console.KeyAvailable)
                {
                    var consoleKey = Console.ReadKey(true);
                    Debug.WriteLine($"KeyPressed: {consoleKey}", "Tui.Application");

                    // check for ctrl+c
                    if (consoleKey.Key == ConsoleKey.C && consoleKey.Modifiers.HasControl())
                    {
                        return;
                    }

                    if (consoleKey.KeyChar == '?')
                    {
                        WriteImage(_screen);
                    }

                    var keyEventArgs = new ConsoleKeyInfoEventArgs(consoleKey);
                    var eventTarget = Focus ?? this;
                    while (keyEventArgs.IsHandled == false && eventTarget != null)
                    {
                        eventTarget.HandleKeyEvent(keyEventArgs);
                        if (!keyEventArgs.IsHandled)
                        {
                            eventTarget = eventTarget.Parent;
                        }
                    }

                    Render();
                }

                // pseudo window resize
                while (WindowSize != _console.GetWindowSize())
                {
                    try
                    {
                        var windowSize = _console.GetWindowSize();
                        Debug.WriteLine($"WindowResize: {WindowSize} ==> {windowSize}", "Tui.Application");

                        var screen = new Image(
                            windowSize,
                            _foreColor,
                            _backColor,
                            ' ');

                        if (windowSize == _console.GetWindowSize())
                        {
                            RenderImage(screen, true);
                            Render();

                            WindowSize = windowSize;
                        }
                    }
                    catch (Exception /* ignored */)
                    {
                    }
                }
            }
        }

        public override void HandleKeyEvent(ConsoleKeyInfoEventArgs e)
        {
            base.HandleKeyEvent(e);

            if (!e.IsHandled)
            {
                Debug.WriteLine($"Application is eating keypress {e}");
            }
        }

        public virtual void Render()
        {
            _console.SetCursorVisible(false);

            var screen = new Image(_screen);
            var clip = new Rectangle(Point.Empty, screen.Size);
            var context = new RenderContext(Focus, screen, clip);
            base.Render(context);
            RenderImage(screen);

            if (Focus != null && Focus.CanFocus && Focus.Cursor != null)
            {
                _console.SetCursorPosition(Focus.Cursor.Value);
                _console.SetCursorVisible(true);
            }

            _console.ResetColor();
        }

        protected void RenderImage(Image source, bool force = false)
        {
            if (source.Size != _console.GetWindowSize())
            {
                //throw new Exception("Image is not same size as console window");
                return;
            }

            var pixelsUpdated = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var priorValue = _console.GetCursorVisible();
            _console.SetCursorVisible(false);

            for (var y = 0; y < source.Rows; y++)
            {
                for (var x = 0; x < source.Columns; x++)
                {
                    var sourcePixel = source[x, y];

                    if (force
                        || _screen == null
                        || x >= _screen.Columns 
                        || y >= _screen.Rows 
                        || sourcePixel != _screen.GetValue(x, y))
                    {
                        _console.Write(sourcePixel.Character.ToString(), x, y, sourcePixel.ForeColor, sourcePixel.BackColor);
                        pixelsUpdated++;
                    }
                }
            }

            _console.SetCursorVisible(priorValue);

            _screen = new Image(source);

            stopwatch.Stop();
            Debug.WriteLine($"IConsole.DrawImage: pixels updated = {pixelsUpdated}; elapsed ms = {stopwatch.ElapsedMilliseconds}");
        }

        protected virtual void WriteImage(Image source)
        {
            var builder = new StringBuilder();

            builder.Append(Borders.Single.NW)
                .Append(new string(Borders.Single.Horizontal, source.Columns))
                .Append(Borders.Single.NE)
                .AppendLine();

            for (var y = 0; y < source.Rows; y++)
            {
                builder.Append(Borders.Single.Vertical);

                for (var x = 0; x < source.Columns; x++)
                {
                    builder.Append(source[x, y].Character);
                }

                builder.Append(Borders.Single.Vertical);
                builder.AppendLine();
            }

            builder.Append(Borders.Single.SW)
                .Append(new string(Borders.Single.Horizontal, source.Columns))
                .Append(Borders.Single.SE)
                .AppendLine();

            Debug.WriteLine(builder.ToString());
        }
    }
}
