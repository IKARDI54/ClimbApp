using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.Model;
using BlazorCLIMB.UI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

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
}

