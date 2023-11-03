using BlazorCLIMB.Data.Dapper.DTOs;
using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.Model;
using BlazorCLIMB.Model.BlazorCRUD.Model;
using BlazorCLIMB.UI.Data;
using BlazorCLIMB.UI.Interfaces;
using System.Threading.Tasks;
using static BlazorCLIMB.UI.Pages.RegisterCustom;

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

        public async Task<bool> CreateUser(AuthModel model)
        {
            UserDto userDto = new UserDto
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.Password,
                Role = "User",  // Puedes cambiar esto según tus necesidades
                Img = model.Img
            };
            return await _authRepository.CreateUser(userDto);
        }


        public async Task<string> GetRolesForUser(string email)
        {
            return await _authRepository.GetUserRole(email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _authRepository.GetUserByEmail(email);
        }

        public async Task<AuthenticationResult> VerifyPassword(string email, string password)
        {
            return await _authRepository.VerifyPassword(email, password);
        }
    }
}
