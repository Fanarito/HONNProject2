using System.Threading.Tasks;
using Common.DtoConverters;
using Microsoft.AspNetCore.Mvc;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Models.InputModels;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IFriendsService FriendsService { get; }

        public UsersController(IFriendsService friendsService)
        {
            FriendsService = friendsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var friends = await FriendsService.GetAll();
            return Ok(friends.ToDtos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            var friend = await FriendsService.GetSingle(id);
            return Ok(friend.ToDto());
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
            return CreatedAtAction(nameof(GetSingle), new {id = friend.Id}, friend.ToDto());
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