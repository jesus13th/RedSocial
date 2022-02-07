using System;

using RedSocial.Controllers;
using RedSocial.Controllers.Repositories;
using RedSocial.Models;
using RedSocial.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedSocial {
    public partial class App : Application {
        public static App Instance { get; private set; }
        public User userLogged { get; set; }
        public static IRepository<User> RepositoryUsers { get; private set; }
        public static IRepository<Post> RepositoryPosts { get; private set; }
        public App() {
            InitializeComponent();
            Instance = this;
            MainPage = new NavigationPage(new LoginPage());

            RepositoryUsers = new MongoDBRepository<User>("Users");
            RepositoryPosts = new MongoDBRepository<Post>("Posts");
        }
    }
}
