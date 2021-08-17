using MvvmHelpers;
using PeedrocaXamAuth.Common;
using PeedrocaXamAuth.Features.Home;
using PeedrocaXamAuth.Features.Login.SignUp;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PeedrocaXamAuth.Features.Login.SignIn
{
    class SignInViewModel : BaseViewModel
    {
        private string email;
        private string password;

        public SignInViewModel()
        {
            SignInCommand = new Command(SignIn);
            SignUpCommand = new Command(SignUp);
            SignInWithGoogleCommand = new Command(SignInWithGoogle);
        }

        private void SignInWithGoogle()
        {
            var service = DependencyService.Resolve<IAuthenticationService>();
            service.SignInWithGoogle(LoggedIn);
        }

        private void LoggedIn(string username, string email, string photourl)
        {
            App.Current.MainPage.DisplayAlert("Logado com sucesso!", $"Usuário: {username}\nE-mail: {email}", "OK");
            App.Current.MainPage = new HomePage();
        }

        private void SignUp() =>
            App.Current.MainPage = new SignUnPage();

        private async void SignIn()
        {
            var service = DependencyService.Resolve<IAuthenticationService>();
            var uid = await service.SignIn(Email, Password);
            if (!string.IsNullOrEmpty(uid))
            {
                await App.Current.MainPage.DisplayAlert("Logado com sucesso!", uid, "OK");
                App.Current.MainPage = new HomePage();
            }
            else
            {
                Console.WriteLine("Ocorreu um erro na autenticação.");
            }
        }

        #region Properties

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        #endregion Properties

        #region Commands

        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand SignInWithGoogleCommand { get; }

        #endregion Commands
    }
}
