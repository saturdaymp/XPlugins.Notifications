# XPlugins.Notifications
Provides a common interface to schedule notifications for Xamarin Forms.

# Installing
You can get the continous integration builds from [MyGet](https://www.myget.org/feed/Packages/saturdaymp).  Look for the XPlugins.Notifictions package.

# Quick Start
Assuming you have an existing Xamarin Forms application the below should get you sending notifications in a couple of minutes.  If you need further help please check out the ExampleClient projects in the source code.

## Platform Specific Setup
Once you have the NuGet package installed you need to setup a dependency reference in each project you want to us notifications in.  This is usually done in the 

### Android
For Android you register the NotificatonScheduler in the main activity:

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
Similar ot Andorid you need to register the NotificationScheduler but this time in your AppDelete FinishedLaunching method.

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


        // Launch the app.
        LoadApplication(new App());
        return base.FinishedLaunching(app, options);
    }

Notice that on iOS you also need to [prompt the user to allow notifications](https://developer.xamarin.com/guides/ios/application_fundamentals/notifications/local_notifications_in_ios/).

## Scheduling a Notification
You can schedule a notification using the following code in your Xamarin Forms PCL project:

    notificationScheduler = DependencyService.Get<INotificationScheduler>();
    var notificationNumber = notificationScheduler.Create("Schedule Now", $"Created: {DateTime.Now:G}, Scheduled: {DateTime.Now:G}");

The above code snippet will schedule a notification that will display right away.

If you use [Prism](https://github.com/PrismLibrary/Prism) in your application you don't need to use DependencyService to load the scheduler.  Instead you can just pass in a INotificationScheduler in your views constructors.

    public MyClass(INotificationScheduler notificationScheduler)
    {
      _notificationScheduler = notificationScheduler;
    }
    
    public void CreateNotification()
    {
      var notificationNumber = _notificationScheduler.Create("Title", "Message", Date.Now);
    }

The create method returns a unique identification number for the newly created notification.  If you need to cancel or update a notification then keep track of this number.  Otherwise you can just ignore this return value.

## Cancel a Notification
To cancel a notification you need to know the notification number.  If you didn't track the notification number when you created then shame on you.  Also you won't be able to cancel the notification.  Assuming you have the notification number then just do:

    notificationScheduler.Cancel(notificationNumber);
    
This method has no return value.  If the notification exists it was canceled.  If the notification does not exist then there is nothing to cancel so no error is raised.
    
# Acknowledgements
Project was inspired by [edsnider/Xamarin.Plugins](https://github.com/edsnider/Xamarin.Plugins) and [EgorBo/Toasts.Forms.Plugin](https://github.com/EgorBo/Toasts.Forms.Plugin).
