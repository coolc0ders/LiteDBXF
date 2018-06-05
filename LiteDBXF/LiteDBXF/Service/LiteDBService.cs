using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LiteDBXF.Service
{
    public class LiteDBService<T>
    {
        protected LiteCollection<T> _collection;

        public LiteDBService()
        {
            var db = new LiteDatabase(DependencyService.Get<IDataBaseAccess>().DatabasePath());
            _collection = db.GetCollection<T>();
        }

        public virtual T CreateItem(T item)
        {
            var val = _collection.Insert(item);
            return item;
        }
        public virtual T UpdateItem(T item)
        {
            _collection.Update(item);
            return item;
        }
        public virtual T DeleteItemAsync(T item)
        {
            var c = _collection.Delete(i => i.Equals(item));
            return item;
        }
        public virtual IEnumerable<T> ReadAllItems()
        {
            var all = _collection.FindAll();
            return new List<T>(all);
        }

    }
}
