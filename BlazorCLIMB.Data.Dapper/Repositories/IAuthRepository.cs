using BlazorCLIMB.Model.BlazorCRUD.Model;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> CreateUser(string email, string password);
        Task<User?> GetUserByEmail(string email);
        Task<bool> VerifyPassword(string email, string password);
        Task<string?> GetUserRole(string email);
        Task<bool> AssignRoleToUser(string email, string role);
    }
}
