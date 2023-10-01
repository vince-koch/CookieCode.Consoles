using System.Drawing;

namespace CookieCode.Consoles
{
    public struct Pixel
    {
        public char Character { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        public Pixel(char character, Color? foreColor = null, Color? backColor = null)
        {
            Character = character; 
            ForeColor = foreColor ?? Color.Transparent;
            BackColor = backColor ?? Color.Transparent;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Pixel))
            {
                return false;
            }

            var result = Equals(this, (Pixel)obj);
            return result;
        }

        public override int GetHashCode() => (BackColor, ForeColor, Character).GetHashCode();
        public static bool operator ==(Pixel? left, Pixel? right) => Equals(left, right);
        public static bool operator !=(Pixel? left, Pixel? right) => !Equals(left, right);

        public static bool Equals(Pixel? left, Pixel? right)
        {
            //if (ReferenceEquals(left, right))
            //{
            //    return true;
            //}

            var areEqual = left?.BackColor == right?.BackColor
                && left?.ForeColor == right?.ForeColor
                && left?.Character == right?.Character;

            return areEqual;
        }
    }
}
