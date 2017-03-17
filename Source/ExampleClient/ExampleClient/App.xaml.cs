using SaturdayMP.XPlugins.Notifications;
using Xamarin.Forms;

namespace ExampleClient
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();


            // Register the notification listener.
            NotificationListener.Subscribe(new NotificationObserver());


            MainPage = new NavigationPage(new Views.MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
