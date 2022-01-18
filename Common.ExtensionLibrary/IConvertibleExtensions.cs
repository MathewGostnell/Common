namespace Common.ExtensionLibrary
{
    using System;

    public static class IConvertibleExtensions
    {
        public static TResult? As<TResult>(
            this IConvertible? source,
            TResult? defaultValue = default)
            where TResult : IConvertible
        {
            if (source is null
                || source is string sourceString
                    && sourceString.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            var resultType = typeof(TResult);
            return (TResult)Convert.ChangeType(
                source,
                Nullable.GetUnderlyingType(resultType)
                    ?? resultType);
        }
    }
}
