using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Index;
using Lucene.Net.Linq.Mapping;
using Lucene.Net.Search;

namespace Linqdex
{
    internal class Key : IDocumentKey
    {
        public string Id { get; }
        public Key(string id)
        {
            Id = id;
        }


        public bool Equals(IDocumentKey other)
        {
            var otherPie = other as Key;
            if (otherPie != null)
            {
                return otherPie.Id == this.Id;
            }
            return false;
        }


        public Query ToQuery()
        {
            return new TermQuery(new Term("__key", this.Id));
        }

        public bool Empty => false;

        public IEnumerable<string> Properties => Enumerable.Empty<string>();

        public object this[string property] => null;
    }
}