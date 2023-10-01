using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieCode.Consoles.Tui.Controls
{
    public class Column : Control
    {
        private readonly List<Dimension> _rowHeights;
        private readonly List<Control> _children;

        public Column() : this(0)
        {
        }

        public Column(int rows) : this(Enumerable.Range(0, rows).Select(i => Dimension.Auto()))
        {
        }

        public Column(IEnumerable<Dimension> rowHeights)
        {
            _rowHeights = rowHeights.ToList();
            _children = new List<Control>(_rowHeights.Count);
        }

        public Column SetRowHeights(params Dimension[] widths)
        {
            for (var x = 0; x < _rowHeights.Count; x++)
            {
                _rowHeights[x] = widths[x];
            }

            return this;
        }

        public Column SetColumnWidth(int column, Dimension width)
        {
            _rowHeights[column] = width;
            return this;
        }

        public Column SetChild(int x, Control control)
        {
            _children[x] = control;
            return this;
        }

        public Column AddChild(Control control, Dimension? dimension = null)
        {
            _rowHeights.Add(dimension ?? Dimension.Auto());
            _children.Add(control);
            return this;
        }

        public override void Render(RenderContext context)
        {
            var heights = Dimension.CalculateAbsoluteValues(_rowHeights, context.Size.Width);

            for (var y = 0; y < _rowHeights.Count; y++)
            {
                var child = _children[y];
                if (child != null)
                {
                    // TODO: account for extra colspans / rowspans
                    var childRectangle = new Rectangle(                        
                        0,
                        heights.Take(y).Sum(),
                        context.Size.Width,
                        heights[y]);

                    var childContext = new RenderContext(context, childRectangle);

                    child.Render(childContext);
                }
            }
        }
    }
}
