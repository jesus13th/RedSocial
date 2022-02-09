using System.Text.Json;

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
        public static Credentials Credentials { get; private set; }

        public App() {
            InitializeComponent();
            Instance = this;
            Credentials = JsonSerializer.Deserialize<Credentials>(@"C:\RedSocial.json");

            MainPage = new NavigationPage(new LoginPage());

            RepositoryUsers = new MongoDBRepository<User>("Users");
            RepositoryPosts = new MongoDBRepository<Post>("Posts");
        }
    }
    public class Credentials {
        public MongoDB MongoDB;
        public AWS AWS;
    }
    public class MongoDB {
        public string ConnectionString;
    }
    public struct AWS {
        public string AccessKey;
        public string SecretKey;
        public string BucketName;
    }
}
