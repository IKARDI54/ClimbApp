using BlazorCLIMB.Data.Dapper.DTOs;
using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;
using Dapper;
using System.Data;
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
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }
        public async Task<User?> GetUserById(int id)
        {
            try
            {
                var sql = "SELECT * FROM Users WHERE Id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al obtener el usuario por ID: {ex.Message}");
                return new User { Id = 41 };
            }
        }

        public async Task<string?> GetUserRole(string email) 
        {
            var user = await GetUserByEmail(email);
            return user?.Role;
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


        // Método para hashing de contraseñas


        private string HashPassword(string password)
        {
            return password; // Simplemente devolvemos la contraseña como su "hash"
        }

        private bool VerifyHashedPassword(string storedPassword, string passwordToVerify)
        {
            return storedPassword == passwordToVerify; // Comparación directa
        }


        private string GenerateTokenForUser(User user)
        {
            
            return $"{user.Id}_{DateTime.UtcNow.Ticks}";
        }

       
    }
}

