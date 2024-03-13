﻿using RestaurantApi.Core.Application.Dtos.Account;
using RestaurantApi.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IUserService 
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
    }
}