using System.Threading.Tasks;
using Common.DtoConverters;
using Microsoft.AspNetCore.Mvc;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Models.InputModels;
using VideotapeGalore.Services.Implementations;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IFriendsService FriendsService { get; }
        private IReviewsService ReviewsService { get; }
        private IRecommendationService RecommendationService { get; }

        public UsersController(
            IFriendsService friendsService,
            IReviewsService reviewsService,
            IRecommendationService recommendationService)
        {
            FriendsService = friendsService;
            ReviewsService = reviewsService;
            RecommendationService = recommendationService;
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FriendInputModel inputModel)
        {
            var friend = await FriendsService.GetSingle(id);
            friend.FirstName = inputModel.FirstName;
            friend.LastName = inputModel.LastName;
            friend.Email = inputModel.Email;
            friend.Phone = inputModel.Phone;
            friend.Address = inputModel.Address;

            FriendsService.Update(friend);
            return NoContent();
        }

        [HttpGet("{friendId}/reviews")]
        public async Task<IActionResult> GetAllReviews([FromRoute] int friendId)
        {
            var reviews = await ReviewsService.GetAllForFriend(friendId);
            return Ok(reviews.ToDtos());
        }

        [HttpGet("{friendId}/reviews/{tapeId}")]
        public async Task<IActionResult> GetReviewOfTape([FromRoute] int friendId, [FromRoute] int tapeId)
        {
            var review = await ReviewsService.GetSingle(friendId, tapeId);
            return Ok(review.ToDto());
        }

        [HttpPost("{friendId}/reviews/{tapeId}")]
        public IActionResult CreateReviewOfTape(
            [FromRoute] int friendId,
            [FromRoute] int tapeId,
            [FromBody] ReviewInputModel inputModel
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var review = new Review
            {
                FriendId = friendId,
                TapeId = tapeId,
                Rating = inputModel.Rating
            };
            ReviewsService.Create(review);
            return CreatedAtAction(
                nameof(GetReviewOfTape),
                new {friendId = review.FriendId, tapeId = review.TapeId},
                review.ToDto());
        }

        [HttpPut("{friendId}/reviews/{tapeId}")]
        public async Task<IActionResult> UpdateReviewForTape(
            [FromRoute] int friendId,
            [FromRoute] int tapeId,
            [FromBody] ReviewInputModel inputModel
        )
        {
            var review = await ReviewsService.GetSingle(friendId, tapeId);
            review.Rating = inputModel.Rating;

            ReviewsService.Update(review);
            return NoContent();
        }

        [HttpDelete("{friendId}/reviews/{tapeId}")]
        public async Task<IActionResult> DeleteReviewForTape([FromRoute] int friendId, [FromRoute] int tapeId)
        {
            var review = await ReviewsService.GetSingle(friendId, tapeId);
            ReviewsService.Delete(review);
            return NoContent();
        }

        [HttpGet("{friendId}/recommendations")]
        public async Task<IActionResult> GetUserRecommendations([FromRoute] int friendId)
        {
            var recommendations = await RecommendationService.GetRecommendations(friendId);
            return Ok(recommendations.ToDtos());
        }

        [HttpGet("{friendId}/tapes")]
        public async Task<IActionResult> GetTapesOnLoanForUser([FromRoute] int friendId)
        {
            // Make sure friend exists
            await FriendsService.GetSingle(friendId);
            var tapes = await FriendsService.TapesOnLoan(friendId);
            return Ok(tapes.ToDtos());
        }
        
        [HttpPatch("{friendId}/tapes/{tapeId}")]
        public async Task<IActionResult> RegisterTapeOnLoan([FromRoute] int friendId, [FromRoute] int tapeId)
        {
            await FriendsService.RegisterTape(friendId, tapeId);
            return NoContent();
        }
        
        [HttpDelete("{friendId}/tapes/{tapeId}")]
        public async Task<IActionResult> HandinTapeOnLoan([FromRoute] int friendId, [FromRoute] int tapeId)
        {
            await FriendsService.ReturnTape(friendId, tapeId);
            return NoContent();
        }       
    }
}