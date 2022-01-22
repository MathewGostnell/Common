namespace Common.ExtensionLibrary
{
    public static class IListExtensions
    {
        public static bool IsEmpty<TElement>(
            this IList<TElement> elementList)
            => !elementList.Any();

        public static bool IsNullOrEmpty<TElement>(
            this IList<TElement>? elementList)
            => elementList is null
                || elementList.IsEmpty();
    }
}
