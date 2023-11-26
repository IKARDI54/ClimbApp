using BlazorCLIMB.Data.Dapper.DTOs;
using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> CreateUser(UserDto userDto);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int id);
        Task<AuthenticationResult> VerifyPassword(string email, string password);

        Task<bool> AssignRoleToUser(string email, string role);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int userId);
    }
}
