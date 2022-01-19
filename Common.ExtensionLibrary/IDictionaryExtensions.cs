namespace Common.ExtensionLibrary
{
    using System.Collections.Generic;

    public static class IDictionaryExtensions
    {
        public static bool AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value = default)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return false;
            }

            dictionary.Add(
                key,
                value);
            return true;
        }
    }
}
