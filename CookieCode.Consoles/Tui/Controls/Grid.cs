using System.Drawing;

namespace CookieCode.Consoles.Tui.Controls
{
    public class Grid : Container
    {
        private Dimension[] _columnWidths;
        private Dimension[] _rowHeights;
        private Array2D<Control> _children;

        public override IEnumerable<Control> Children => _children;

        public Grid(int columns, int rows)
        {
            _columnWidths = Enumerable.Range(0, columns).Select(i => Dimension.Auto()).ToArray();
            _rowHeights = Enumerable.Range(0, rows).Select(i => Dimension.Auto()).ToArray();
            _children = new Array2D<Control>(_columnWidths.Length, _rowHeights.Length);
        }

        public Grid(Dimension[] columnWidths, Dimension[] rowHeights)
        {
            _columnWidths = columnWidths;
            _rowHeights = rowHeights;
            _children = new Array2D<Control>(_columnWidths.Length, _rowHeights.Length);
        }

        public Grid SetColumnWidths(params Dimension[] widths)
        {
            for (var x = 0; x < _columnWidths.Length; x++)
            {
                _columnWidths[x] = widths[x];
            }

            return this;
        }

        public Grid SetColumnWidth(int column,  Dimension width)
        {
            _columnWidths[column] = width;
            return this;
        }

        public Grid SetRowHeights(params Dimension[] heights)
        {
            for (var x = 0; x < _columnWidths.Length; x++)
            {
                _rowHeights[x] = heights[x];
            }

            return this;
        }

        public Grid SetRowHeight(int column, Dimension height)
        {
            _rowHeights[column] = height;
            return this;
        }

        public Grid SetChild(int x, int y, Control control)
        {
            _children[x, y] = control;
            control.Parent = this;
            return this;
        }

        public override void Render(RenderContext context)
        {
            var widths = Dimension.CalculateAbsoluteValues(_columnWidths, context.Size.Width);
            var heights = Dimension.CalculateAbsoluteValues(_rowHeights, context.Size.Height);

            for (var y = 0; y < _rowHeights.Length; y++)
            {
                for (var x = 0; x < _columnWidths.Length; x++)
                {
                    var child = _children[x, y];
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
    }
}
