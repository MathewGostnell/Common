namespace Common.ExtensionLibrary;

public static class IEnumerableExtensions
{
    public static bool IsEmpty<TElement>(
        this IEnumerable<TElement> elementList)
        => !elementList.Any();

    public static bool IsNullOrEmpty<TElement>(
        this IEnumerable<TElement>? elementList)
        => elementList is null
            || elementList.IsEmpty();
}