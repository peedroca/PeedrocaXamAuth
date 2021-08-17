using System;
using System.Threading.Tasks;

namespace PeedrocaXamAuth.Common
{
    public interface IAuthenticationService
    {
        bool IsSignIn();
        Task<bool> CreateUser(string email, string password);
        Task<string> SignIn(string email, string password);
        void SignOut();
        void SignInWithGoogle(Action<string, string, string> LoggedIn);
    }
}
