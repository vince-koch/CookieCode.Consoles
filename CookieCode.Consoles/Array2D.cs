using System.Drawing;

namespace CookieCode.Consoles
{
    public class Array2D<T>
    {
        protected T[,] _items;
        private int _columns;
        private int _rows;

        public int Columns
        {
            get { return _columns; }
            set { Resize(value, Rows); }
        }

        public int Rows
        {
            get { return _rows; }
            set { Resize(Columns, value); }
        }

        public Size Size
        {
            get {  return new Size(Columns, Rows); }
        }

        public T this[int x, int y]
        {
            get => _items[x, y];
            set => _items[x, y] = value;
        }

        public T this[Point location]
        {
            get => _items[location.X, location.Y];
            set => _items[location.X, location.Y] = value;
        }

        public Array2D(int columns, int rows)
        {
            _columns = columns;
            _rows = rows;
            _items = new T[columns, rows];
        }

        public Array2D(int columns, int rows, T[] array)
        {
            if (array.Length != columns * rows)
            {
                throw new InvalidOperationException($"Expected array of length {columns}*{rows}={columns * rows}, but received length={array.Length}");
            }

            _columns = columns;
            _rows = rows;
            _items = new T[columns, rows];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int index = y * columns + x;
                    if (index < array.Length)
                    {
                        _items[x, y] = array[index];
                    }
                }
            }
        }

        public Array2D(Array2D<T> source)
            : this(source.Columns, source.Rows)
        {
            Copy(source, this);
        }

        public virtual T GetValue(int columnNumber, int rowNumber)
        {
            return _items[columnNumber, rowNumber];
        }

        public virtual Array2D<T> SetValue(int columnNumber, int rowNumber, T inputItem)
        {
            _items[columnNumber, rowNumber] = inputItem;
            return this;
        }

        public Array2D<T> Resize(int columns, int rows)
        {
            var grid = new T[columns, rows];
            var maxY = Math.Min(Rows, rows);
            var maxX = Math.Min(Columns, columns);

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    grid[x, y] = _items[x, y];
                }
            }

            _columns = columns;
            _rows = rows;
            _items = grid;

            return this;
        }

        public Point? Find(T item)
        {
            for (var y = 0; y < Rows; y++)
            {
                for (var x = 0; x < Columns; x++)
                {
                    var value = _items[x, y];
                    if (EqualityComparer<T>.Default.Equals(value, item))
                    {
                        return new Point(x, y);
                    }
                }
            }

            return null;
        }

        public Array2D<T> Swap(Point first, Point second)
        {
            T firstValue = GetValue(first.X, first.Y);
            T secondValue = GetValue(second.X, second.Y);

            SetValue(first.X, first.Y, secondValue);
            SetValue(second.X, second.Y, firstValue);

            return this;
        }

        public static void Copy(Array2D<T> source, Array2D<T> target)
        {
            Copy(
                source,
                new Rectangle(Point.Empty, source.Size),
                target,
                new Rectangle(Point.Empty, target.Size));
        }

        public static void Copy(Array2D<T> source, Rectangle sourceRect, Array2D<T> target, Rectangle targetRect)
        {
            for (int y = 0; y < Math.Min(sourceRect.Height, targetRect.Height); y++)
            {
                for (int x = 0; x < Math.Min(sourceRect.Width, targetRect.Width); x++)
                {
                    int sourceX = sourceRect.X + x;
                    int sourceY = sourceRect.Y + y;

                    int targetX = targetRect.X + x;
                    int targetY = targetRect.Y + y;

                    if (sourceX >= 0 && sourceX < source.Columns &&
                        sourceY >= 0 && sourceY < source.Rows &&
                        targetX >= 0 && targetX < target.Columns &&
                        targetY >= 0 && targetY < target.Rows)
                    {
                        target[targetX, targetY] = source[sourceX, sourceY];
                    }
                }
            }
        }
    }

    public static partial class ExtensionMethods
    {
        public static void ForEach<T>(this Array2D<T> grid, Action<int, int> action)
        {
            for (var y = 0; y < grid.Rows; y++)
            {
                for (var x = 0; x < grid.Columns; x++)
                {
                    var value = grid.GetValue(x, y);
                    action(x, y);
                }
            }
        }

        public static Point? GetFirstEmptyCell<T>(this Array2D<T> grid)
        {
            for (var y = 0; y < grid.Rows; y++)
            {
                for (var x = 0; x < grid.Columns; x++)
                {
                    var cell = grid.GetValue(x, y);
                    if (cell == null)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return null;
        }

        public static T? GetNextCell<T>(this Array2D<T> grid, T startingValue, Size gridDelta)
        {
            var point = grid.Find(startingValue);
            if (point == null)
            {
                return default(T);
            }

            var nextPoint = Point.Add(point.Value, gridDelta);

            var result = grid.IsValidPoint(nextPoint)
                ? grid[nextPoint]
                : default;

            return result;
        }

        public static bool IsValidPoint<T>(this Array2D<T> grid, Point point)
        {
            var isValid = point.X > -1 && point.X < grid.Columns
                && point.Y > -1 && point.Y < grid.Rows;
            return isValid;
        }
    }
}
