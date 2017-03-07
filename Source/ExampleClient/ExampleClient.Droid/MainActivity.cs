using Android.App;
using Android.Content.PM;
using Android.OS;
using SaturdayMP.XPlugins.Notifications.Droid;
using Xamarin.Forms;

namespace ExampleClient.Droid
{
    [Activity(Label = "ExampleClient", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Setup Xamarin forms.
            Forms.Init(this, bundle);


            // Register the notification dependency.
            DependencyService.Register<NotificationScheduler>();


            // Start the application.
            LoadApplication(new App());
        }
    }
}

