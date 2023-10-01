using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    public class Grid : Control
    {
        public Dimension[] ColumnWidths { get; }
        public Dimension[] RowHeights { get; }
        public Control[,] Children { get; }

        public Grid(int columns, int rows)
        {
            ColumnWidths = Enumerable.Range(0, columns).Select(i => Dimension.Auto()).ToArray();
            RowHeights = Enumerable.Range(0, rows).Select(i => Dimension.Auto()).ToArray();
            Children = new Control[ColumnWidths.Length, RowHeights.Length];
        }

        public Grid(Dimension[] columnWidths, Dimension[] rowHeights)
        {
            ColumnWidths = columnWidths;
            RowHeights = rowHeights;
            Children = new Control[ColumnWidths.Length, RowHeights.Length];
        }

        public Grid SetColumnWidths(params Dimension[] widths)
        {
            for (var x = 0; x < ColumnWidths.Length; x++)
            {
                ColumnWidths[x] = widths[x];
            }

            return this;
        }

        public Grid SetColumnWidth(int column,  Dimension width)
        {
            ColumnWidths[column] = width;
            return this;
        }

        public Grid SetRowHeights(params Dimension[] heights)
        {
            for (var x = 0; x < ColumnWidths.Length; x++)
            {
                RowHeights[x] = heights[x];
            }

            return this;
        }

        public Grid SetRowHeight(int column, Dimension height)
        {
            RowHeights[column] = height;
            return this;
        }

        public Grid AddChild(int x, int y, Control control)
        {
            Children[x, y] = control;
            return this;
        }

        public override void Render(RenderContext context)
        {
            var widths = CalculateAbsoluteValues(ColumnWidths, context.Size.Width);
            var heights = CalculateAbsoluteValues(RowHeights, context.Size.Height);

            for (var y = 0; y < RowHeights.Length; y++)
            {
                for (var x = 0; x < ColumnWidths.Length; x++)
                {
                    var child = Children[x, y];
                    if (child != null)
                    {
                        // TODO: account for extra colspans / rowspans
                        var childRectangle = new Rectangle(
                            widths.Take(x).Sum(),
                            heights.Take(y).Sum(),
                            widths[x],
                            heights[y]);

                        var childContext = new RenderContext(context, childRectangle);

                        child.Render(childContext);
                    }
                }
            }
        }

        private int[] CalculateAbsoluteValues(Dimension[] dimensions, int maxValue)
        {
            var absoluteValues = new int[dimensions.Length];

            // copy absolute widths
            dimensions.ForEach((dimension, index) =>
            {
                absoluteValues[index] = dimension.DimensionType == DimensionType.Absolute ? dimension.Value : 0;
            });

            // calculate percentage widths
            var remainingValue = maxValue - absoluteValues.Sum();
            dimensions.ForEach((dimension, index) =>
            {
                if (dimension.DimensionType == DimensionType.Percent)
                {
                    var percent = Math.Clamp(dimension.Value, 0, 100);
                    var width = remainingValue * (percent / (decimal)100);
                    absoluteValues[index] = (int)width;
                }
            });

            // calculate auto widths
            var autoCount = dimensions.Count(dimension => dimension.DimensionType == DimensionType.Auto);
            if (autoCount > 0)
            {
                var autoWidth = (maxValue - absoluteValues.Sum()) / autoCount;
                dimensions.ForEach((dimension, index) =>
                {
                    if (dimension.DimensionType == DimensionType.Auto)
                    {
                        absoluteValues[index] = autoWidth;
                    }
                });
            }

            return absoluteValues;
        }
    }
}
