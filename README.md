# XPlugins.Notifications
Provides a common interface to schedule notifications for Xamarin Forms.

# Installing
NYI

# Quick Start
Assuming you have an existing Xamarin Forms application the below should get you sending notifications in a couple of minutes.  If you need further help please check out the ExampleClient projects in the source code.

## Platform Specific Setup
Once you have the NuGet package installed you need to setup a dependency reference in each project you want to us notifications in.  This is usually done in the 

### Android
For Android you register it in the main activity:

    Public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
      // Code we don't care about....

      // Setup Xamarin forms.
      Xamarin.Forms.Forms.Init(this, bundle);

      // Register the notification dependency.
      DependencyService.Register<NotificationScheduler>();
      
      // Start the application.
      LoadApplication(new App());
    }

### iOS
NYI

## Scheduling a Notification
You can schedule a notification using the following code in your Xamarin Forms PCL project:

    notificationScheduler = DependencyService.Get<INotificationScheduler>();
    notificationScheduler.Create("Schedule Now", $"Created: {DateTime.Now:G}, Scheduled: {DateTime.Now:G}");

The above code snippet will schedule a notification that will display right away.
    
# Acknowledgements
Project was inspired by [edsnider/Xamarin.Plugins](https://github.com/edsnider/Xamarin.Plugins) and [EgorBo/Toasts.Forms.Plugin](https://github.com/EgorBo/Toasts.Forms.Plugin).
