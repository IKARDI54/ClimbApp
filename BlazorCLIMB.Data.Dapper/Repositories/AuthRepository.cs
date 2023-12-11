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
using BCrypt.Net;



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
            if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.PasswordHash))
            {
                throw new ArgumentException("El correo electrónico y la contraseña son obligatorios.");
            }

            var existingUser = await GetUserByEmail(userDto.Email);
            if (existingUser != null)
            {
                
                return false;
            }

            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                await connection.OpenAsync();

                var hashedPassword = HashPassword(userDto.PasswordHash);
            var sql = @"INSERT INTO Users (Email, PasswordHash, Role, Name, Img) 
                VALUES (@Email, @PasswordHash, @Role, @Name, @Img)";
            var result = await connection.ExecuteAsync(sql, new
            {
                Email = userDto.Email,
                PasswordHash = hashedPassword,
                Role = userDto.Role,
                Name = userDto.Name,
                Img = userDto.Img
            });
            return result > 0;
            }
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
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public async Task<AuthenticationResult> VerifyPassword(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user == null)
            {
                return new AuthenticationResult { IsSuccess = false };
            }

            // Comprobación para usuarios con contraseñas en formato antiguo
            if (IsPasswordInOldFormat(user.PasswordHash))
            {
                if (VerifyOldPassword(user.PasswordHash, password))
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                    await UpdateUser(user);  // Asegúrate de que este método actualice correctamente en la DB.
                    return new AuthenticationResult { IsSuccess = true, Token = GenerateTokenForUser(user) };
                }
                else
                {
                    return new AuthenticationResult { IsSuccess = false };
                }
            }
            else
            {
                // Para contraseñas en el formato encriptado con BCrypt (manejo anterior)
                if (!VerifyHashedPassword(user.PasswordHash, password))
                {
                    return new AuthenticationResult { IsSuccess = false };
                }

                return new AuthenticationResult { IsSuccess = true, Token = GenerateTokenForUser(user) };
            }
        }


        private bool IsPasswordInOldFormat(string passwordHash)
        {
            
            return passwordHash != null && passwordHash.Length < 60;
        }

        private bool VerifyOldPassword(string storedPassword, string passwordToVerify)
        {
         
            return storedPassword == passwordToVerify;
        }

        private bool VerifyHashedPassword(string storedPasswordHash, string passwordToVerify)
        {
            // Retorna true si la contraseña proporcionada, una vez hasheada, coincide con el hash almacenado
            return BCrypt.Net.BCrypt.Verify(passwordToVerify, storedPasswordHash);
        }
        private string GenerateTokenForUser(User user)
        {
            
            return $"{user.Id}_{DateTime.UtcNow.Ticks}";
        }

       
    }
}

