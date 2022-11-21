using DictionaryAPI.Context;
using DictionaryAPI.Context.DictionaryRepository;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using DictionaryAPI.Operations.Create;
using DictionaryAPI.Operations.Delete;
using DictionaryAPI.Operations.Get;
using DictionaryAPI.Operations.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DictionaryAPI.Controllers
{
    [Route("[controller]/Words")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly DictionaryDB context;

        public DictionaryController(DictionaryDB context)
        {
            this.context = context;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetWordById(long id)
        {
            var command = new GetWordByIdQuery(context);
            
            WordViewModel viewModel = await command.GetWordById(id);
            
            return Ok(viewModel);
        }

        [HttpGet("{wordName}")]

        public async Task<IActionResult> GetWordByName(string wordName)
        {
            var command = new GetWordByNameQuery(context);

            WordViewModel viewModel = await command.GetWordByName(wordName);

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] WordViewModel _word)
        {
            ICreateWordCommand command = new CreateWordCommand(context);
            Word word = command.CreateNewWord(_word);
            await command.SaveNewWord(word);
            
            return CreatedAtAction(
                actionName: nameof(GetWordById),
                routeValues: new { id = word.Id },
                _word
                );
        }

        [HttpPut("{wordName}")]
        public async Task<IActionResult> UpdateWord([FromBody] WordViewModel _word, string wordName)
        {
            IUpdateWordCommand command = new UpdateWordCommand(context);
            await command.Update(_word, wordName);
            return Ok("Succesfully Updated");
        }

        [HttpDelete("{wordName}")]
        public IActionResult DeleteWordByName(string wordName)
        {
            IDeleteWordByNameCommand command = new DeleteWordByNameCommand(context);
            bool IsDeleted = command.DeleteByName(wordName).Result;
            if (IsDeleted)
            {
                command.SaveChanges();
                return Ok("Succesfully Deleted");
            }
            return BadRequest();
            
        }

        [HttpDelete("{id:long}")]
        public IActionResult DeleteWordById(long id)
        {
            IDeleteWordByIdCommand command = new DeleteWordByIdCommand(context);
            bool IsDeleted = command.DeleteById(id).Result;
            if (IsDeleted)
            {
                command.SaveChanges();
                return Ok("Succesfully Deleted");
            }
            return BadRequest();
        }




    }
}
