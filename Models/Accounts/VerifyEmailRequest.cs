using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Accounts
{
    public class VerifyEmailRequest
    {
        
        public string Token { get; set; }

        public string Email { get; set; }
    }
}