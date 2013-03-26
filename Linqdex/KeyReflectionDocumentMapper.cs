using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Linq.Mapping;
using Lucene.Net.Search.Function;
using Version = Lucene.Net.Util.Version;

namespace Linqdex
{
    internal class KeyReflectionDocumentMapper<T> : ReflectionDocumentMapper<T>
    {
        public KeyReflectionDocumentMapper(Version version) : base(version)
        {
        }

        public KeyReflectionDocumentMapper(Version version, Analyzer externalAnalyzer) : base(version, externalAnalyzer)
        {
        }

        public override void ToDocument(T source, global::Lucene.Net.Documents.Document target)
        {
            base.ToDocument(source, target);
            target.Add(new Field("__key", source.Key(), Field.Store.YES, Field.Index.NOT_ANALYZED));
        }
        public override IDocumentKey ToKey(T source)
        {
            return new Key(source.Key());
        }

        public override void ToObject(global::Lucene.Net.Documents.Document source, global::Lucene.Net.Linq.IQueryExecutionContext context, T target)
        {
            var id = source.GetField("__key").StringValue;
            target.Key(id);
            base.ToObject(source, context, target);
        }
    }
}