using DictionaryAPI.Context;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using DictionaryAPI.Operations.Create;
using DictionaryAPI.Operations.Delete;
using DictionaryAPI.Operations.Get;
using DictionaryAPI.Operations.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryAPI.Controllers
{
    [Route("api/[controller]/Words")]
    [ApiController]
    [Authorize(Roles = "User")]
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
        [AllowAnonymous]

        public async Task<IActionResult> GetWordByName(string wordName)
        {
            var command = new GetWordByNameQuery(context);

            WordViewModel viewModel = await command.GetWordByName(wordName);

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] WordViewModel _word)
        {
            CreateWordValidation createWordValidation= new CreateWordValidation();
            await createWordValidation.Validate(_word);
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
            bool IsUpdated = await command.Update(_word, wordName);
            if (IsUpdated)
                return Ok(new { message = "Succesfully Updated" });
            else
                return BadRequest(new { message = "The word to be updated couldn't be found." });
        }

        [HttpDelete("{wordName}")]
        public IActionResult DeleteWordByName(string wordName)
        {
            IDeleteWordByNameCommand command = new DeleteWordByNameCommand(context);
            bool IsDeleted = command.DeleteByName(wordName).Result;
            if (IsDeleted)
            {
                command.SaveChanges();
                return Ok(new { message = "Succesfully Deleted" });
            }
            return BadRequest(new { message = "The word to be deleted couldn't be found." });

        }

        [HttpDelete("{id:long}")]
        public IActionResult DeleteWordById(long id)
        {
            IDeleteWordByIdCommand command = new DeleteWordByIdCommand(context);
            bool IsDeleted = command.DeleteById(id).Result;
            if (IsDeleted)
            {
                command.SaveChanges();
                return Ok(new {message = "Succesfully Deleted" });
            }
            return BadRequest(new { message = "The word to be deleted couldn't be found." });
        }
    }
}
