using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/goals")]
    public class GoalController : BaseController
    {
        
        [HttpPatch]
        public IActionResult UpdateGoal(long id, [FromBody] object goal)
        {
            return GetResultSuccess();
        }
    }
}