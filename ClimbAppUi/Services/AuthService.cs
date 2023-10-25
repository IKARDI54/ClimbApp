using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;
using BlazorCLIMB.UI.Data;
using BlazorCLIMB.UI.Interfaces;
using System.Threading.Tasks;

namespace BlazorCLIMB.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> AssignRoleToUser(string email, string role)
        {
            return await _authRepository.AssignRoleToUser(email, role);
        }

        public async Task<bool> CreateUser(string email, string password)
        {
            return await _authRepository.CreateUser(email, password);
        }

        public async Task<string> GetRolesForUser(string email)
        {
            return await _authRepository.GetUserRole(email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _authRepository.GetUserByEmail(email);
        }

        public async Task<bool> VerifyPassword(string email, string password)
        {
            return await _authRepository.VerifyPassword(email, password);
        }
    }
}
