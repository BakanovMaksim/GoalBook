using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GoalBook.SharedKernel;
using GoalBook.Core.Domain.Entities;

namespace GoalBook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class GoalController : ControllerBase
    {
        private readonly IRepository<Goal> _repository;

        public GoalController(IRepository<Goal> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetGoals()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("id")]
        [Route("get")]
        public async Task<IActionResult> GetGoal(string id)
        {
            if (Guid.TryParse(id, out var goalId))
            {
                var goal = await _repository.GetByIdAsync(goalId);

                return goal != null
                    ? Ok(goal)
                    : NoContent();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGoal([FromBody] Goal goal)
        {
            await _repository.CreateAsync(goal);

            return CreatedAtRoute("get", new { id = goal.Id.ToString() }, goal);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateGoal([FromBody] Goal goal)
        {
            var result = await _repository.UpdateAsync(goal);

            return result
                ? NoContent()
                : BadRequest();
        }

        [HttpDelete("id")]
        [Route("delete")]
        public async Task<IActionResult> DeleteGoal(string id)
        {
            if (Guid.TryParse(id, out var goalId))
            {
                var result = await _repository.DeleteAsync(goalId);

                return result
                    ? NoContent()
                    : BadRequest();
            }

            return BadRequest();
        }
    }
}
