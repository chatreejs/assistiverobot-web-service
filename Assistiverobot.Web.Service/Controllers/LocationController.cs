using System;
using AssistiveRobot.Web.Service.Constants;
using Assistiverobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/locations")]
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public IActionResult GetAllLocation()
        {
            try
            {
                var locationResponse = _locationService.GetAllLocation();
                if (locationResponse == null)
                {
                    return GetResultSuccess(null, StatusCodes.Status204NoContent);
                }
                return GetResultSuccess(locationResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLocationById(long id)
        {
            try
            {
                var locationResponse = _locationService.GetLocationById(id);
                if (locationResponse == null)
                {
                    return GetResultSuccess(null, StatusCodes.Status204NoContent);
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
        // [Authorize(Roles = Role.Admin)]
        public IActionResult CreateLocation([FromBody] LocationRequest locationRequest)
        {
            if (locationRequest.Name == null || locationRequest.Position == null || locationRequest.Orientation == null)
            {
                return GetResultBadRequest();
            }

            try
            {
                _locationService.CreateLocation(locationRequest);
                return GetResultCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }

        [HttpPatch("{id}")]
        // [Authorize(Roles = Role.Admin)]
        public IActionResult UpdateLocation(long id, [FromBody] LocationRequest locationRequest)
        {
            try
            {
                var location = _locationService.GetLocationById(id);
                if (location == null)
                {
                    return GetResultNotFound();
                }
                _locationService.UpdateLocation(id, locationRequest);
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