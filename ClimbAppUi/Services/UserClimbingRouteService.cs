
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlazorCLIMB.Model;
    using BlazorCLIMB.Data.Dapper.Repositories; // Dapper mapeo objeto-relacional (ORM)
using BlazorCLIMB.UI.Interfaces;

namespace BlazorCLIMB.UI.Services
{
    public class UserClimbingRouteService : IUserClimbingRouteService
    {
        private readonly IUserClimbingRouteRepository _repository;

        public UserClimbingRouteService(IUserClimbingRouteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserClimbingRoutes>> GetUserClimbingRoutes(int userId)
        {
            return await _repository.GetUserClimbingRoutes(userId);
        }

        public async Task<UserClimbingRoutes> GetUserClimbingRoute(int userId, int climbingRouteId)
        {
            return await _repository.GetUserClimbingRoute(userId, climbingRouteId);
        }

        public async Task<bool> CreateUserClimbingRoute(UserClimbingRoutes userClimbingRoute)
        {
            return await _repository.CreateUserClimbingRoute(userClimbingRoute);
        }

        public async Task<double?> GetRatingForClimbingRoute(int climbingRouteId)
        {
            return await _repository.GetRatingForClimbingRoute(climbingRouteId);
        }

        public async Task<bool> UpdateUserClimbingRoute(UserClimbingRoutes userClimbingRoute)
        {
            return await _repository.UpdateUserClimbingRoute(userClimbingRoute);
        }

        public async Task<bool> DeleteUserClimbingRoute(int userId, int climbingRouteId)
        {
            return await _repository.DeleteUserClimbingRoute(userId, climbingRouteId);
        }
    }

}
