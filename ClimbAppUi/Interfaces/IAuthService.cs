using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;
using System.Threading.Tasks;

namespace BlazorCLIMB.UI.Interfaces
{
    public interface IAuthService
    {
        Task<bool> CreateUser(string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<bool> VerifyPassword(string email, string password);
        Task<bool> AssignRoleToUser(string email, string role);
        Task<string> GetRolesForUser(string email);
    }
}

