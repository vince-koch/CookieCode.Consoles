using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieCode.Consoles.Tui.Controls
{
    public class Row : Control
    {
        private readonly List<Dimension> _columnWidths;
        private readonly List<Control> _children;

        public Row() : this(0)
        {
        }

        public Row(int columns) : this(Enumerable.Range(0, columns).Select(i => Dimension.Auto()))
        {
            
        }

        public Row(IEnumerable<Dimension> columnWidths)
        {
            _columnWidths = columnWidths.ToList();
            _children = new List<Control>(_columnWidths.Count);
        }

        public Row SetColumnWidths(params Dimension[] widths)
        {
            for (var x = 0; x < _columnWidths.Count; x++)
            {
                _columnWidths[x] = widths[x];
            }

            return this;
        }

        public Row SetColumnWidth(int column, Dimension width)
        {
            _columnWidths[column] = width;
            return this;
        }

        public Row SetChild(int x, Control control)
        {
            _children[x] = control;
            return this;
        }

        public Row AddChild(Control control, Dimension? dimension = null)
        {
            _columnWidths.Add(dimension ?? Dimension.Auto());
            _children.Add(control);
            return this;
        }

        public override void Render(RenderContext context)
        {
            var widths = Dimension.CalculateAbsoluteValues(_columnWidths, context.Size.Width);

            for (var x = 0; x < _columnWidths.Count; x++)
            {
                var child = _children[x];
                if (child != null)
                {
                    // TODO: account for extra colspans / rowspans
                    var childRectangle = new Rectangle(
                        widths.Take(x).Sum(),
                        0,
                        widths[x],
                        context.Size.Height);

                    var childContext = new RenderContext(context, childRectangle);

                    child.Render(childContext);
                }
            }
        }
    }
}
