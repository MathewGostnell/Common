namespace Common.ExtensionLibrary
{
    public static class StringExtensions
    {
        public static bool IsEqualTo(
            this string? value,
            string? target,
            bool isCaseSensitive = false,
            bool isWhiteSpaceSensitive = false)
        {
            if (value is null)
            {
                return target is null;
            }

            if (!isWhiteSpaceSensitive)
            {
                value = value.SafeTrim();
                target = target.SafeTrim();
            }

            return value.Equals(
                target,
                isCaseSensitive
                    ? StringComparison.InvariantCulture
                    : StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsNullOrEmpty(
            this string? value)
            => string.IsNullOrEmpty(value);

        public static bool IsNullOrWhiteSpace(
            this string? value)
            => string.IsNullOrWhiteSpace(value);

        public static string SafeTrim(
            this string? value,
            string? defaultValue = default)
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            => value.IsNullOrWhiteSpace()
                ? defaultValue ?? string.Empty
                : value.Trim();
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
