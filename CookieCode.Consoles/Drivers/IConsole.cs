using System.Drawing;

namespace CookieCode.Consoles.Drivers
{
    public interface IConsole
    {
        IConsole Clear();
        IConsole ResetColor();

        //Point GetCursorPosition();
        (int Left, int Top) GetCursorPosition();
        bool GetCursorVisible();
        string GetTitle();
        Size GetWindowSize();
        Size GetBufferSize();

        IConsole SetCursorPosition(Point cursor);
        IConsole SetCursorPosition(int x, int y); 
        IConsole SetCursorPosition((int Left, int Top) position);
        
        IConsole SetCursorVisible(bool visible);
        IConsole SetTitle(string title);

        IConsole Write(string text, int x, int y, Color fore, Color back);
        IConsole Write(string text, Color fore, Color back);
        IConsole WriteLine();
        IConsole WriteLine(string text, Color fore, Color back);
    }
}
