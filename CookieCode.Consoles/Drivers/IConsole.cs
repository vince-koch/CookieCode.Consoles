using System.Drawing;

namespace CookieCode.Consoles.Drivers
{
    public interface IConsole
    {
        public bool IsCursorVisible { get; set; }
        
        public Point Cursor { get; set; }
    }
}
