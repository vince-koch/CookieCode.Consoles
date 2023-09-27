namespace CookieCode.Consoles.Tui
{
    public class BindSource<TValue>
    {
        private readonly TValue? _value;
        private readonly Func<TValue?>? _getValue;

        public BindSource(TValue? value)
        {
            _value = value;
        }

        public BindSource(Func<TValue?> getValue)
        {
            _getValue = getValue;
        }

        public virtual TValue? GetValue()
        {
            var result = _getValue != null ? _getValue() : _value;
            return result;
        }

        public override string ToString()
        {
            return GetValue()?.ToString() ?? string.Empty;
        }

        public static implicit operator BindSource<TValue>(TValue? value) => new BindSource<TValue>(value);

        public static implicit operator BindSource<TValue>(Func<TValue?> getText) => new BindSource<TValue>(getText);

        public static implicit operator TValue?(BindSource<TValue> bindSource) => bindSource.GetValue();
    }

    public class TextSource
    {
        private readonly string? _text;
        private readonly Func<string?>? _getTextFunc;

        public TextSource()
        {
            _text = string.Empty;
        }

        public TextSource(string? text)
        {
            _text = text;
        }

        public TextSource(Func<string?> getTextFunc)
        {
            _getTextFunc = getTextFunc;
        }

        public override string ToString()
        {
            var result = _getTextFunc?.Invoke()
                ?? _text
                ?? string.Empty;

            return result;
        }

        public static implicit operator TextSource(string? text) => new TextSource(text);

        public static implicit operator TextSource(Func<string?> getText) => new TextSource(getText);
    }
}
