﻿using BlazorCLIMB.Data.Dapper.DTOs;
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
                Role = "User",  // Rol por defecto
                Img = model.Img
            };
            return await _authRepository.CreateUser(userDto);
        }
        
        public async Task<User> GetUserByEmail(string email)
        {
            return await _authRepository.GetUserByEmail(email);
        }
        public async Task<User> GetUserById(int userId)
        {
            return await _authRepository.GetUserById(userId);
        }

        public async Task<AuthenticationResult> VerifyPassword(string email, string password)

        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email and password cannot be null or empty.");
            }
            return await _authRepository.VerifyPassword(email, password);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            
            return await _authRepository.GetAllUsers();
        }
        public async Task<bool> UpdateUser(User user)
        {
            
            return await _authRepository.UpdateUser(user);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            
            return await _authRepository.DeleteUser(userId);
        }


    }
}
