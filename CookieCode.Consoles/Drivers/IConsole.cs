using System.Drawing;

namespace CookieCode.Consoles.Drivers
{
    public interface IConsole
    {
        IConsole Clear();
        IConsole ResetColor();

        (int X, int Y) GetCursorPosition();
        bool GetCursorVisible();
        string GetTitle();
        int GetWindowHeight();
        int GetWindowWidth();
        
        IConsole SetCursorPosition(Point cursor);
        IConsole SetCursorPosition(int x, int y);
        IConsole SetCursorVisible(bool visible);
        IConsole SetTitle(string title);

        IConsole Write(string text, int x, int y, Color fore, Color back);
        IConsole Write(string text, Color fore, Color back);
        IConsole WriteLine();
        IConsole WriteLine(string text, Color fore, Color back);
    }
}
