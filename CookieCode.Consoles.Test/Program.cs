using CookieCode.Consoles.Colors;
using CookieCode.Consoles.Drivers;
using CookieCode.Consoles.Tui;
using CookieCode.Consoles.Tui.Controls;

namespace CookieCode.Consoles.Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var counter = 0;

            new Application(new AnsiConsole())
                .AddChild(new Row()
                    .AddChild(new Button("first", (s, e) => { }))
                    .AddChild(new Button("second", (s, e) => { }))
                    .AddChild(new Button("third", (s, e) => { })))
                .AddChild(new HorizontalRule())
                .AddChild(new Row()
                    .AddChild(new Button("fourth", (s, e) => { }))
                    .AddChild(new Button("fifth", (s, e) => { }))
                    .AddChild(new Button("sixth", (s, e) => { }))
                    .AddChild(new Button("seventh", (s, e) => { })))
                .AddChild(new HorizontalRule())
                .AddChild(new Row()
                    .AddChild(new Label($" IsWindows={OperatingSystem.IsWindows()} ") { Width = Dimension.Percent(50) })
                    .AddChild(new Label($" IsLinux={OperatingSystem.IsLinux()} ") { Width = Dimension.Percent(50) }))
                .AddChild(new HorizontalRule())
                .AddChild(new Row()
                    .AddChild(new Button("- 100", (s, e) => counter -= 100))
                    .AddChild(new Button("- 10", (s, e) => counter -= 10))
                    .AddChild(new Button("- 1", (s, e) => counter -= 1))
                    .AddChild(new Label(new BindSource<string?>(() => $" Counter = {counter} ")))
                    .AddChild(new Button("+ 1", (s, e) => counter += 1))
                    .AddChild(new Button("+ 10", (s, e) => counter += 10))
                    .AddChild(new Button("+ 100", (s, e) => counter += 100)))
                .AddChild(new HorizontalRule { ForeColor = DraculaColors.Orange })
                .AddChild(new TextBox() { Placeholder = "Enter Text" })
                .Run();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Application has exited cleanly");
            Console.ResetColor();
        }
    }
}