namespace CookieCode.Consoles.Tui.Controls
{
    public class HorizontalRule : Control
    {
        public override void Render(RenderContext context)
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(new string('-', Console.WindowWidth));
            Console.ResetColor();
        }
    }
}
