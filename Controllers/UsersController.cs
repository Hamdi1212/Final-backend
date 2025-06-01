using app.models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller

    {
        private readonly Datacontext _datacontext;
        private readonly Random _random = new Random();
        public UsersController(Datacontext datacontext)
        {
            _datacontext = datacontext;
        }

        [HttpGet("Users")]
        public IActionResult GetUsers()
            {
            var users = _datacontext.Users.ToList();
            return Ok(users);
        }

        [HttpGet("Stat")]
        public IActionResult GetStat()
        {
            var stat = _datacontext.Equipements.GroupBy(x => x.Marque).Select(x => new 
            {
                name = x.Key,
                value = x.Count()
            });
            return Ok(stat);
        }
        [HttpGet("Stat1")]
        public IActionResult GetStat1()
        {
            var stat = _datacontext.Equipements.GroupBy(x => x.category).Select(x => new
            {
                name = x.Key,
                value = x.Count()
            });
            return Ok(stat);
        }
        [HttpGet("Stat2")]
        public IActionResult GetStat2()
        {
            var stat = _datacontext.Users.Where(x =>(x.Role =="RH" || x.Role =="Teck")).GroupBy(y=>y.Role).Select(x => new
            {
                name = x.Key,
                value = x.Count()
            });
            return Ok(stat);
        }
        [HttpGet("Admin")]
        public IActionResult GetAdminUsers()
        {
            var users = _datacontext.Users.Where(x => (x.Role == "RH" || x.Role == "Teck")).ToList();
            return Ok(users);
        }
        [HttpDelete("Admin/{matricule}")]
        public IActionResult DeleteUsers(string matricule)
        {
            var users = _datacontext.Users.Where(x => x.matricule == matricule).FirstOrDefault();
            if(users != null)
            {
                _datacontext.Users.Remove(users);
                _datacontext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost("Employee")]
        public IActionResult CreateUser([FromBody] Worker UserObj)
        {
            if (UserObj == null)
                return BadRequest();

            UserObj.WorkId = new Guid();
                _datacontext.workers.Add(UserObj);
                _datacontext.SaveChanges();
            return Ok();
        }
    }
}
