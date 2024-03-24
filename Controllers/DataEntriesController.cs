using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Models;


namespace SimpleCRUD.Controllers
{
    [Route("tasks/[controller]")]
    [ApiController]
    public class DataEntriesController : ControllerBase
    {
        private readonly SimpleDBContext _dbContext;

        public DataEntriesController(SimpleDBContext context)
        {
            _dbContext = context;
        }

        private bool TaskExist(Guid id)
        {
            return _dbContext.DataEntries.Any(e => e.Id == id);
        }

        //Now lets get some data list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataEntries>>> GetDataEntries()
        {
            return await _dbContext.DataEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataEntries>> GetDataEntry(Guid id)
        {
            var task = await _dbContext.DataEntries.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        [HttpPost]
        public async Task<ActionResult<DataEntries>> PostTask(DataEntries task)
        {
            _dbContext.DataEntries.Add(task);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("New Task", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(DataEntries task, Guid id)
        {
            task.Id = id;

            _dbContext.Entry(task).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExist(id)) {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataEntries>> DeleteTask(Guid id)
        {
            var task = await _dbContext.DataEntries.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _dbContext.DataEntries.Remove(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }
    }
}
