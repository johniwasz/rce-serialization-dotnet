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
    public class TodoItemsFixedController : ApiController
    {

        private readonly static SynchronizedCollection<TodoItemFixed> _items = new SynchronizedCollection<TodoItemFixed>();

        // GET: api/TodoItemsFixed
        public IEnumerable<TodoItemFixed> Get()
        {
            return _items;
        }

        // GET: api/TodoItemsFixed/5
        public TodoItemFixed Get(int id)
        {
            return _items[id];
        }

        // POST: api/TodoItemsFixed
        public TodoItemFixed Post([FromBody] TodoItemFixed value)
        {
            _items.Add(value);

            value.Id = _items.IndexOf(value);

            return value;
        }

        // PUT: api/TodoItemsFixed/5
        public HttpResponseMessage Put(int id, [FromBody] TodoItemFixed value)
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
    }
}
