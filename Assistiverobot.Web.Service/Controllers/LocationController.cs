using System;
using System.Collections.Generic;
using System.Linq;
using Assistiverobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Domains;
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

        [HttpPost]
        public IActionResult CreateLocation([FromBody] LocationRequest locationRequest)
        {
            if (locationRequest.Name == null || locationRequest.Position == null || locationRequest.Orientation == null)
            {
                return GetResultBadRequest();
            }

            try
            {
                var location = new Location()
                {
                    Name = locationRequest.Name,
                    PositionX = locationRequest.Position.X,
                    PositionY = locationRequest.Position.Y,
                    PositionZ = locationRequest.Position.Z,
                    OrientationX = locationRequest.Orientation.X,
                    OrientationY = locationRequest.Orientation.Y,
                    OrientationZ = locationRequest.Orientation.Z,
                    OrientationW = locationRequest.Orientation.W
                };
                _locationRepository.Add(location);
            return GetResultCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }

        [HttpPatch]
        public IActionResult UpdateLocation()
        {
            return GetResultSuccess();
        }
    }
}