using System;
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
        private readonly Func<string, T> _findObject;

        public KeyReflectionDocumentMapper(Func<string, T> findObject, Version version) : base(version)
        {
            _findObject = findObject;
        }

        public KeyReflectionDocumentMapper(Func<string, T> findObject, Version version, Analyzer externalAnalyzer)
            : base(version, externalAnalyzer)
        {
            _findObject = findObject;
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

        public override IDocumentKey ToKey(Document document)
        {
            return new Key(document.GetField("__key").StringValue);
        }
        public override void ToObject(global::Lucene.Net.Documents.Document source, global::Lucene.Net.Linq.IQueryExecutionContext context, T target)
        {
            base.ToObject(source, context, target);
        }

        public T Create(IDocumentKey key)
        {
            var id = ((Key) key).Id;
            var source = _findObject(id);
            return source;
        }
    }
}