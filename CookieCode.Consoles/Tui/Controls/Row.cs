using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    public class Row : Container
    {
        public override void Render(RenderContext context)
        {
            var windowSize = context.Size;
            var cellWidths = CalculateCellWidths(windowSize.Width);

            //var position = context.Console.GetCursorPosition();
            var children = Children.ToList();

            for (var i = 0; i < children.Count; i++)
            {
                var child = children[i];
                if (child.IsVisible)
                {
                    var x = cellWidths.Take(i).Sum();
                    var width = cellWidths[i];

                    var childContext = new RenderContext(context, new Rectangle(x, 0, width, 1));
                    child.Render(childContext);
                }
            }
        }

        private int[] CalculateCellWidths(int containerWidth)
        {
            var children = Children.ToArray();
            var cellWidths = new int[children.Length];

            // copy absolute widths
            children.ForEach((control, index) =>
            {
                cellWidths[index] = control.Width.DimensionType == DimensionType.Absolute ? control.Width.Value : 0;
            });

            // calculate percentage widths
            var remainingWidth = containerWidth - cellWidths.Sum();
            children.ForEach((control, index) =>
            {
                if (control.Width.DimensionType == DimensionType.Percent)
                {
                    var percent = Math.Clamp(control.Width.Value, 0, 100);
                    var width = remainingWidth * (percent / (decimal)100);
                    cellWidths[index] = (int)width;
                }
            });

            // calculate auto widths
            var autoCount = children.Count(control => control.Width.DimensionType == DimensionType.Auto);
            if (autoCount > 0)
            {
                var autoWidth = (containerWidth - cellWidths.Sum()) / autoCount;
                children.ForEach((control, index) =>
                {
                    if (control.Width.DimensionType == DimensionType.Auto)
                    {
                        cellWidths[index] = autoWidth;
                    }
                });
            }

            return cellWidths;
        }
    }
}
