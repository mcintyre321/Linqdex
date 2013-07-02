using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Linq;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace Linqdex
{
    public class Linqdex<T>  : IDisposable
    {
        private readonly LuceneDataProvider _provider;
        private readonly RAMDirectory _directory;
        readonly IDictionary<string, T> _objectLookup = new Dictionary<string, T>();
        private KeyReflectionDocumentMapper<T> _documentMapper;

        public Linqdex()
        {
            _directory = new RAMDirectory();
            _provider = new LuceneDataProvider(_directory, Version.LUCENE_30);
            _documentMapper = new KeyReflectionDocumentMapper<T>(s => _objectLookup[s], Version.LUCENE_30);
        }
        public void Add(T item)
        {
            AddRange(new[] {item});
        }
        public void AddRange(IEnumerable<T> items)
        {
            using (var session = _provider.OpenSession<T>(_documentMapper.Create, _documentMapper))
            {
                foreach (var item in items)
                {
                    var key = item.Key();
                    _objectLookup.Add(key, item);
                    session.Add(item);
                    var notify = item as INotifyPropertyChanged;
                    if (notify != null)
                    {
                        notify.PropertyChanged += notify_PropertyChanged;
                    }
                }
                session.Commit();
            }
        }

        private void notify_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            using (var session = _provider.OpenSession<T>(_documentMapper.Create, _documentMapper))
            {
                session.Delete((T) sender);
                session.Add((T) sender);
                session.Commit();
            }
            ;
        }

        public void Remove(T item)
        {
            RemoveRange(new []{item});
        }
        public void RemoveRange(IEnumerable<T> items)
        {
            using (var session = _provider.OpenSession<T>(_documentMapper.Create, _documentMapper))
            {
                foreach (var item in items)
                {
                    var key = item.Key();
                    if (_objectLookup.Remove(key))
                    {
                        session.Delete(item);
                        var notify = item as INotifyPropertyChanged;
                        if (notify != null)
                        {
                            notify.PropertyChanged -= notify_PropertyChanged;
                        }
                    }    
                }
                session.Commit();
            }
        }

        public IQueryable<T> ToIndexedQuery()
        {
            using (var s = _provider.OpenSession<T>(_documentMapper.Create, _documentMapper))
            {
                var indexQ = s.Query();
                return indexQ;
            }
        }

        public void Dispose()
        {
            _provider.Dispose();
        }


        public void Clear()
        {
            using (var s = _provider.OpenSession<T>(_documentMapper.Create, _documentMapper))
            {
                s.DeleteAll();
            }
        }
    }
}
