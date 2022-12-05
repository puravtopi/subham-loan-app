
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.ViewModel;

namespace WebApi.Services.Interface
{
    public interface IAuthenticateService
    {
        ServiceResponse<SignupVM> Register(SignupVM model, string origin);

        ServiceResponse<UserMasterVM> Login(LoginVM model, string ipAddress);

        public ServiceResponse<bool> VerifyEmail(string token);

        ServiceResponse<bool> checkEmail(string email);

        ServiceResponse<bool> ForgotPassword(string email, string origin);

        ServiceResponse<bool> ResetPassword(ResetPasswordRequest model);

    }
}
