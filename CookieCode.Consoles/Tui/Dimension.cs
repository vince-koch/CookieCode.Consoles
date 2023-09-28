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
    }
}
