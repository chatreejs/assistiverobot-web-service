using System;
using System.Collections.Generic;
using System.Linq;
using Assistiverobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Response;
using AssistiveRobot.Web.Service.Repositories;

namespace AssistiveRobot.Web.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public List<LocationResponse> GetAllLocation()
        {
            try
            {
                var locations = _locationRepository.GetAll();
                if (!locations.Any())
                {
                    return null;
                }

                var locationResponse = new List<LocationResponse>();
                foreach (var location in locations)
                {
                    var position = new Position()
                    {
                        X = location.PositionX,
                        Y = location.PositionY,
                        Z = location.PositionZ
                    };
                    var orientation = new Orientation()
                    {
                        X = location.OrientationX,
                        Y = location.OrientationY,
                        Z = location.OrientationZ,
                        W = location.OrientationW,
                    };
                    locationResponse.Add(new LocationResponse()
                    {
                        LocationId = location.LocationId,
                        Name = location.Name,
                        Position = position,
                        Orientation = orientation
                    });
                }
                return locationResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public LocationResponse GetLocationById(long id)
        {
            try
            {
                var location = _locationRepository.Get(id);
                if (location == null)
                {
                    return null;
                }
                var position = new Position()
                {
                    X = location.PositionX,
                    Y = location.PositionY,
                    Z = location.PositionZ
                };
                var orientation = new Orientation()
                {
                    X = location.OrientationX,
                    Y = location.OrientationY,
                    Z = location.OrientationZ,
                    W = location.OrientationW,
                };
                var locationResponse = new LocationResponse()
                {
                    LocationId = location.LocationId,
                    Name = location.Name,
                    Position = position,
                    Orientation = orientation
                };

                return locationResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public void CreateLocation(LocationRequest locationRequest)
        {
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
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public void UpdateLocation(long id, LocationRequest locationRequest)
        {
            try
            {
                var location = _locationRepository.Get(id);
                Position position;
                Orientation orientation;

                if (locationRequest.Position == null)
                {
                    position = new Position()
                    {
                        X = location.PositionX,
                        Y = location.PositionY,
                        Z = location.PositionZ
                    };
                }
                else
                {
                    position = new Position()
                    {
                        X = locationRequest.Position.X,
                        Y = locationRequest.Position.Y,
                        Z = locationRequest.Position.Z,
                    };
                }

                if (locationRequest.Orientation == null)
                {
                    orientation = new Orientation()
                    {
                        X = location.OrientationX,
                        Y = location.OrientationY,
                        Z = location.OrientationZ,
                        W = location.OrientationW
                    };
                }
                else
                {
                    orientation = new Orientation()
                    {
                        X = locationRequest.Orientation.X,
                        Y = locationRequest.Orientation.Y,
                        Z = locationRequest.Orientation.Z,
                        W = locationRequest.Orientation.W,
                    };
                }

                var locationUpdate = new Location()
                {
                    LocationId = location.LocationId,
                    Name = locationRequest.Name == null ? location.Name : locationRequest.Name,
                    PositionX = position.X,
                    PositionY = position.Y,
                    PositionZ = position.Z,
                    OrientationX = orientation.X,
                    OrientationY = orientation.Y,
                    OrientationZ = orientation.Z,
                    OrientationW = orientation.W
                };
                _locationRepository.Update(location, locationUpdate);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}