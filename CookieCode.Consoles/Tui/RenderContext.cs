using System.Drawing;

namespace CookieCode.Consoles.Tui
{
    public class RenderContext
    {
        private readonly Image _image;
        private readonly Rectangle _clip;

        public Size Size => _clip.Size; 

        public Control? Focus { get; }

        public RenderContext(
            Control? focus,
            Image image,
            Rectangle clip)
        {
            Focus = focus;
            _image = image;
            _clip = clip;
        }

        public RenderContext(
            RenderContext context,
            Rectangle clip)
        {
            Focus = context.Focus;
            _image = context._image;
            _clip = clip;
            _clip.Offset(context._clip.Location);
            _clip.Intersect(new Rectangle(Point.Empty, _image.Size));
        }

        public Pixel? this[int x, int y]
        {
            get => this[new Point(x, y)];
            set => this[new Point(x, y)] = value;
        }

        public Pixel? this[Point point]
        {
            get
            {
                point.Offset(_clip.Location);
                Pixel? result = _clip.Contains(point)
                    ? _image[point.X, point.Y]
                    : null;

                return result;
            }

            set
            {
                if (value != null)
                {
                    point.Offset(_clip.Location);
                    if (_clip.Contains(point))
                    {
                        _image[point.X, point.Y] = value.Value;
                    }
                }
            }
        }
    }

    public static class RenderContextExtensions
    {
        public static RenderContext Write(this RenderContext context, string? text, int x = 0, int y = 0)
        {
            context.Write(text, x, y, Color.Transparent, Color.Transparent);
            return context;
        }

        public static RenderContext Write(this RenderContext context, string? text, Color? foreColor = null, Color? backColor = null)
        {
            context.Write(text, 0, 0, foreColor, backColor);
            return context;
        }

        public static RenderContext Write(this RenderContext context, string? text, int x = 0, int y = 0, Color? foreColor = null, Color? backColor = null)
        {
            if (!string.IsNullOrEmpty(text))
            {
                for (var i = 0; i < text.Length; i++)
                {
                    context[x + i, y] = new Pixel(text[i], foreColor, backColor);
                }
            }

            return context;
        }

        public static RenderContext FillRectangle(this RenderContext context, Rectangle rectangle, Color? foreColor = null, Color? backColor = null, char? c = null)
        {
            for (int y = rectangle.Top; y < rectangle.Bottom; y++)
            {
                for (int x = rectangle.Left; x < rectangle.Right; x++) 
                {
                    context[x, y] = new Pixel(c ?? ' ', foreColor, backColor);
                }
            }

            return context;
        }
    }
}
