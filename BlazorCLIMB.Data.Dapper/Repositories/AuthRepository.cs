using BlazorCLIMB.Model.BlazorCRUD.Model;
using Dapper;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;


namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _dbConnection;
        public const string DefaultUserRole = "User";


        public AuthRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> CreateUser(string email, string password)
        {
            try
            {



                var hashedPassword = HashPassword(password);
                var sql = "INSERT INTO Users (Email, PasswordHash, Role) VALUES (@Email, @PasswordHash, @Role)";
                var result = await _dbConnection.ExecuteAsync(sql, new { Email = email, PasswordHash = hashedPassword, Role = DefaultUserRole });
                return result > 0;
            }
            catch
            {
                return false;
            }
        }



        public async Task<User?> GetUserByEmail(string email) 
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task<string?> GetUserRole(string email) 
        {
            var user = await GetUserByEmail(email);
            return user?.Role;
        }


        public async Task<bool> VerifyPassword(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user != null)
            {
                return VerifyHashedPassword(user.PasswordHash, password);
            }
            return false;
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
            return password;
        }

        // Sin hashing, simplemente comparamos directamente las contraseñas
        private bool VerifyHashedPassword(string storedPassword, string passwordToVerify)
        {
            return storedPassword == passwordToVerify;
        }
    }
}

