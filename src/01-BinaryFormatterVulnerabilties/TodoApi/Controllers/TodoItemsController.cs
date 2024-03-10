using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class TodoItemsController : ApiController
    {

        private readonly static SynchronizedCollection<TodoItem> _items = new SynchronizedCollection<TodoItem>();

        // GET: api/TodoItems
        public IEnumerable<TodoItem> Get()
        {
            return _items;
        }

        // GET: api/TodoItems/5
        public TodoItem Get(int id)
        {
            return _items[id];
        }

        // POST: api/TodoItems
        public TodoItem Post([FromBody] TodoItem value)
        {
            _items.Add(value);

            value.Id = _items.IndexOf(value);

            return value;
        }

        // PUT: api/TodoItems/5
        public HttpResponseMessage Put(int id, [FromBody] TodoItem value)
        {
            if (id != value.Id || id < 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (_items.Count > id)
            {
                _items[id] = value;

            }

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // DELETE: api/TodoItems/5
        //public void Delete(int id)
        //{
        //}
    }
}
