using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Firebase.Auth;
using PeedrocaXamAuth.Common;
using System;
using System.Threading.Tasks;

namespace PeedrocaXamAuth.Droid
{
    public class FirebaseAuthentication : IAuthenticationService
    {
        public async Task<bool> CreateUser(string email, string password)
        {
            try
            {
                var authResult = await FirebaseAuth.Instance
                    .CreateUserWithEmailAndPasswordAsync(email, password);

                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult(false);
            }
        }

        public void SignInWithGoogle(Action<string, string, string> LoggedIn)
        {
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken("935505272588-g4iu177167ucea5btmj9qjf0gj6h5ote.apps.googleusercontent.com")
                .RequestEmail()
                .Build();

            GoogleApiClient googleApiClient = new GoogleApiClient.Builder(MainActivity.Context)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();

            var intent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);
            
            MainActivity.LoggedIn = LoggedIn;
            MainActivity.Context.StartActivityForResult(intent, 1);
        }

        public bool IsSignIn()
        {
            var currentUser = FirebaseAuth.Instance.CurrentUser;
            return currentUser != null;
        }

        public async Task<string> SignIn(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance
                .SignInWithEmailAndPasswordAsync(email, password);

            var token = authResult.User.GetIdToken(false);
            return authResult.User.Uid;
        }

        public void SignOut()
        {
            FirebaseAuth.Instance.SignOut();
        }
    }
}