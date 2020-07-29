using System;
using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Models.Response;
using AssistiveRobot.Web.Service.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/locations")]
    public class LocationController : BaseController
    {
        private readonly LocationRepository _locationRepository;

        public LocationController(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet]
        public IActionResult GetAllLocation()
        {
            try
            {
                var locations = _locationRepository.GetAll();
                if (!locations.Any())
                {
                    return GetResultSuccess(null, StatusCodes.Status204NoContent);
                }

                var locationResponse = new List<LocationResponse>();
                foreach (var location in locations)
                {
                    locationResponse.Add(new LocationResponse()
                    {
                        LocationId = location.LocationId,
                        Name = location.Name
                    });
                }
                return GetResultSuccess(locationResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }
    }
}