using MvvmHelpers;
using PeedrocaXamAuth.Common;
using PeedrocaXamAuth.Features.Login.SignIn;
using System.Windows.Input;
using Xamarin.Forms;

namespace PeedrocaXamAuth.Features.Home
{
    class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            LogoutCommand = new Command(Logout);
        }

        private void Logout()
        {
            DependencyService.Resolve<IAuthenticationService>().SignOut();
            App.Current.MainPage = new SignInPage();
        }

        #region Commands

        public ICommand LogoutCommand { get; }

        #endregion Commands
    }
}
