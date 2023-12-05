using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.Model;
using BlazorCLIMB.UI.Data;
using BlazorCLIMB.UI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BlazorCLIMB.Data.Dapper.Repositories.ClimbingRouteRepository;

namespace BlazorCLIMB.UI.Services
{
    public class ClimbingRouteService : IClimbingRouteService
{
    private readonly IClimbingRouteRepository _climbingRouteRepository;

    public ClimbingRouteService(IClimbingRouteRepository climbingRouteRepository)
    {
        _climbingRouteRepository = climbingRouteRepository;
    }

    public async Task<IEnumerable<ClimbingRoute>> GetAllClimbingRoutes()
    {
        return await _climbingRouteRepository.GetAllClimbingRoutes();
    }

    public async Task<ClimbingRoute> GetClimbingRouteDetails(int id)
    {
        return await _climbingRouteRepository.GetClimbingRouteDetails(id);
    }

    public async Task<bool> InsertClimbingRoute(ClimbingRoute climbingRoute)
    {
        return await _climbingRouteRepository.InsertClimbingRoute(climbingRoute);
    }

    public async Task<bool> UpdateClimbingRoute(ClimbingRoute climbingRoute)
    {
        return await _climbingRouteRepository.UpdateClimbingRoute(climbingRoute);
    }

    public async Task<bool> DeleteClimbingRoute(int id)
    {
        return await _climbingRouteRepository.DeleteClimbingRoute(id);
    }

    public async Task<double> CalculateAverageRating(int climbingRouteId)
    {
        return await _climbingRouteRepository.CalculateAverageRating(climbingRouteId);
    }

        public async Task<IEnumerable<SchoolRoutesCount>> GetRoutesCountBySchool()
        {
            return await _climbingRouteRepository.GetRoutesCountBySchool();
        }

        public async Task<IEnumerable<SchoolAverageRating>> GetAverageRatingBySchool()
        {
            return await _climbingRouteRepository.GetAverageRatingBySchool();
        }

        public async Task<IEnumerable<SchoolAverageGrade>> GetAverageGradeBySchool()
        {
            return await _climbingRouteRepository.GetAverageGradeBySchool();
        }
        
    }
}

