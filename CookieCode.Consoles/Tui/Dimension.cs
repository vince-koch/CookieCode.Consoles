namespace CookieCode.Consoles.Tui
{
    public enum DimensionType
    {
        Absolute,
        Percent,
        Auto
    }

    public class Dimension
    {
        public DimensionType DimensionType { get; }
        public int Value { get; }

        public Dimension(DimensionType dimensionType, int value)
        {
            DimensionType = dimensionType;
            Value = value;
        }

        public static Dimension Absolute(int value) => new Dimension(DimensionType.Absolute, value);
        public static Dimension Percent(int value) => new Dimension(DimensionType.Percent, value);
        public static Dimension Auto() => new Dimension(DimensionType.Auto, -1);

        public override bool Equals(object? obj) => Equals(this, obj as Dimension);
        public override int GetHashCode() => (DimensionType, Value).GetHashCode(); 
        public static bool operator ==(Dimension? left, Dimension? right) => Equals(left, right);
        public static bool operator !=(Dimension? left, Dimension? right) => !Equals(left, right);

        public static bool Equals(Dimension? left, Dimension? right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            var areEqual = left?.DimensionType == right?.DimensionType
                && left?.Value == right?.Value;

            return areEqual;
        }

        public static int[] CalculateAbsoluteValues(IEnumerable<Dimension> dimensions, int maxValue)
        {
            var absoluteValues = new int[dimensions.Count()];

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
