using System;
using System.Collections.Generic;
using System.Linq;
using MemoryPack;
using NUnit.Framework;
using UniRx;
using UnityEditor;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

namespace Tests
{
    public class MemoryPackSerializeTest
    {
        [SetUp]
        public void SetUp()
        {
            // フォーマッターの登録
            FormatterRegister.Register();
        }
        
        /// <summary>
        /// ReactiveProperty
        /// </summary>
        [Test]
        public void ReactivePropertySerializeTest()
        {
            var p1 = new ReactiveProperty<int>(100);
            var p2 = new ReactiveProperty<float>(11.111f);
            var p3 = new ReactiveProperty<string>("MemoryPack");

            var b1 = MemoryPackSerializer.Serialize(p1);
            var b2 = MemoryPackSerializer.Serialize(p2);
            var b3 = MemoryPackSerializer.Serialize(p3);

            var d1 = MemoryPackSerializer.Deserialize<ReactiveProperty<int>>(b1);
            var d2 = MemoryPackSerializer.Deserialize<ReactiveProperty<float>>(b2);
            var d3 = MemoryPackSerializer.Deserialize<ReactiveProperty<string>>(b3);
            
            Assert.That(p1.Value, Is.EqualTo(d1.Value));
            Assert.That(p2.Value, Is.EqualTo(d2.Value));
            Assert.That(p3.Value, Is.EqualTo(d3.Value));
        }
        
        /// <summary>
        /// ReactiveCollection
        /// </summary>
        [Test]
        public void ReactiveCollectionSerializeTest([Range(0,10)] int length)
        {
            var c1 = Enumerable.Range(Random.Range(0, length), Random.Range(10, 100)).ToReactiveCollection();
            var c2 = Enumerable.Range(Random.Range(0, length), Random.Range(10, 100)).Select(_ => Random.value - .5f).ToReactiveCollection();
            var c3 = Enumerable.Range(Random.Range(0, length), Random.Range(10, 100)).Select(x => x.ToString()).ToReactiveCollection();
            
            ReactiveCollectionSerializeTestCore(c1);
            ReactiveCollectionSerializeTestCore(c2);
            ReactiveCollectionSerializeTestCore(c3);
        }

        void ReactiveCollectionSerializeTestCore<T>(ReactiveCollection<T> collection)
        {
            var b = MemoryPackSerializer.Serialize(collection);
            var d = MemoryPackSerializer.Deserialize<ReactiveCollection<T>>(b);
            
            Assert.That(collection.Count, Is.EqualTo(d.Count));

            for(var i = 0; i < collection.Count; i++)
            {
                Assert.That(collection[i], Is.EqualTo(d[i]));
            }
        }

        /// <summary>
        /// ReactiveDictionary
        /// </summary>
        [Test]
        public void ReactiveDictionarySerializeTest()
        {
            var d1 = new ReactiveDictionary<string, int>();
            var d2 = new ReactiveDictionary<EEnum, float>();

            var count = Enum.GetValues(typeof(EEnum)).Length;
            for(var i = 0; i < count; i++)
            {
                d1.Add(i.ToString(), Random.Range(10,100));
                d2.Add((EEnum)i, Random.value - .5f);
            }

            ReactiveCollectionDictionaryTestCore(d1);
            ReactiveCollectionDictionaryTestCore(d2);
        }
        
        void ReactiveCollectionDictionaryTestCore<TKey, TValue>(ReactiveDictionary<TKey, TValue> dictionary)
        {
            var b = MemoryPackSerializer.Serialize(dictionary);
            var d = MemoryPackSerializer.Deserialize<ReactiveDictionary<TKey, TValue>>(b);
            
            Assert.That(dictionary.Count, Is.EqualTo(d.Count));

            for(var i = 0; i < dictionary.Count; i++)
            {
                Assert.That(dictionary.Keys.ElementAt(i), Is.EqualTo(d.Keys.ElementAt(i)));
                Assert.That(dictionary.Values.ElementAt(i), Is.EqualTo(d.Values.ElementAt(i)));
            }
        }
        
        enum EEnum{A,B,C}
    }
}
