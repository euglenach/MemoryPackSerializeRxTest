#nullable enable
using MemoryPack;
using UniRx;

public class ReactiveCollectionFormatter<TElement> : MemoryPackFormatter<ReactiveCollection<TElement>>
{
    public override void Serialize(ref MemoryPackWriter writer, ref ReactiveCollection<TElement>? value)
    {
        if (value == null)
        {
            writer.WriteNullCollectionHeader();
            return;
        }

        var formatter = writer.GetFormatter<TElement?>();

        writer.WriteCollectionHeader(value.Count);
        foreach (var item in value)
        {
            var v = item;
            formatter.Serialize(ref writer, ref v);
        }
    }

    public override void Deserialize(ref MemoryPackReader reader, ref ReactiveCollection<TElement>? value)
    {
        if (!reader.TryReadCollectionHeader(out var length))
        {
            value = default;
            return;
        }

        var formatter = reader.GetFormatter<TElement?>();

        var collection = new ReactiveCollection<TElement>();
        for (var i = 0; i < length; i++)
        {
            TElement? v = default;
            formatter.Deserialize(ref reader, ref v);
            collection.Add(v);
        }

        value = collection;
    }
}
