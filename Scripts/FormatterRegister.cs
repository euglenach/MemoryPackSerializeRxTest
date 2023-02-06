using MemoryPack;
using UniRx;
using UnityEngine;

/// <summary>
/// MemoryPackFormatterProviderに登録するやつ
/// </summary>
public static class FormatterRegister
{
    private static bool registered;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Register()
    {
        if(registered) return;
        registered = true;
        
        // IL2CPPで死ぬかも
        MemoryPackFormatterProvider.RegisterGenericType(typeof(ReactiveProperty<>), typeof(ReactivePropertyFormatter<>));
        MemoryPackFormatterProvider.RegisterGenericType(typeof(ReactiveCollection<>), typeof(ReactiveCollectionFormatter<>));
        MemoryPackFormatterProvider.RegisterGenericType(typeof(ReactiveDictionary<,>), typeof(ReactiveDictionaryFormatter<,>));
        
        // 使うであろう型を一つ一つ列挙する
        MemoryPackFormatterProvider.Register(new ReactivePropertyFormatter<string>());
        MemoryPackFormatterProvider.Register(new ReactivePropertyFormatter<int>());
        MemoryPackFormatterProvider.Register(new ReactivePropertyFormatter<float>());
        MemoryPackFormatterProvider.RegisterCollection<ReactiveCollection<int>, int>();
        MemoryPackFormatterProvider.RegisterCollection<ReactiveCollection<float>, float>();
        MemoryPackFormatterProvider.RegisterCollection<ReactiveCollection<string>, string>();
        
        // etc...
    }
}
