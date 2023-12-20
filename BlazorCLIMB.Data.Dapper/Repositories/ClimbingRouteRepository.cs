using BlazorCLIMB.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public class ClimbingRouteRepository : IClimbingRouteRepository
    {
        private readonly string ConnectionString;

        public ClimbingRouteRepository(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<bool> DeleteClimbingRoute(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM ClimbingRoute
                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql.ToString(), new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<ClimbingRoute>> GetAllClimbingRoutes()
        {
            try
            {
                var db = dbConnection();

                var sql = @"
        SELECT CR.Id, CR.Name, CR.Grade, CR.Description, CR.ClimbingSector, CR.ClimbingSchoolId,
               COALESCE(AVG(RR.Rating), 0.0) AS AverageRating,
               COUNT(RR.Id) AS NumberOfRatings
        FROM ClimbingRoute CR
        LEFT JOIN RouteRating RR ON CR.Id = RR.ClimbingRouteId
        GROUP BY CR.Id, CR.Name, CR.Grade, CR.Description, CR.ClimbingSector, CR.ClimbingSchoolId";

            var result = await db.QueryAsync<ClimbingRoute>(sql);

            return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error en GetClimbingRouteDetails: {ex.Message}");
                throw;
            }
            }

        public async Task<ClimbingRoute> GetClimbingRouteDetails(int id)
        {
            try
            {
                using var db = dbConnection(); // Utiliza 'using' para manejar automáticamente la conexión

                if (db == null)
                {
                    throw new InvalidOperationException("La conexión a la base de datos no pudo ser establecida.");
                }

                var sql = @"SELECT Id, Name, Grade, Description, ClimbingSchoolId, ClimbingSector  
            FROM ClimbingRoute
            WHERE Id = @id";

                var result = await db.QueryFirstOrDefaultAsync<ClimbingRoute>(sql, new { id });
                if (result == null)
                {
                    throw new KeyNotFoundException($"No se encontró una ruta de escalada con el ID: {id}");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetClimbingRouteDetails: {ex.Message}");
                throw; // Vuelve a lanzar la excepción para manejar error
            }
        }


        public async Task<bool> InsertClimbingRoute(ClimbingRoute climbingRoute)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO ClimbingRoute (Name, Grade, Description, ClimbingSchoolId, ClimbingSector)
                        VALUES (@Name, @Grade, @Description, @ClimbingSchoolId, @ClimbingSector )";

            var result = await db.ExecuteAsync(sql.ToString(),
                new
                {
                    climbingRoute.Name,
                    climbingRoute.Grade,
                    climbingRoute.Description,
                    climbingRoute.ClimbingSchoolId,
                    climbingRoute.ClimbingSector,


                });

            return result > 0;
        }

        public async Task<bool> UpdateClimbingRoute(ClimbingRoute climbingRoute)
        {
            var db = dbConnection();

            var sql = @"UPDATE ClimbingRoute 
                        SET Name = @Name, Grade = @Grade, Description = @Description,
                            ClimbingSector = @ClimbingSector, ClimbingSchoolId = @ClimbingSchoolId

                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql.ToString(),
                new
                {
                    climbingRoute.Name,
                    climbingRoute.Grade,
                    climbingRoute.Description,
                    climbingRoute.ClimbingSector,
                    climbingRoute.ClimbingSchoolId,
                    climbingRoute.Id
                });

            return result > 0;
        }
        public async Task<double> CalculateAverageRating(int climbingRouteId)
        {
            var db = dbConnection();

            var sql = @"SELECT AVG(Rating) 
                FROM RouteRating
                WHERE ClimbingRouteId = @ClimbingRouteId";

            var averageRating = await db.ExecuteScalarAsync<double?>(sql, new { ClimbingRouteId = climbingRouteId });

            return averageRating ?? 0.0;
        }
        public async Task<IEnumerable<SchoolRoutesCount>> GetRoutesCountBySchool()
        {
            var db = dbConnection();

            var sql = @"
        SELECT CR.ClimbingSchoolId, COUNT(CR.Id) AS NumberOfRoutes
        FROM ClimbingRoute CR
        GROUP BY CR.ClimbingSchoolId";

            return await db.QueryAsync<SchoolRoutesCount>(sql);
        }

        public async Task<IEnumerable<SchoolAverageRating>> GetAverageRatingBySchool()
        {
            var db = dbConnection();

            var sql = @"
        SELECT CR.ClimbingSchoolId, COALESCE(AVG(RR.Rating), 0.0) AS AverageRating, COUNT(RR.Rating) AS NumberOfRatings
        FROM ClimbingRoute CR
        LEFT JOIN RouteRating RR ON CR.Id = RR.ClimbingRouteId
        GROUP BY CR.ClimbingSchoolId";

            return await db.QueryAsync<SchoolAverageRating>(sql);
        }

        public async Task<IEnumerable<SchoolAverageGrade>> GetAverageGradeBySchool()
        {
            var db = dbConnection();

            // Actualiza la consulta para usar la nueva columna NumericGrade
            var sql = @"
            SELECT CR.ClimbingSchoolId, AVG(CR.NumericGrade) AS AverageGrade
            FROM ClimbingRoute CR
            GROUP BY CR.ClimbingSchoolId";

            // Ejecuta la consulta y obtén los resultados directamente
            var averageGrades = await db.QueryAsync<SchoolAverageGrade>(sql);

            return averageGrades;
        }

        public class SchoolAverageGrade
        {
            public int ClimbingSchoolId { get; set; }
            public double AverageGrade { get; set; }
        }

        public class SchoolAverageRating
        {
            public int ClimbingSchoolId { get; set; }

            public int NumberOfRatings { get; set; }
            public double AverageRating { get; set; }
        }

        public class SchoolRoutesCount
        {
            public int ClimbingSchoolId { get; set; }
            public int NumberOfRoutes { get; set; }
        }

    }
}
