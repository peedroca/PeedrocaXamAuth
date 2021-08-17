using PeedrocaXamAuth.Common;
using PeedrocaXamAuth.Features.Home;
using PeedrocaXamAuth.Features.Login.SignIn;
using PeedrocaXamAuth.Features.Login.SignUp;
using Xamarin.Forms;

namespace PeedrocaXamAuth
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var auth = DependencyService.Resolve<IAuthenticationService>();
            if (!auth.IsSignIn())
                MainPage = new SignInPage();
            else
                MainPage = new HomePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
