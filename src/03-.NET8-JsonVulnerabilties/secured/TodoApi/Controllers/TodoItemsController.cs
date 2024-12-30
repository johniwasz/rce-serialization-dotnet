using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Configuration.Install;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        private readonly static SynchronizedCollection<TodoItem> _items = new SynchronizedCollection<TodoItem>();

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return _items;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
          /*
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var todoItem = await _context.TodoItems.Include(x=> x.Metadata).FirstOrDefaultAsync(x => x.Id == id);


            if (todoItem == null)
            {
                return NotFound();
            }
            */

            var todoItem = _items[id];

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id || id<0)
            {
                return BadRequest();
            }

            if(_items.Count > id)
            {
                _items[id] = todoItem;

            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        { 

            _items.Add(todoItem);

            todoItem.Id = _items.IndexOf(todoItem);

            return todoItem;
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {

            _items.RemoveAt(id);

            return NoContent();
        }
    }
}
