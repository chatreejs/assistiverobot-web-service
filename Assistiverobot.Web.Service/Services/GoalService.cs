using System;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Repositories;

namespace AssistiveRobot.Web.Service.Services
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;

        public GoalService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public void UpdateGoal(long id, GoalRequest goalRequest)
        {
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
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}