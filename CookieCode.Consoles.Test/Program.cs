using CookieCode.Consoles.Tui;
using CookieCode.Consoles.Tui.Controls;

namespace CookieCode.Consoles.Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var counter = 0;

            new Application()
                .AddChild(new Button("- 100", (s, e) => counter -= 100))
                .AddChild(new Button("- 10", (s, e) => counter -= 10))
                .AddChild(new Button("- 1", (s, e) => counter -= 1))
                .AddChild(new Label(new BindSource<string?>(() => $" Counter = {counter} ")))
                .AddChild(new Button("+ 1", (s, e) => counter += 1))
                .AddChild(new Button("+ 10", (s, e) => counter += 10))
                .AddChild(new Button("+ 100", (s, e) => counter += 100))
                .AddChild(new TextBox() { Placeholder = "Enter Text" })
                .Run();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Application has exited cleanly");
            Console.ResetColor();
        }
    }
}