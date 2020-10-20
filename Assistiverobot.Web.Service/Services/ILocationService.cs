using System.Collections.Generic;
using Assistiverobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Models.Response;

namespace AssistiveRobot.Web.Service.Services
{
    public interface ILocationService
    {
        public List<LocationResponse> GetAllLocation();
        public LocationResponse GetLocationById(long id);
        public void CreateLocation(LocationRequest locationRequest);
        public void UpdateLocation(long id, LocationRequest locationRequest);
    }
}