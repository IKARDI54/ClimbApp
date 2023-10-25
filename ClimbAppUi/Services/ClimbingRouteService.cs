using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.Model;
using BlazorCLIMB.UI.Data;
using System.Data.SqlClient;




    public class ClimbingRouteService : IClimbingRouteService
    {
        private readonly SqlConfiguration _configuration;
        private IClimbingRouteRepository _climbingRouteRepository;

        public ClimbingRouteService(SqlConfiguration configuration)
        {
            _configuration = configuration;
            _climbingRouteRepository = new ClimbingRouteRepository(configuration.ConnectionString);
        }

        public Task<bool> DeleteClimbingRoute(int id)
        {
            return _climbingRouteRepository.DeleteClimbingRoute(id);
        }

        public Task<IEnumerable<ClimbingRoute>> GetAllClimbingRoutes()
        {
            return _climbingRouteRepository.GetAllClimbingRoutes();
        }

        public Task<ClimbingRoute> GetClimbingRouteDetails(int id)
        {
            return _climbingRouteRepository.GetClimbingRouteDetails(id);
        }


        public Task<double> CalculateAverageRating(int climbingRouteId)
        {
            return _climbingRouteRepository.CalculateAverageRating(climbingRouteId);
        }

        public Task<bool> InsertClimbingRoute(ClimbingRoute climbingRoute)
        {
            if (climbingRoute.Id == 0)
                return _climbingRouteRepository.InsertClimbingRoute(climbingRoute);
            else
                return _climbingRouteRepository.UpdateClimbingRoute(climbingRoute);
        }

        public Task<bool> UpdateClimbingRoute(ClimbingRoute climbingRoute)
        {
            return _climbingRouteRepository.UpdateClimbingRoute(climbingRoute);
        }
    }

