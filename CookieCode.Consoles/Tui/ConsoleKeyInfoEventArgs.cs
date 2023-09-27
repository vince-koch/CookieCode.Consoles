namespace CookieCode.Consoles.Tui
{
    public class ConsoleKeyInfoEventArgs
    {
        public bool IsHandled { get; set; } = false;

        public ConsoleKeyInfo Key { get; }

        public ConsoleKeyInfoEventArgs(ConsoleKeyInfo key)
        {
            Key = key;
        }
    }
}
