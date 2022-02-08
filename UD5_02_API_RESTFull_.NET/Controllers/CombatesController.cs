using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UD5_02_API_RESTFull_.NET.Models;

namespace UD5_02_API_RESTFull_.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombatesController : ControllerBase
    {
        private readonly CombateContext _context;

        public CombatesController(CombateContext context)
        {
            _context = context;
        }

        // GET: api/Combates
        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CombateDTO>>> GetTodoItems()
        {
            return await _context.Combate
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CombateDTO>> GetTodoItem(long id)
        {
            var todoItem = await _context.Combate.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, CombateDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.Combate.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CombateDTO>> CreateTodoItem(CombateDTO todoItemDTO)
        {
            var todoItem = new Combate
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.Combate.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.Combate.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Combate.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id) =>
             _context.Combate.Any(e => e.Id == id);

        private static CombateDTO ItemToDTO(Combate todoItem) =>
            new CombateDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
