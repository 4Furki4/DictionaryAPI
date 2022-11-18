using DictionaryAPI.Context;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using DictionaryAPI.Operations.Create;
using DictionaryAPI.Operations.Get;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly DictionaryDB context;

        public DictionaryController(DictionaryDB context)
        {
            this.context = context;
        }

        [HttpGet("Word/{id:long}")]
        public async Task<IActionResult> GetWord(long id)
        {
            var command = new GetWordQuery(context);
            
            WordViewModel viewModel = await command.GetWordById(id);
            
            return Ok(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] WordViewModel _word)
        {
            ICreateWordCommand command = new CreateWordCommand(context);
            Word word = command.CreateNewWord(_word);
            await command.SaveNewWord(word);
            
            return CreatedAtAction(
                actionName: nameof(GetWord),
                routeValues: new { id = word.Id },
                _word
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWord()
        {
            return Ok();
        }

        
    }
}
