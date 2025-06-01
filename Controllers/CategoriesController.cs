using app.models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly Datacontext _datacontext;
        public CategoriesController(Datacontext datacontext)
        {
            _datacontext = datacontext;
        }

        [HttpPost("categories")]
        public IActionResult AddCategories([FromBody] Category categories)
        {
            if(categories == null) { 
                return BadRequest(); 
            }
            else
            {
                _datacontext.categorys.Add(categories);
                _datacontext.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {   
            var categories = _datacontext.categorys.ToList();
            return Ok(categories);
        }
    }
}
