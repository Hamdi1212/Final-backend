using app.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionController : ControllerBase
    {
        private readonly Datacontext _context;

        public ActionController(Datacontext datacontext)
        {
            _context = datacontext;
        }
        [HttpGet("equipement/category/{categoryCode}")]
        public IActionResult GetEquipementByCategory(string categoryCode)
        {
            var equipements = _context.Equipements
                .Where(x => x.category == categoryCode && x.IsDeleted == false)
                .ToList();

            if (!equipements.Any())
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "No equipment found for the given category"
                });
            }

            return Ok(equipements);
        }
        [HttpPost("data")]
        public IActionResult Data([FromBody]Equipement equipementobj) 
        {
            if(equipementobj == null)
            {
                return BadRequest();
            }
            else
            {
                // Récupérer le store à partir de l'ID fourni
                var store = _context.Stores.FirstOrDefault(s => s.StoreId == equipementobj.StoreId);
                if (store != null)
                {
                    equipementobj.CurrentEmplacement = store.Name;
                }
                else
                {
                    equipementobj.CurrentEmplacement = null;
                }

                _context.Equipements.Add(equipementobj);
                var transaction = new Transaction()
                {
                    TransactionDate = DateTime.Now,
                    SerialNumber = equipementobj.SerialNumber,
                    Status = Constants.NewStatus,
                    TransactionId = Guid.NewGuid()
                };
                _context.transactions.Add(transaction);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode= 200,
                    Message="details added successfuly"
                });
            }
        }
        [HttpPut("update_equipement")]
        public IActionResult UpdateEquipement([FromBody]Equipement equipementobj) 
        {
            if(equipementobj == null)
            {
                return BadRequest();
            }
            var equipement = _context.Equipements.AsNoTracking().FirstOrDefault(x=>x.SerialNumber == equipementobj.SerialNumber);
            if(equipement==null) 
            {
                return NotFound(new
                {
                    StatusCode=404,
                    Message="equipement not found"
                });
            }
            else 
            {
                _context.Entry(equipementobj).State = EntityState.Modified;
                var transaction = new Transaction()
                {
                    TransactionDate = DateTime.Now,
                    SerialNumber = equipementobj.SerialNumber,
                    Status=Constants.ModifyStatus,
                    TransactionId = new Guid()

                };
                _context.transactions.Add(transaction);
                _context.SaveChanges();
                return Ok(new 
                {
                    StatusCode= 200,
                    Message = "Equipement has been update successfuly"
                });
            }
        }
        [HttpDelete("delet_equipement/{SerialNumber}")]
        public IActionResult DeleteEquipement(string SerialNumber) 
        {
            var equipement = _context.Equipements.FirstOrDefault(x=>x.SerialNumber==SerialNumber);
            if (equipement == null) 
            {
                return NotFound(new
                {
                    StatusCode= 200,
                    Message = "equipement not found"
                });
            }
            else
            {
                equipement.IsDeleted = true;
                _context.Equipements.Update(equipement);
                var transaction = new Transaction()
                {
                    TransactionDate = DateTime.Now,
                    SerialNumber = equipement.SerialNumber,
                    Status = Constants.DeletedStautus,
                    TransactionId = new Guid()

                };
                _context.transactions.Add(transaction);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode= 200,
                    Message = "equipement deleted"
                });
            }
        }
        [Authorize]
        [HttpGet("get_all_equipement")]
        public IActionResult GetAllEquipement()
        {
            var equipementdetails = _context.Equipements.Where(x => x.IsDeleted==false).ToList();
            return Ok(new
            {
                StatusCode= 200,
                EquipementDetails=equipementdetails
            });
        }
        [HttpGet("getequipement/SerialNumber")]
        public IActionResult GetEmploye(string SerialNumber)
        { //FirstOrDefault(e => e.SerialNumber == SerialNumber)
            var equipementdetail = _context.Equipements.Where(x => x.IsDeleted == false).FirstOrDefault(e=>e.SerialNumber==SerialNumber);
            if(equipementdetail == null) 
            {
                return NotFound(new
                {
                    StatusCode= 404,
                    Message="User Not found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    equipementdetail = equipementdetail
                });
            }
        }

    }
}

