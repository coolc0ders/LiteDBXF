using LiteDB;
using LiteDBXF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteDBXF.Service
{
    public class TodoLiteDBService : LiteDBService<Todo>
    {
        public TodoLiteDBService()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Todo>()
                .Id(x => x.ID);
        }
        public override Todo CreateItem(Todo item)
        {
            item.ID = Guid.NewGuid().ToString();
            return base.CreateItem(item);
        }

        public override Todo DeleteItemAsync(Todo item)
        {
            var c = _collection.Delete(i => i.ID == item.ID);
            return c == 0 ? null : item;
        }

        public override Todo UpdateItem(Todo item)
        {
            return base.UpdateItem(item);
        }

        public override IEnumerable<Todo> ReadAllItems()
        {
            return base.ReadAllItems();
        }
    }
}
