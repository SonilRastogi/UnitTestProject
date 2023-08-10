using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ShoppingCartProject.Inteface;
using ShoppingCartProject.Models;

namespace ShoppingCartProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;
        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ShoppingItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _service.Add(value);
            return CreatedAtAction("Post", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            bool result = false;
            if(id == 0)
            {
                throw new ArgumentException("Id can't be null");
            }
            var existingItem = _service.GetById(id);
            if (existingItem == null)
            {
                return NotFound(result);
            }
           result=  _service.Remove(id);
            return NoContent();
        }
    }
}
