using System;
using System.Threading.Tasks;
using Common.DtoConverters;
using Microsoft.AspNetCore.Mvc;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Models.InputModels;
using VideotapeGalore.Services.Filters;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TapesController : ControllerBase
    {
        private ITapesService TapesService { get; }
        private IReviewsService ReviewsService { get; }
        private IBorrowInfosService BorrowInfosService { get; }

        public TapesController(
            ITapesService tapesService,
            IReviewsService reviewsService,
            IBorrowInfosService borrowInfosService
        )
        {
            TapesService = tapesService;
            ReviewsService = reviewsService;
            BorrowInfosService = borrowInfosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] DateTime? loanDate,
            [FromQuery] int? loanDuration
        )
        {
            var tapes = await BorrowInfosService.GetTapesMatchingBorrowFilter(new BorrowFilter
            {
                LoanDate = loanDate,
                LoanDuration = loanDuration,
            });
            return Ok(tapes.ToDtos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            var tape = await TapesService.GetSingle(id);
            return Ok(tape.ToDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] TapeInputModel inputModel)
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
            return CreatedAtAction(nameof(GetSingle), new {id = tape.Id}, tape.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            TapesService.Delete(await TapesService.GetSingle(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TapeInputModel inputModel)
        {
            var tape = await TapesService.GetSingle(id);
            tape.Title = inputModel.Title;
            tape.Type = inputModel.Type;
            tape.ReleaseDate = inputModel.ReleaseDate;
            tape.Eidr = inputModel.Eidr;
            tape.DirectorFirstName = inputModel.DirectorFirstName;
            tape.DirectorLastName = inputModel.DirectorLastName;

            TapesService.Update(tape);
            return NoContent();
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await ReviewsService.GetAll();
            return Ok(reviews.ToDtos());
        }

        [HttpGet("{tapeId}/reviews")]
        public async Task<IActionResult> GetAllReviewsForTape([FromRoute] int tapeId)
        {
            var reviews = await ReviewsService.GetAllForTape(tapeId);
            return Ok(reviews.ToDtos());
        }

        [HttpGet("{tapeId}/reviews/{friendId}")]
        public async Task<IActionResult> GetAllReviewsForTape([FromRoute] int tapeId, [FromRoute] int friendId)
        {
            var review = await ReviewsService.GetFriendReviewOfTape(friendId, tapeId);
            return Ok(review.ToDto());
        }

        [HttpPut("{tapeId}/reviews/{friendId}")]
        public async Task<IActionResult> UpdateReviewForTape(
            [FromRoute] int tapeId,
            [FromRoute] int friendId,
            [FromBody] ReviewInputModel inputModel
        )
        {
            var review = await ReviewsService.GetSingle(friendId, tapeId);
            review.Rating = inputModel.Rating;

            ReviewsService.Update(review);
            return NoContent();
        }

        [HttpDelete("{tapeId}/reviews/{friendId}")]
        public async Task<IActionResult> DeleteReviewForTape([FromRoute] int tapeId, [FromRoute] int friendId)
        {
            var review = await ReviewsService.GetSingle(friendId, tapeId);
            ReviewsService.Delete(review);
            return NoContent();
        }
    }
}