using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjTravelUnitTest.Api.Models;

namespace ProjTravelUnitTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly TravelContext _context;

        public ClientController(TravelContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            return await _context.Client.ToListAsync();
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await _context.Client.FindAsync(client.Id);
        }

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return await _context.Client.FindAsync(client.Id);

        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }

        private int getMaxId()
        {
            return _context.Client.OrderByDescending(u => u.Id).FirstOrDefault().Id;
        }
    }
}
