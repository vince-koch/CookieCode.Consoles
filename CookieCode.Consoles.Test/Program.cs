using CookieCode.Consoles.Colors;
using CookieCode.Consoles.Drivers;
using CookieCode.Consoles.Tui;
using CookieCode.Consoles.Tui.Controls;

namespace CookieCode.Consoles.Test
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var console = new AnsiConsole();

            RunApplication(console);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Application has exited cleanly");
            Console.ResetColor();
        }

        private static void RunApplication(IConsole console)
        {
            var counter = 0;

            var headerRow = new Row()
                .AddChild(new Button("- 100", (s, e) => counter -= 100).SetColor(DraculaColors.Cyan))
                .AddChild(new Button("- 10", (s, e) => counter -= 10).SetColor(DraculaColors.Purple))
                .AddChild(new Button("- 1", (s, e) => counter -= 1).SetColor(DraculaColors.Orange))
                .AddChild(new Label(new BindSource<string?>(() => $" Counter = {counter} ")))
                .AddChild(new Button("+ 1", (s, e) => counter += 1).SetColor(DraculaColors.Orange))
                .AddChild(new Button("+ 10", (s, e) => counter += 10).SetColor(DraculaColors.Purple))
                .AddChild(new Button("+ 100", (s, e) => counter += 100).SetColor(DraculaColors.Cyan));

            var bodyGrid = new Grid(
                new Dimension[] { Dimension.Absolute(5), Dimension.Percent(25), Dimension.Auto(), Dimension.Percent(25), Dimension.Absolute(5) },
                new Dimension[] { Dimension.Absolute(5), Dimension.Percent(25), Dimension.Auto(), Dimension.Percent(25), Dimension.Absolute(5) });

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (x == 2 && y == 2)
                    {
                        bodyGrid.SetChild(x, y, new GroupBox($"Hello Kitty").SetColor(DraculaColors.Red)
                            .SetChild(new TextBox()
                                .Configure(c => c.Text = "Meow")
                                .Configure(c => c.Placeholder = "What does the dog say?")));
                    }
                    else
                    {
                        bodyGrid.SetChild(x, y, new Label($"C{x} R{y}").SetColor(DraculaColors.Green));
                    }
                }
            }

            var document = new Column()
                .AddChild(headerRow, Dimension.Absolute(1))
                .AddChild(new HorizontalRule().SetColor(DraculaColors.Pink), Dimension.Absolute(1))
                .AddChild(bodyGrid);

            new Application(console, DraculaColors.Foreground, DraculaColors.Background)
                .SetChild(document)
                .Run();
        }
    }
}