using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefitSamples.Data;
using RefitSamples.Models;

namespace RefitSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloModelsController : ControllerBase
    {
        private readonly RefitSamplesDbContext _context;

        public HelloModelsController(RefitSamplesDbContext context)
        {
            _context = context;
        }

        // GET: api/HelloModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HelloModel>>> GetHelloModels()
        {
            return await _context.HelloModels.ToListAsync();
        }

        // GET: api/HelloModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HelloModel>> GetHelloModel(Guid id)
        {
            var helloModel = await _context.HelloModels.FindAsync(id);

            if (helloModel == null)
            {
                return NotFound();
            }

            return helloModel;
        }

        // PUT: api/HelloModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelloModel(Guid id, HelloModel helloModel)
        {
            if (id != helloModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(helloModel).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelloModelExists(id))
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

        // POST: api/HelloModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HelloModel>> PostHelloModel(HelloModel helloModel)
        {
            _context.HelloModels.Add(helloModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetHelloModel", new { id = helloModel.Id }, helloModel);
        }

        // DELETE: api/HelloModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHelloModel(Guid id)
        {
            var helloModel = await _context.HelloModels.FindAsync(id);
            if (helloModel == null)
            {
                return NotFound();
            }

            _context.HelloModels.Remove(helloModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HelloModelExists(Guid id)
        {
            return _context.HelloModels.Any(e => e.Id == id);
        }
    }
}
