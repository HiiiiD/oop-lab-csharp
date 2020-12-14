namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private readonly IDictionary<TKey1, IDictionary<TKey2, TValue>> _values = new Dictionary<TKey1, IDictionary<TKey2, TValue>>();

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements => GetElements().Count;

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get => _values[key1][key2];
            set
            {
                if (!_values.ContainsKey(key1))
                {
                    _values.Add(key1, new Dictionary<TKey2, TValue>());
                }
                _values[key1].Add(key2, value);
            }
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            return _values[key1].Select(pair => new Tuple<TKey2, TValue>(pair.Key, pair.Value)).ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            return _values.Where(pair => pair.Value.ContainsKey(key2)).SelectMany(pair => pair.Value.Where(innerPair => innerPair.Key.Equals(key2)).Select(innerPair => new Tuple<TKey1, TValue>(pair.Key, innerPair.Value))).ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            return _values.SelectMany(pair => pair.Value.Select(value => new Tuple<TKey1, TKey2, TValue>(pair.Key, value.Key, value.Value))).ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            List<Tuple<TKey1, TKey2>> keys = keys1.SelectMany(item => keys2.Select(innerItem => new Tuple<TKey1, TKey2>(item, innerItem))).ToList();
            keys.ForEach(keys => this[keys.Item1, keys.Item2] = generator.Invoke(keys.Item1, keys.Item2));
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            return GetElements().SequenceEqual(other.GetElements());
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return base.Equals(obj);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            // TODO: improve
            return base.GetHashCode();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            return GetElements().Aggregate(new StringBuilder(), (strBuilder, currentTuple) => {
                return strBuilder.AppendLine($"Row: {currentTuple.Item1}, Column: {currentTuple.Item2}, Value: {currentTuple.Item3})");
            }).ToString();
        }
    }
}
