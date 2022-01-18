namespace Common.ExtensionLibrary
{
    public static class IComparableExtensions
    {
        public static bool IsGreaterThan<TComparable>(
            this TComparable source,
            TComparable target)
            where TComparable : IComparable<TComparable>
            => source.CompareTo(target) > 0;

        public static bool IsGreaterThanOrEqualTo<TComparable>(
            this TComparable source,
            TComparable target)
            where TComparable : IComparable<TComparable>
            => !source.IsLessThan(target);

        public static bool IsLessThan<TComparable>(
            this TComparable source,
            TComparable target)
            where TComparable : IComparable<TComparable>
            => source.CompareTo(target) < 0;

        public static bool IsLessThanOrEqualTo<TComparable>(
            this TComparable source,
            TComparable target)
            where TComparable : IComparable<TComparable>
            => !source.IsGreaterThan(target);
    }
}
