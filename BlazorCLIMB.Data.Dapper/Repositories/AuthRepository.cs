using BlazorCLIMB.Data.Dapper.DTOs;
using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;



namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _dbConnection;
        


        public AuthRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> CreateUser(UserDto userDto)
        {
            var existingUser = await GetUserByEmail(userDto.Email);
            if (existingUser != null)
            {
                
                return false;
            }
            var hashedPassword = HashPassword(userDto.PasswordHash);
            var sql = @"INSERT INTO Users (Email, PasswordHash, Role, Name, Img) 
                VALUES (@Email, @PasswordHash, @Role, @Name, @Img)";
            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                Email = userDto.Email,
                PasswordHash = hashedPassword,
                Role = userDto.Role,
                Name = userDto.Name,
                Img = userDto.Img
            });
            return result > 0;
        }


        public async Task<User?> GetUserByEmail(string email)
        {
            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Users WHERE Email = @Email";
                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
            }
        }

        public async Task<User?> GetUserById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_dbConnection.ConnectionString))
                {
                    await connection.OpenAsync();
                    var sql = "SELECT * FROM Users WHERE Id = @Id";
                    return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el usuario por ID: {ex.Message}");
                return new User { Id = 41 };
            }
        }

        public async Task<AuthenticationResult> VerifyPassword(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user != null)
            {
                bool isVerified = VerifyHashedPassword(user.PasswordHash, password);
                if (isVerified)
                {
                    // Aquí generas el token para el usuario, podría ser con JWT o como lo manejes
                    string token = GenerateTokenForUser(user);
                    return new AuthenticationResult { IsSuccess = true, Token = token };
                }
            }
            return new AuthenticationResult { IsSuccess = false };
        }


        public async Task<bool> AssignRoleToUser(string email, string role)
        {
            var user = await GetUserByEmail(email);
            if (user != null && user.Role != role)
            {
                var sql = "UPDATE Users SET Role = @Role WHERE Email = @Email";
                var result = await _dbConnection.ExecuteAsync(sql, new { Email = email, Role = role });
                return result > 0;
            }
            return false;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var sql = @"UPDATE Users 
                    SET Email = @Email, 
                        PasswordHash = @PasswordHash, 
                        Role = @Role, 
                        Name = @Name, 
                        Img = @Img 
                    WHERE Id = @Id";

            var result = await _dbConnection.ExecuteAsync(sql, user);
            return result > 0;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new { Id = userId });
            return result > 0;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var sql = "SELECT * FROM Users";
            return await _dbConnection.QueryAsync<User>(sql);
        }
       


        private string HashPassword(string password)
        {
            return password; 
        }

        private bool VerifyHashedPassword(string storedPassword, string passwordToVerify)
        {
            return storedPassword == passwordToVerify; 
        }


        private string GenerateTokenForUser(User user)
        {
            
            return $"{user.Id}_{DateTime.UtcNow.Ticks}";
        }

       
    }
}

