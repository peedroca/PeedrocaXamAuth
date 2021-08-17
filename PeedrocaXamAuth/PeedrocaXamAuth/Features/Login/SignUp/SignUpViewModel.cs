using MvvmHelpers;
using PeedrocaXamAuth.Common;
using PeedrocaXamAuth.Features.Home;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PeedrocaXamAuth.Features.Login.SignUp
{
    class SignUpViewModel : BaseViewModel
    {
        private string email;
        private string password;

        public SignUpViewModel()
        {
            SignUpCommand = new Command(SignUp);
        }

        private async void SignUp()
        {
            var service = DependencyService.Resolve<IAuthenticationService>();
            if (await service.CreateUser(Email, Password))
            {
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

        public ICommand SignUpCommand { get; }

        #endregion Commands
    }
}
