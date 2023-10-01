using System.Drawing;

namespace CookieCode.Consoles
{
    public class Image : Array2D<Pixel>
    {
        public Image(Size size, Color foreColor, Color backColor, char? fill = null)
            : this(size.Width, size.Height, foreColor, backColor, fill)
        {
        }

        public Image(int width, int height, Color foreColor, Color backColor, char? fill = null)
            : base(width, height, Enumerable.Range(0, width * height)
                .Select(index => new Pixel(fill ?? ' ', foreColor, backColor))
                .ToArray())
        {
        }

        public Image(Image source)
            : base(source)
        {
        }
    }
}
