using MemoryPack;
using UniRx;
using UnityEngine;

public static class FormatterRegister
{
    private static bool registered;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Register()
    {
        if(registered) return;
        registered = true;
            
        MemoryPackFormatterProvider.RegisterGenericType(typeof(ReactiveProperty<>), typeof(ReactivePropertyFormatter<>));
        MemoryPackFormatterProvider.RegisterGenericType(typeof(ReactiveCollection<>), typeof(ReactiveCollectionFormatter<>));
        MemoryPackFormatterProvider.RegisterGenericType(typeof(ReactiveDictionary<,>), typeof(ReactiveDictionaryFormatter<,>));
        
        // 一つ一つ列挙するのはダルいしいろいろ問題ある
        // MemoryPackFormatterProvider.Register(new ReactivePropertyFormatter<string>());
        // MemoryPackFormatterProvider.Register(new ReactivePropertyFormatter<int>());
        // MemoryPackFormatterProvider.Register(new ReactivePropertyFormatter<float>());
        // MemoryPackFormatterProvider.RegisterCollection<ReactiveCollection<int>, int>();
        // MemoryPackFormatterProvider.RegisterCollection<ReactiveCollection<float>, float>();
        // MemoryPackFormatterProvider.RegisterCollection<ReactiveCollection<string>, string>();
        // MemoryPackFormatterProvider.RegisterCollection(typeof(ReactiveCollection<>));  // これはNG
        // MemoryPackFormatterProvider.RegisterDictionary(typeof(ReactiveDictionary<,>)); // これもNG
        
        // etc...
    }
}
