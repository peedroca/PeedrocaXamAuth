using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeedrocaXamAuth.Features.Login.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUnPage : ContentPage
    {
        public SignUnPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModel();
        }
    }
}