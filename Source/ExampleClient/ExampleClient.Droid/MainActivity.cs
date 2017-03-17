using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using SaturdayMP.XPlugins.Notifications.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ExampleClient.Droid
{
    [Activity(Label = "ExampleClient", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Setup Xamarin forms.
            Forms.Init(this, bundle);


            // Register the notification dependencies.
            DependencyService.Register<NotificationScheduler>();
            DependencyService.Register<NotificationListener>();


            // Start the application.
            LoadApplication(new App());
        }

        /// <summary>
        ///     This is called when a new notification is recieved.
        /// </summary>
        /// <param name="intent">The intent that contains information about the nofication.</param>
        /// <remarks>
        ///     This event could get triggered by non-notification intents but for this example
        ///     program it should only ever be notifications.
        /// </remarks>
        protected override void OnNewIntent(Intent intent)
        {
            // Let the parent do it's thing.
            base.OnNewIntent(intent);

            // Let the notification plugin translate the Andorid notification (i.e. Intent)
            // to a XPlugins notification.  This will also trigger the event in the portable
            // project assuming it has registered an observer.
            var listener = new NotificationListener();
            listener.NotificationRecieved(intent);
        }
    }
}