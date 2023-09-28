using CookieCode.Consoles.Drivers;
using System.Diagnostics;
using System.Drawing;

namespace CookieCode.Consoles.Tui
{
    public class MultiDimensionalArray<TItem>
    {
        public int Width { get; }

        public int Height { get; }

        public TItem[] Array { get; }

        public TItem this[int x, int y]
        {
            get { return Array[y * Width + x]; }
            set { Array[y * Width + x] = value; }
        }

        public MultiDimensionalArray(int width, int height, TItem[] array)
        {
            if (array.Length != width * height)
            {
                throw new InvalidOperationException($"Expected array of length {width}*{height}={width * height}, but received length={array.Length}");
            }

            Width = width;
            Height = height;
            Array = array;
        }
    }

    public class BufferChar
    {
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public char Character { get; set; }

        public BufferChar(Color foreColor, Color backColor, char character)
        {
            ForeColor = foreColor;
            BackColor = backColor;
            Character = character;
        }
    }

    public class Buffer : MultiDimensionalArray<BufferChar>
    {
        public Buffer(int width, int height, Color foreColor, Color backColor, char character)
            : base(width, height, Enumerable.Range(0, width * height)
                .Select(index => new BufferChar(foreColor, backColor, character))
                .ToArray())
        {
        }
    }

    [DebuggerDisplay("Application")]
    public class Application : Container
    {
        private readonly IConsole _console;

        public bool IsExitRequested { get; set; }

        public Control? Focus { get; set; }

        public Size WindowSize { get; private set; }

        public Application(IConsole console)
        {
            _console = console;
        }

        public void Run()
        {
            IsExitRequested = false;

            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) =>
            {
                Debug.WriteLine($"CancelKeyPress", "Tui.Application");
                IsExitRequested = true; 
                e.Cancel = true;
            };

            WindowSize = _console.GetWindowSize();
            Focus = this.Flatten().FirstOrDefault(control => control.CanFocus);
            Render();

            while (!IsExitRequested)
            {
                Thread.Sleep(100);

                // process key events
                while (Console.KeyAvailable)
                {
                    var consoleKey = Console.ReadKey(true);
                    Debug.WriteLine($"KeyPressed: {consoleKey}", "Tui.Application");

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
                var windowSize = _console.GetWindowSize();
                if (WindowSize != windowSize)
                {
                    Debug.WriteLine($"WindowResize: {WindowSize} ==> {windowSize}", "Tui.Application");
                    WindowSize = windowSize;
                    Render();
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
            _console.Clear();

            var context = new RenderContext(_console, Focus);
            Render(context);

            if (Focus != null && Focus.CanFocus && Focus.Cursor != null)
            {
                _console.SetCursorPosition(Focus.Cursor.Value);
                _console.SetCursorVisible(true);
            }
        }
    }

    public class RenderContext
    {
        public IConsole Console { get; }

        public Control? Focus { get; }

        public RenderContext(
            IConsole console,
            Control? focus)
        {
            Console = console;
            Focus = focus;
        }

        public BufferChar this[int x, int y]
        {
            set
            {
                //Array[y * Width + x] = value;
            }
        }
    }
}
