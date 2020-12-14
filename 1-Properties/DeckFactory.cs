namespace Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A factory class for building <see cref="ISet{T}">decks</see> of <see cref="Card"/>s.
    /// </summary>
    public class DeckFactory
    {
        private string[] seeds;
        public IList<string> Seeds
        {
            get => this.seeds.ToList();
            set => this.seeds = value.ToArray();
        }

        private string[] names;

        public IList<string> Names
        {
            get => this.names.ToList();
            set => this.names = value.ToArray();
        }

        public int DeckSize => this.Names.Count * this.Seeds.Count;

        public ISet<Card> Deck
        {
            get
            {
                if (this.names == null || this.seeds == null)
                {
                    throw new InvalidOperationException();
                }

                return new HashSet<Card>(Enumerable
                    .Range(0, this.names.Length)
                    .SelectMany(i => Enumerable
                        .Repeat(i, this.seeds.Length)
                        .Zip(
                            Enumerable.Range(0, this.seeds.Length),
                            (n, s) => Tuple.Create(this.names[n], this.seeds[s], n)))
                    .Select(tuple => new Card(tuple))
                    .ToList());
            }
        }
    }
}
