using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using PeedrocaXamAuth.Common;
using Android.Content;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Firebase.Auth;
using Android.Gms.Tasks;
using Xamarin.Forms.Platform.Android;

namespace PeedrocaXamAuth.Droid
{
    [Activity(Label = "PeedrocaXamAuth", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : FormsAppCompatActivity, IOnSuccessListener
    {
        internal static FormsAppCompatActivity Context { get; private set; }
        internal static Action<string, string, string> LoggedIn { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var options = new Firebase.FirebaseOptions.Builder()
              .SetProjectId("peedrocaxamauth")
              .SetApplicationId("peedrocaxamauth")
              .SetApiKey("AIzaSyA3pEdRkS0ekbR4uqbhTX2kyNlgR2mGFgM")
              .SetDatabaseUrl("https://peedrocaxamauth.firebaseio.com")
              .SetStorageBucket("peedrocaxamauth.appspot.com")
              .Build();

            Firebase.FirebaseApp.InitializeApp(this, options);
            Context = this;

            DependencyService.Register<IAuthenticationService, FirebaseAuthentication>();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                if (result.IsSuccess)
                {
                    GoogleSignInAccount account = result.SignInAccount;
                    LoginWithGoogle(account);
                }
            }
        }

        public void LoginWithGoogle(GoogleSignInAccount account)
        {
            var credentials = GoogleAuthProvider.GetCredential(account.IdToken, null);
            FirebaseAuth.Instance.SignInWithCredential(credentials)
                .AddOnSuccessListener(this);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            LoggedIn.Invoke(user.DisplayName, user.Email, user.PhotoUrl.ToString());
        }
    }
}