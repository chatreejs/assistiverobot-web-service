
using AssistiveRobot.Web.Service.Models.Request;

namespace AssistiveRobot.Web.Service.Services
{
    public interface IGoalService
    {
        public void UpdateGoal(long id, GoalRequest goalRequest);
    }
}