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
                .AddChild(new Label($" IsWindows={OperatingSystem.IsWindows()} "))
                .AddChild(new Label($" IsLinux={OperatingSystem.IsLinux()} "))
                .AddChild(new HorizontalRule())
                .AddChild(new Button("- 100", (s, e) => counter -= 100))
                .AddChild(new Button("- 10", (s, e) => counter -= 10))
                .AddChild(new Button("- 1", (s, e) => counter -= 1))
                .AddChild(new Label(new BindSource<string?>(() => $" Counter = {counter} ")))
                .AddChild(new Button("+ 1", (s, e) => counter += 1))
                .AddChild(new Button("+ 10", (s, e) => counter += 10))
                .AddChild(new Button("+ 100", (s, e) => counter += 100))
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