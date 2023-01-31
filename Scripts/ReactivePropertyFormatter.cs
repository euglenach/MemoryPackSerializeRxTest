#nullable enable
using MemoryPack;
using UniRx;

/// <summary>
/// https://github.com/Cysharp/MemoryPack#serialize-external-types を参考に作成
/// </summary>
public class ReactivePropertyFormatter<T> : MemoryPackFormatter<ReactiveProperty<T>>
{
    public override void Serialize(ref MemoryPackWriter writer, ref ReactiveProperty<T>? value)
    {
        if (value == null)
        {
            writer.WriteNullObjectHeader();
            return;
        }

        var formatter = writer.GetFormatter<T>();
        var v = value.Value;
        formatter.Serialize(ref writer, ref v);
    }

    public override void Deserialize(ref MemoryPackReader reader, ref ReactiveProperty<T>? value)
    {
        if (reader.PeekIsNull())
        {
            value = null;
            return;
        }
        
        var formatter = reader.GetFormatter<T>();
        T? v = default;
        
        formatter.Deserialize(ref reader, ref v);
        value = new ReactiveProperty<T>(v);
    }
}