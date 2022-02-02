using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using RedSocial.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;

namespace RedSocial.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        public bool IsPassword { get; set; }
        public LoginPage() {
            InitializeComponent();
            BindingContext = this;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) {
            string value = await DisplayActionSheet("Opciones", "Cerrar", null, "Crear cuenta nueva", "Olvide mi contraseña", "Ayuda");
            switch (value) {
                case "Crear cuenta nueva":
                await Navigation.PushAsync(new SigninPage());
                break;
                case "Olvide mi contraseña": break;
                case "Ayuda": break;
                default:
                break;
            }
        }

        private async void loginBtn_Clicked(object sender, EventArgs e) {
            var email = emailEntry.Text;
            var password = passwordEntry.Text;

            if (string.IsNullOrWhiteSpace(email)) {
                await this.DisplayToastAsync("Introduce un correo electronico valido");
                emailEntry.Focus();
                return;
            } else if(string.IsNullOrWhiteSpace(password)) {
                await this.DisplayToastAsync("Introduce una contraseña");
                passwordEntry.Focus();
                return;
            }
            var user = await App.Repository.Read(x => x.Email == email);

            if (user == null) {
                await this.DisplayToastAsync("el correo solicitado no esta registrado");
                return;
            } else {
                if (user.Password == password) {
                    Login(user);
                } else { 
                    await this.DisplayToastAsync("la contraseña es incorrecta owo");
                }
            }
        }
        private async void Login(User user) {
            /*if (true) {
                await SecureStorage.SetAsync("Password", user.Password);
                await SecureStorage.SetAsync("Password", user.Email);
            }*/
            await Navigation.PushAsync(new MainPage());
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) {
            passwordEntry.IsPassword = IsPassword = !IsPassword;
        }
    }
}