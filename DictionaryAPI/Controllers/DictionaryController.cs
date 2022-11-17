using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        [HttpGet("{id:long}")]
        public ActionResult GetWord(long id)
        {
            return Ok();
        }
    }
}
