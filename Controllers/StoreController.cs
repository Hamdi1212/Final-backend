using Microsoft.AspNetCore.Mvc;
using app.models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly Datacontext _context;

        public StoreController(Datacontext context)
        {
            _context = context;
        }

        // POST: /Store/storepost
        [HttpPost("storepost")]
        public async Task<IActionResult> CreateStore([FromBody] StoreDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.SerialNumber))
                return BadRequest("Le nom et le numéro de série sont obligatoires.");

            var store = new Store
            {
                StoreId = Guid.NewGuid(),
                Name = dto.Name,
                SerialNumber = dto.SerialNumber
            };
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Magasin créé avec succès.", store });
        }

        // GET: /Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        // GET: /Store/search-equipement
        [HttpGet("search-equipement")]
        public async Task<ActionResult<IEnumerable<Equipement>>> SearchEquipement(
            [FromQuery] string description = null,
            [FromQuery] string marque = null,
            [FromQuery] string serialNumber = null)
        {
            var query = _context.Equipements.AsQueryable();

            if (!string.IsNullOrWhiteSpace(description))
                query = query.Where(e => e.Description.Contains(description));

            if (!string.IsNullOrWhiteSpace(marque))
                query = query.Where(e => e.Marque.Contains(marque));

            if (!string.IsNullOrWhiteSpace(serialNumber))
                query = query.Where(e => e.SerialNumber.Contains(serialNumber));

            var result = await query.ToListAsync();
            return Ok(result);
        }
    }

    public class StoreDto
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
    }
}