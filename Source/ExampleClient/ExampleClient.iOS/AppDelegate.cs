using Foundation;
using SaturdayMP.XPlugins.Notifications.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ExampleClient.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            // Check if the user wants notifications.
            var settings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                new NSSet());
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);


            // Register the notification dependency.  Don't forget to do this.
            DependencyService.Register<NotificationScheduler>();
            DependencyService.Register<NotificationListener>();


            // Launch the app.
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        ///     Handle a incoming iOS notification.
        /// </summary>
        /// <param name="application">See <see cref="FormsApplicationDelegate.ReceivedLocalNotification" /> for more info.</param>
        /// <param name="notification">See <see cref="FormsApplicationDelegate.ReceivedLocalNotification" /> for more info.</param>
        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            var listener = new NotificationListener();
            listener.NotificationReceived(notification);
        }
    }
}