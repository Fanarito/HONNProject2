using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Models.InputModels;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TapesController : ControllerBase
    {
        private ITapesService TapesService { get; }

        public TapesController(ITapesService tapesService)
        {
            TapesService = tapesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await TapesService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            return Ok(await TapesService.GetSingle(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TapeInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tape = new Tape
            {
                Title = inputModel.Title,
                Type = inputModel.Type,
                ReleaseDate = inputModel.ReleaseDate,
                Eidr = inputModel.Eidr,
                DirectorFirstName = inputModel.DirectorFirstName,
                DirectorLastName = inputModel.DirectorLastName,
            };
            TapesService.Create(tape);
            return CreatedAtAction(nameof(GetSingle), new {id = tape.Id}, tape);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            TapesService.Delete(await TapesService.GetSingle(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Tape tape)
        {
            TapesService.Update(tape);
            return NoContent();
        }
    }
}