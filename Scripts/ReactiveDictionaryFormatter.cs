#nullable enable
using MemoryPack.Formatters;
using UniRx;

public class ReactiveDictionaryFormatter<TKey, TValue> : GenericDictionaryFormatterBase<ReactiveDictionary<TKey, TValue>,TKey, TValue>
    where TKey : notnull
{
    protected override ReactiveDictionary<TKey, TValue> CreateDictionary()
    {
        return new();
    }
}