using System;
using System.Linq;
using System.Reflection;
using Assistiverobot.Web.Service.Constants;
using AssistiveRobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/goals")]
    public class GoalController : BaseController
    {
        private readonly GoalService _goalService;

        public GoalController(GoalService goalService)
        {
            _goalService = goalService;
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
                _goalService.UpdateGoal(id, goalRequest);
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