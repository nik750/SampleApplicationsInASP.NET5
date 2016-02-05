using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheOneWithSwagger.Models;
using Newtonsoft.Json;
using Swashbuckle.SwaggerGen.Annotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheOneWithSwagger.Controllers
{
    [Route("api/todo")]
    public class TodoController : Controller
    {
        [FromServices]
        public ITodoRepository TodoItems { get; set; }

        /// <summary>
        /// This method returns all the todo items.
        /// </summary>
        /// <returns>All items found</returns>
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        /// <summary>
        /// This method returns todo item based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(item);
        }

        /// <summary>
        /// This method creates a todo item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return HttpBadRequest();
            }
            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { controller = "Todo", id = item.Key }, item);
        }

        /// <summary>
        /// This method updates a todo item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return HttpBadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }

            TodoItems.Update(item);
            return new NoContentResult();
        }

        /// <summary>
        /// This method deletes a todo item
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TodoItems.Remove(id);
        }
    }
}
