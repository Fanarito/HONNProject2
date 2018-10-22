using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Models.InputModels;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private IFriendsService FriendsService { get; }

        public FriendsController(IFriendsService friendsService)
        {
            FriendsService = friendsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await FriendsService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            return Ok(await FriendsService.GetSingle(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] FriendInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var friend = new Friend
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Email = inputModel.Email,
                Phone = inputModel.Phone,
                Address = inputModel.Address,
            };
            FriendsService.Create(friend);
            return CreatedAtAction(nameof(GetSingle), new {id = friend.Id}, friend);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            FriendsService.Delete(await FriendsService.GetSingle(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Friend friend)
        {
            FriendsService.Update(friend);
            return NoContent();
        }
    }
}