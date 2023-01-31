#nullable enable
using MemoryPack.Formatters;
using UniRx;

/// <summary>
/// GenericDictionaryFormatterBaseを継承した実装方法
/// </summary>
public class ReactiveDictionaryFormatter<TKey, TValue> : GenericDictionaryFormatterBase<ReactiveDictionary<TKey, TValue>,TKey, TValue>
    where TKey : notnull
{
    protected override ReactiveDictionary<TKey, TValue> CreateDictionary()
    {
        return new();
    }
}