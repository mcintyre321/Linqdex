using Lucene.Net.Index;
using Lucene.Net.Linq.Mapping;
using Lucene.Net.Search;

namespace Linqdex
{
    internal class Key : IDocumentKey
    {
        private readonly string _key;
        public Key(string key)
        {
            _key = key;
        }


        public bool Equals(IDocumentKey other)
        {
            var otherPie = other as Key;
            if (otherPie != null)
            {
                return otherPie._key == this._key;
            }
            return false;
        }


        public Query ToQuery()
        {
            return new TermQuery(new Term("__key", this._key));
        }

        public bool Empty { get { return false; } }
    }
}