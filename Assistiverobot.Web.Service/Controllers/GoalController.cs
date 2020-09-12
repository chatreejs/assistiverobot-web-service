using System;
using System.Linq;
using System.Reflection;
using Assistiverobot.Web.Service.Constants;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/goals")]
    public class GoalController : BaseController
    {
        private readonly GoalRepository _goalRepository;

        public GoalController(GoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateGoal(long id, [FromBody] GoalRequest goalRequest)
        {
            if (goalRequest.Status == null)
            {
                return GetResultBadRequest();
            }

            var hasField = typeof(GoalStatus).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetRawConstantValue().ToString()).Contains(goalRequest.Status);

            if (!hasField)
            {
                return GetResultBadRequest();
            }

            try
            {
                var goal = _goalRepository.Get(id);
                var goalUpdated = new Goal()
                {
                    GoalId = goal.GoalId,
                    JobId = goal.JobId,
                    LocationId = goal.LocationId,
                    Status = goalRequest.Status,
                };
                _goalRepository.Update(goal, goalUpdated);
                return GetResultSuccess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }

        }
    }
}