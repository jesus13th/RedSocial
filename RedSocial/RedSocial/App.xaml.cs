using System;

using RedSocial.Controllers;
using RedSocial.Models;
using RedSocial.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedSocial {
    public partial class App : Application {
        public static IRepository<User> Repository { get; private set; }
        public App() {
            InitializeComponent();

            MainPage = new MainPage();
            Repository = new MongoDBRepository();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
