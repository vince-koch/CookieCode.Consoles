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

            var headerGrid = new Grid(7, 1)
                .AddChild(0, 0, new Button("- 100", (s, e) => counter -= 100))
                .AddChild(1, 0, new Button("- 10", (s, e) => counter -= 10))
                .AddChild(2, 0, new Button("- 1", (s, e) => counter -= 1))
                .AddChild(3, 0, new Label(new BindSource<string?>(() => $" Counter = {counter} ")))
                .AddChild(4, 0, new Button("+ 1", (s, e) => counter += 1))
                .AddChild(5, 0, new Button("+ 10", (s, e) => counter += 10))
                .AddChild(6, 0, new Button("+ 100", (s, e) => counter += 100));

            var bodyGrid = new Grid(
                new Dimension[] { Dimension.Absolute(5), Dimension.Percent(25), Dimension.Auto(), Dimension.Percent(25), Dimension.Absolute(5) },
                new Dimension[] { Dimension.Absolute(5), Dimension.Percent(25), Dimension.Auto(), Dimension.Percent(25), Dimension.Absolute(5) });

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    bodyGrid.AddChild(x, y, new Label($"C{x} R{y}"));
                }
            }

            var mainGrid = new Grid(1, 3)
                .AddChild(0, 0, headerGrid)
                .AddChild(0, 1, bodyGrid);

            new Application(console, null, BootstrapColors.Purple500)
                .AddChild(mainGrid)
                .Run();

                //.AddChild(new Row()
                //    .AddChild(new Button("first", (s, e) => { }))
                //    .AddChild(new Button("second", (s, e) => { }))
                //    .AddChild(new Button("third", (s, e) => { })))
                //.AddChild(new HorizontalRule())
                //.AddChild(new Row()
                //    .AddChild(new Button("fourth", (s, e) => { }))
                //    .AddChild(new Button("fifth", (s, e) => { }))
                //    .AddChild(new Button("sixth", (s, e) => { }))
                //    .AddChild(new Button("seventh", (s, e) => { })))
                //.AddChild(new HorizontalRule())
                //.AddChild(new Row()
                //    .AddChild(new Label($" IsWindows={OperatingSystem.IsWindows()} ") { Width = Dimension.Percent(50) })
                //    .AddChild(new Label($" IsLinux={OperatingSystem.IsLinux()} ") { Width = Dimension.Percent(50) }))
                //.AddChild(new HorizontalRule())
                //.AddChild(new Row()
                //    .AddChild(new Button("- 100", (s, e) => counter -= 100))
                //    .AddChild(new Button("- 10", (s, e) => counter -= 10))
                //    .AddChild(new Button("- 1", (s, e) => counter -= 1))
                //    .AddChild(new Label(new BindSource<string?>(() => $" Counter = {counter} ")))
                //    .AddChild(new Button("+ 1", (s, e) => counter += 1))
                //    .AddChild(new Button("+ 10", (s, e) => counter += 10))
                //    .AddChild(new Button("+ 100", (s, e) => counter += 100)))
                //.AddChild(new HorizontalRule { ForeColor = DraculaColors.Orange })
                //.AddChild(new TextBox() { Placeholder = "Enter Text" })
                //.Run();
        }
    }
}