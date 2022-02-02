using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using RedSocial.Models;

namespace RedSocial.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SigninPage : ContentPage {
        public SigninPage() {
            InitializeComponent();
        }

        private async void singin_Clicked(object sender, EventArgs e) {
            var userName = userNameEntry.Text;
            var email = emailEntry.Text;
            var password = passwordEntry.Text;
            var repassword = repasswordEntry.Text;
            var born = bornDatePicker.Date;

            if (string.IsNullOrWhiteSpace(userName)) {
                await this.DisplayToastAsync("Ingrese un nombre de usuario");
                userNameEntry.Focus();
                return;
            } else if (string.IsNullOrWhiteSpace(email)) {
                await this.DisplayToastAsync("Ingrese un correo electronico");
                emailEntry.Focus();
                return;
            } else if (string.IsNullOrWhiteSpace(password)) {
                await this.DisplayToastAsync("Ingrese una contraseña");
                passwordEntry.Focus();
                return;
            } else if (string.IsNullOrWhiteSpace(repassword)) {
                await this.DisplayToastAsync("Vuelva a escribir la contraseña ingresada");
                repasswordEntry.Focus();
                return;
            } else if (password != repassword) {
                await this.DisplayToastAsync("las contraseñas no coinciden");
                passwordEntry.Focus();
                return;
            }
            if ((DateTime.Now - born).TotalDays / 365 < 14) {
                await this.DisplayToastAsync("debes tener minimo 14 años para registrarte :(");
                return;
            }
            User user = new User() { UserName = userName, Email = email, Password = password, DateOfBorn = born.ToString("d") };
            try {
                await App.Repository.Create(user);
                await this.DisplayToastAsync("El usuario se registro correctamente");
            } catch {
                await this.DisplayToastAsync("Hubo un error al registrar al usuario");
            }
        }
    }
}