using app.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly Datacontext _datacontext;
        private readonly Random _random = new Random();
        public HomeController(Datacontext datacontext)
        {
            _datacontext = datacontext;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Usermodel UserObj)
        {
            if (UserObj == null)
              return BadRequest();
            var Role = "";
            var matricule = "";
            var user = await _datacontext.Users.FirstOrDefaultAsync(x=>x.Name == UserObj.Name && x.Password == UserObj.Password);
            if(user != null)
            {
                Role = _datacontext.Users.Where(x => x.Name == UserObj.Name && x.Password == UserObj.Password).FirstOrDefault().Role;
                matricule = _datacontext.Users.Where(x => x.Name == UserObj.Name && x.Password == UserObj.Password).FirstOrDefault().matricule;
            }    
            if(user==null)
                return NotFound(new {message="User not found"});
            UserObj.Role = Role;
            UserObj.matricule = matricule;
            user.Token = Createjwt(UserObj);
            return Ok(new {
                Token = user.Token,
                message="login succed"});
        }
        [HttpPost("register")]
        public async Task<IActionResult> registeruser([FromBody]Usermodel UserObj)
        {
            if(UserObj==null) 
                return BadRequest();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            UserObj.matricule = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[_random.Next(s.Length)]).ToArray()); 
            await _datacontext.Users.AddAsync(UserObj);
            await _datacontext.SaveChangesAsync();
            return Ok
                (new 
                {
                    message="User registered!"
                });
        }

        [HttpPost("init-stores")]
        public async Task<IActionResult> InitStores()
        {
            var stores = new List<Store>
            {
                new Store { StoreId = Guid.NewGuid(), Name = "SR", SerialNumber = "01" },
                new Store { StoreId = Guid.NewGuid(), Name = "SR", SerialNumber = "02" },
                new Store { StoreId = Guid.NewGuid(), Name = "SR", SerialNumber = "03" },
                new Store { StoreId = Guid.NewGuid(), Name = "SR", SerialNumber = "04" },
                new Store { StoreId = Guid.NewGuid(), Name = "SR", SerialNumber = "05" }
            };

            // Vérifie si les magasins existent déjà pour éviter les doublons
            foreach (var store in stores)
            {
                if (!_datacontext.Stores.Any(s => s.Name == store.Name && s.SerialNumber == store.SerialNumber))
                {
                    await _datacontext.Stores.AddAsync(store);
                }
            }
            await _datacontext.SaveChangesAsync();
            return Ok(new { message = "Stores initialisés avec succès." });
        }

        private string Createjwt(Usermodel user)

        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("HamdiTestTest12345");

            var identity = new ClaimsIdentity(new Claim[]

            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.NameIdentifier,user.matricule),
            });

            var cred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
