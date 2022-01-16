using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab6API.Models;
using lab6API.Attributes;

namespace lab6API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class WheaterItemsController : ControllerBase
    {
        private readonly WeatherContext _context;

        public WheaterItemsController(WeatherContext context)
        {
            _context = context;
        }

        // GET: api/WheaterItems
        [HttpGet]
        [Produces("application/json")]
        [SwaggerResponse(200, "Success", Type = typeof(List<WheaterItem>))]
        public async Task<ActionResult<IEnumerable<WheaterItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/WheaterItems/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerResponse(200, "The item with the specified {id} was found.", Type = typeof(WheaterItem))]
        [SwaggerResponse(404, "The item with the specified {id} was not found.")]
        public async Task<ActionResult<WheaterItem>> GetWheaterItem(
            [SwaggerParameter("Pick which ID you want to read", Required = true)]  int id
            )
        {
            var wheaterItem = await _context.TodoItems.FindAsync(id);

            if (wheaterItem == null)
            {
                return NotFound();
            }

            return wheaterItem;
        }

        // PUT: api/WheaterItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces("application/json")]
        [SwaggerResponse(204, "Updated item with the given {id}")]
        [SwaggerResponse(400, "Input not recognized")]
        [SwaggerResponse(404, "The item with the specified {id} was not found.")]
        public async Task<IActionResult> PutWheaterItem(
            [SwaggerParameter("Enter the item number you want to update", Required = true)] int id,
            [SwaggerParameter("Item Data", Required = true)] WheaterItem wheaterItem)
        {
            if (id != wheaterItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(wheaterItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WheaterItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WheaterItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [SwaggerResponse(201, "Successfully save.", Type = typeof(WheaterItem))]
        public async Task<ActionResult<WheaterItem>> PostWheaterItem(
            [SwaggerParameter("Item Data", Required = true)] WheaterItem wheaterItem)
        {
            _context.TodoItems.Add(wheaterItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWheaterItem", new { id = wheaterItem.Id }, wheaterItem);
        }

        // DELETE: api/WheaterItems/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [SwaggerResponse(204, "An item with the specified {id} has been deleted.")]
        [SwaggerResponse(404, "The item with the provided {id} was not found.")]
        public async Task<IActionResult> DeleteWheaterItem(
            [SwaggerParameter("Enter the ID of the item you wish to Eleminate..", Required = true)] int id)
        {
            var wheaterItem = await _context.TodoItems.FindAsync(id);
            if (wheaterItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(wheaterItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WheaterItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
