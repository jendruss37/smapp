using Flurl.Http;
using IdentityApi.Data;
using IdentityApi.Enums;
using IdentityApi.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace IdentityApi.Services
{
    public interface IIdentityService
    {
        Task<RegistrationResult> RegisterNewUser(NewUserDto newUserDto);
        Task<AuthenticationResult> AuthenticateUser(CredentialsDto credentialsDto);
        Task DeleteUser(int id);
    }
    public class IdentityService : IIdentityService
    {
        private readonly CredentialsDbContext _context;
        public IdentityService(CredentialsDbContext credentialsDbContext)
        {
            _context = credentialsDbContext;
        }
        public async Task<RegistrationResult> RegisterNewUser(NewUserDto newUserDto)
        {
            try
            {
                var isUserNameTaken = await _context.UserCredentials.AnyAsync(w => w.UserName == newUserDto.UserName);
                if (isUserNameTaken)
                {
                    return RegistrationResult.UserNameTakenError;
                }
                var creds = new Credentials(newUserDto);
                await _context.UserCredentials.AddAsync(creds);
                await _context.SaveChangesAsync();
                newUserDto.LoginId = creds.Id;
            }
            catch
            {
                return RegistrationResult.UnknownError;
            }
            await RegisterUserInfo(newUserDto);
            return RegistrationResult.Success;
        }
        public async Task<AuthenticationResult> AuthenticateUser(CredentialsDto credentialsDto)
        {
            try
            {
                var user = await _context.UserCredentials.FirstOrDefaultAsync(u => u.UserName == credentialsDto.UserName);
                if (user == null)
                {
                    return AuthenticationResult.NoUserError;
                }
                if (credentialsDto.Password != user.Password)
                {
                    return AuthenticationResult.IncorrectPasswordError;
                }
            }
            catch (Exception ex)
            {
                return AuthenticationResult.UnknownError;
            }
            return AuthenticationResult.Success;
        }
        public async Task DeleteUser(int id)
        {
            var user = await _context.UserCredentials.FindAsync(id);
            if (user != null)
            {
                await DeleteUserInfo(id);
                _context.UserCredentials.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        private async Task RegisterUserInfo(NewUserDto newUserDto)
        {
            var response = await "http://people_api:80/api/People/new".PostJsonAsync(newUserDto);
            if (response.StatusCode != 200)
            {
                throw new Exception(response.ResponseMessage.ToString());
            }
        }
        private async Task DeleteUserInfo(int id)
        {
            var response = await $"http://people_api:80/api/People/delete?loginId={id}".DeleteAsync();
            if (response.StatusCode != 200)
            {
                throw new Exception(response.ResponseMessage.ToString());
            }
        }
    }
}
