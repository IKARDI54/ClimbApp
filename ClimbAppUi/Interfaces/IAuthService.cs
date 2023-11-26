using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;
using System.Threading.Tasks;
using static BlazorCLIMB.UI.Pages.RegisterCustom;

namespace BlazorCLIMB.UI.Interfaces
{
    public interface IAuthService
    {
        Task<bool> CreateUser(AuthModel model);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int userId);
        Task<AuthenticationResult> VerifyPassword(string email, string password);
        Task<bool> AssignRoleToUser(string email, string role);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int userId);
        Task<IEnumerable<User>> GetAllUsers();
    }
}

