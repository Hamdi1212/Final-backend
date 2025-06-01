using app.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly Datacontext _datacontext;
         public  TransactionController (Datacontext datacontext)
        {
            _datacontext = datacontext;
        }
        // GET: Transaction

        [HttpGet("Get_transactions")]
        public ActionResult Get_transactions()
        {
            var transactions = _datacontext.transactions
                .OrderByDescending(x => x.TransactionDate)
                .Select(x => new
                {
                    SerialNumber = x.SerialNumber,
                    TransactionDate = x.TransactionDate.ToString("dd/MM/yyyy"),
                    Status = x.Status,
                    // Ajout de la description de l'équipement lié à la transaction
                    EquipementDescription = _datacontext.Equipements
                        .Where(e => e.SerialNumber == x.SerialNumber)
                        .Select(e => e.Description)
                        .FirstOrDefault(),
                    // Description de la transaction (déjà présente dans Transaction)
                    TransactionDescription = x.Description
                })
                .ToList();

            return Ok(transactions);
        }

        [HttpGet("ProductionZone")]
        public ActionResult GetProductionZone()
        {
            var productionZone = _datacontext.ProductionZonees.ToList();
            return Ok(productionZone);
        }

        [HttpPost("MoveEquipement")]
        public async Task<IActionResult> MoveEquipement([FromBody] MoveEquipementDto dto)
        {
            var equipement = await _datacontext.Equipements.FirstOrDefaultAsync(e => e.SerialNumber == dto.SerialNumber);
            if (equipement == null) return NotFound("Equipement non trouvé");

            // Mise à jour de l’emplacement
            equipement.StoreId = dto.StoreId;
            equipement.ProjetId = dto.ProjetId;
            equipement.LigneId = dto.LigneId;
            equipement.PosteId = dto.PosteId;

            // Construction de l’emplacement courant
            if (dto.PosteId != null)
            {
                var poste = await _datacontext.Postes.FindAsync(dto.PosteId);
                var ligne = await _datacontext.Lignes.FindAsync(poste.LigneId);
                var projet = await _datacontext.Projets.FindAsync(ligne.Project_Id);
                equipement.CurrentEmplacement = $"Projet: {projet.Nom}, Ligne: {ligne.Nom}, Poste: {poste.Name}";
            }
            else if (dto.LigneId != null)
            {
                var ligne = await _datacontext.Lignes.FindAsync(dto.LigneId);
                var projet = await _datacontext.Projets.FindAsync(ligne.Project_Id);
                equipement.CurrentEmplacement = $"Projet: {projet.Nom}, Ligne: {ligne.Nom}";
            }
            else if (dto.ProjetId != null)
            {
                var projet = await _datacontext.Projets.FindAsync(dto.ProjetId);
                equipement.CurrentEmplacement = $"Projet: {projet.Nom}";
            }
            else if (dto.StoreId != null)
            {
                var store = await _datacontext.Stores.FindAsync(dto.StoreId);
                equipement.CurrentEmplacement = $"Store: {store.Name}";
            }
            else
            {
                equipement.CurrentEmplacement = "Non défini";
            }

            // Ajout d’une transaction
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                TransactionDate = DateTime.Now,
                SerialNumber = equipement.SerialNumber,
                Status = "Déplacé",
                Description = equipement.Description, // description de l'équipement
                Deplacement = equipement.CurrentEmplacement // nouvel emplacement
            };
            await _datacontext.transactions.AddAsync(transaction);
            await _datacontext.SaveChangesAsync();

            return Ok("Equipement déplacé avec succès");
        }
    }

    public class MoveEquipementDto
    {
        public string SerialNumber { get; set; }
        public Guid? StoreId { get; set; }
        public int? ProjetId { get; set; }
        public int? LigneId { get; set; }
        public int? PosteId { get; set; }
    }
}
