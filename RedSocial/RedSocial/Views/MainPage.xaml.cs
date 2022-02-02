using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedSocial.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage {
        public MainPage() {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed() {
            Debug.Write("No back");
            
            return true;
        }
    }
}