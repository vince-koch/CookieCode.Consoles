using System.Drawing;

namespace CookieCode.Consoles
{
    public static partial class ExtensionMethods
    {
        public static Point Center(this Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }

        public static Point Center(this Size size)
        {
            return new Point(size.Width / 2, size.Height / 2);
        }
    }
}
