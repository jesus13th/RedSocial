using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RedSocial.Controllers;
using RedSocial.Controllers.AWS;
using RedSocial.Models;

using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedSocial.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage {
        public User User => App.Instance.userLogged;
        public List<Post> Posts { get; private set; } = new List<Post>();
        private FileResult selectedPhoto;
        private string photoName;

        public ProfilePage() {
            InitializeComponent();
            BindingContext = this;
        }
        protected override async void OnAppearing() {
            base.OnAppearing();

            Posts = (List<Post>) await App.RepositoryPosts.Read();
            ListPosts.ItemsSource = Posts;
        }

        private async void uploadPhotoButton_Clicked(object sender, EventArgs e) {
            selectedPhoto = await MediaPicker.PickPhotoAsync(new MediaPickerOptions() { Title = "Selecciona una foto" });
            postPrevImg.IsVisible = true;
            var stream = await selectedPhoto.OpenReadAsync();
            postPrevImg.Source = ImageSource.FromStream(() => stream);
        }

        private async void publishButton_Clicked(object sender, EventArgs e) {
            photoName = await AWSFileManager.UploadFile(selectedPhoto.FullPath);

            Post post = new Post(App.Instance.userLogged.Id, descriptionPostEntry.Text, photoName);
            await post.Publish();

            Posts.Add(post);
            ListPosts.ItemsSource = Posts;

            postPrevImg.Source = null;
            selectedPhoto = null;
            postPrevImg.IsVisible = false;
            photoName = null;
            descriptionPostEntry.Text = null;

            await this.DisplayToastAsync("Se ha realizado la publicacion");
        }
        private async void profileImg_Clicked(object sender, EventArgs e) {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions() { Title = "Selecciona una foto de perfil" });
            var photoName = await AWSFileManager.UploadFile(photo.FullPath);
            var stream = await photo.OpenReadAsync();

            var newUser = App.Instance.userLogged;
            newUser.ProfileImgName = photoName;

            await App.RepositoryUsers.Update(newUser, u => u.Id == newUser.Id);

            profileImg.Source = ImageSource.FromStream(() => stream);

        }
    }
}