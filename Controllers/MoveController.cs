using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoveController : ControllerBase
    {
        private readonly Datacontext _context;

        public MoveController(Datacontext context)
        {
            _context = context;
        }

        // GET: /Move/projets
        [HttpGet("projets")]
        public async Task<ActionResult<IEnumerable<Projet>>> GetProjets()
        {
            return await _context.Projets
                .Include(p => p.Lignes)
                .ThenInclude(l => l.Postes)
                .ToListAsync();
        }

        // GET: /Move/projets/{id}
        [HttpGet("projets/{id}")]
        public async Task<ActionResult<Projet>> GetProjet(int id)
        {
            var projet = await _context.Projets
                .Include(p => p.Lignes)
                .ThenInclude(l => l.Postes)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (projet == null) return NotFound();
            return projet;
        }

        // POST: /Move/projets
        // Crée un projet, une ligne et un poste en une seule opération
        [HttpPost("projets")]
        public async Task<ActionResult> CreateProjet([FromBody] ProjetLignePosteDto dto)
        {
            // Vérifier l'unicité du nom du projet
            var exist = await _context.Projets.AnyAsync(p => p.Nom == dto.ProjetNom);
            if (exist)
                return BadRequest("Un projet avec ce nom existe déjà.");

            var projet = new Projet { Nom = dto.ProjetNom };
            _context.Projets.Add(projet);
            await _context.SaveChangesAsync();

            var ligne = new Ligne { Nom = dto.LigneNom, Project_Id = projet.Id };
            _context.Lignes.Add(ligne);
            await _context.SaveChangesAsync();

            var poste = new Poste { Name = dto.PosteNom, LigneId = ligne.Id };
            _context.Postes.Add(poste);
            await _context.SaveChangesAsync();

            return Ok(new { projet, ligne, poste });
        }

        // DELETE: /Move/projets/{id}
        [HttpDelete("projets/{id}")]
        public async Task<IActionResult> DeleteProjet(int id)
        {
            var projet = await _context.Projets.FindAsync(id);
            if (projet == null) return NotFound();
            _context.Projets.Remove(projet);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /Move/projets/byname/{name}
        [HttpDelete("projets/byname/{name}")]
        public async Task<IActionResult> DeleteProjetByName(string name)
        {
            var projet = await _context.Projets.FirstOrDefaultAsync(p => p.Nom == name);
            if (projet == null) return NotFound();
            _context.Projets.Remove(projet);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: /Move/lignes
        [HttpGet("lignes")]
        public async Task<ActionResult<IEnumerable<Ligne>>> GetLignes()
        {
            return await _context.Lignes.Include(l => l.Postes).ToListAsync();
        }

        // GET: /Move/lignes/{id}
        [HttpGet("lignes/{id}")]
        public async Task<ActionResult<Ligne>> GetLigne(int id)
        {
            var ligne = await _context.Lignes.Include(l => l.Postes).FirstOrDefaultAsync(l => l.Id == id);
            if (ligne == null) return NotFound();
            return ligne;
        }

        // POST: /Move/lignes
        [HttpPost("lignes")]
        public async Task<ActionResult<Ligne>> CreateLigne([FromBody] LigneDto dto)
        {
            var ligne = new Ligne { Nom = dto.LigneNom, Project_Id = dto.ProjectId };
            _context.Lignes.Add(ligne);
            await _context.SaveChangesAsync();
            return Ok(ligne);
        }

        // PUT: /Move/lignes/{id}
        [HttpPut("lignes/{id}")]
        public async Task<IActionResult> UpdateLigne(int id, [FromBody] LigneDto dto)
        {
            var ligne = await _context.Lignes.FindAsync(id);
            if (ligne == null) return NotFound();
            ligne.Nom = dto.LigneNom;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /Move/lignes/{id}
        [HttpDelete("lignes/{id}")]
        public async Task<IActionResult> DeleteLigne(int id)
        {
            var ligne = await _context.Lignes.FindAsync(id);
            if (ligne == null) return NotFound();
            _context.Lignes.Remove(ligne);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /Move/lignes/byname/{name}
        [HttpDelete("lignes/byname/{name}")]
        public async Task<IActionResult> DeleteLigneByName(string name)
        {
            var ligne = await _context.Lignes.FirstOrDefaultAsync(l => l.Nom == name);
            if (ligne == null) return NotFound();
            _context.Lignes.Remove(ligne);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: /Move/postes
        [HttpGet("postes")]
        public async Task<ActionResult<IEnumerable<Poste>>> GetPostes()
        {
            return await _context.Postes.Include(p => p.Ligne).ToListAsync();
        }

        // GET: /Move/postes/{id}
        [HttpGet("postes/{id}")]
        public async Task<ActionResult<Poste>> GetPoste(int id)
        {
            var poste = await _context.Postes.Include(p => p.Ligne).FirstOrDefaultAsync(p => p.Id == id);
            if (poste == null) return NotFound();
            return poste;
        }

        // POST: /Move/postes
        [HttpPost("postes")]
        public async Task<ActionResult<Poste>> CreatePoste([FromBody] PosteDto dto)
        {
            var poste = new Poste { Name = dto.PosteNom, LigneId = dto.LigneId };
            _context.Postes.Add(poste);
            await _context.SaveChangesAsync();
            return Ok(poste);
        }

        // PUT: /Move/postes/{id}
        [HttpPut("postes/{id}")]
        public async Task<IActionResult> UpdatePoste(int id, [FromBody] PosteDto dto)
        {
            var poste = await _context.Postes.FindAsync(id);
            if (poste == null) return NotFound();
            poste.Name = dto.PosteNom;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /Move/postes/{id}
        [HttpDelete("postes/{id}")]
        public async Task<IActionResult> DeletePoste(int id)
        {
            var poste = await _context.Postes.FindAsync(id);
            if (poste == null) return NotFound();
            _context.Postes.Remove(poste);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /Move/postes/byname/{name}
        [HttpDelete("postes/byname/{name}")]
        public async Task<IActionResult> DeletePosteByName(string name)
        {
            var poste = await _context.Postes.FirstOrDefaultAsync(p => p.Name == name);
            if (poste == null) return NotFound();
            _context.Postes.Remove(poste);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class ProjetLignePosteDto
    {
        public string ProjetNom { get; set; }
        public string LigneNom { get; set; }
        public string PosteNom { get; set; }
    }

    public class LigneDto
    {
        public string LigneNom { get; set; }
        public int ProjectId { get; set; }
    }

    public class PosteDto
    {
        public string PosteNom { get; set; }
        public int LigneId { get; set; }
    }
}