using BlazorCLIMB.Data.Dapper.DTOs;
using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> CreateUser(UserDto userDto);
        Task<User?> GetUserByEmail(string email);
        Task<AuthenticationResult> VerifyPassword(string email, string password);

        Task<string?> GetUserRole(string email);
        Task<bool> AssignRoleToUser(string email, string role);
    }
}
