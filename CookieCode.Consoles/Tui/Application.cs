using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool IsExitRequested { get; set; }

        public Control? Focus { get; set; }

        public void Run()
        {
            IsExitRequested = false;

            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
                IsExitRequested = true;
            };

            Focus = this.Flatten().FirstOrDefault(control => control.CanFocus);
            Render();

            while (!IsExitRequested)
            {
                if (Console.KeyAvailable)
                {
                    var consoleKey = Console.ReadKey(true);
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
            Console.CursorVisible = false;
            Console.Clear();

            var context = new RenderContext { Focus = Focus };
            Render(context);

            if (Focus != null && Focus.CanFocus && Focus.Cursor != null)
            {
                Console.SetCursorPosition(Focus.Cursor.Value.X, Focus.Cursor.Value.Y);
                Console.CursorVisible = true;
            }
        }
    }

    public class RenderContext
    {
        public Control? Focus { get; set;  }

        public int Width { get; }

        public int Height { get; }

        public BufferChar this[int x, int y]
        {
            set
            {
                //Array[y * Width + x] = value;
            }
        }
    }
}
