# XPlugins.Notifications
Provides a common interface to schedule notifications for Xamarin Forms.

# Installing
You can get the continous integration builds from [MyGet](https://www.myget.org/feed/saturdaymp/package/nuget/SaturdayMP.XPlugin.Notifications).  Look for the XPlugins.Notifictions package.

# Schedule NotificationQuick Start
Assuming you have an existing Xamarin Forms application the below should get you sending notifications in a couple of minutes.  If you need further help please check out the ExampleClient projects in the source code.

## Platform Specific Setup
Once you have the NuGet package installed you need to setup a dependency reference in each project you want to use notifications in.  This is usually done in the 

### Android
For Android you register the NotificatonScheduler in the main activity:

```C#
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
```

### iOS
Similar to Andorid you need to register the NotificationScheduler but this time in your AppDelete FinishedLaunching method.

```C#
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
```

Notice that on iOS you also need to [prompt the user to allow notifications](https://developer.xamarin.com/guides/ios/application_fundamentals/notifications/local_notifications_in_ios/).

## Scheduling a Notification
You can schedule a notification using the following code in your Xamarin Forms PCL project:

```C#
notificationScheduler = DependencyService.Get<INotificationScheduler>();
var notificationNumber = notificationScheduler.Create("Schedule Now", $"Created: {DateTime.Now:G}, Scheduled: {DateTime.Now:G}");
```

The above code snippet will schedule a notification that will display right away.

If you use [Prism](https://github.com/PrismLibrary/Prism) in your application you don't need to use DependencyService to load the scheduler.  Instead you can just pass in a INotificationScheduler in your views constructors.

```C#
public MyClass(INotificationScheduler notificationScheduler)
{
  _notificationScheduler = notificationScheduler;
}
    
public void CreateNotification()
{
  var notificationNumber = _notificationScheduler.Create("Title", "Message", Date.Now);
}
```

The create method returns a unique identification number for the newly created notification.  If you need to cancel or update a notification then keep track of this number.  Otherwise you can just ignore this return value.

## Cancel a Notification
To cancel a notification you need to know the notification number.  If you didn't track the notification number when you created then shame on you.  Also you won't be able to cancel the notification.  Assuming you have the notification number then just do:

```C#
notificationScheduler.Cancel(notificationNumber);
```

This method has no return value.  If the notification exists it was canceled.  If the notification does not exist then there is nothing to cancel so no error is raised.

## Find a Notification
If need be you can find an existing notification as such:

```C#
var foundNotification = notificationScheduler.Find(notificationNumber);
   
if (foundNotification != null)
{
  Console.Writeline(foundNotification.Id.ToString());
  Console.Writeline(foundNotification.Title);
  Console.Writeline(foundNotification.Message);
}
```

Will only find notifications that have not been canceled.  On iOS this will only return notifications in the future.  It won't find notifications in the past.

# Receive Notifications Quick Start
Now that you can send notifications it would be nice to react when you get one.  The below will let your Xamarin Forms handle incoming local notifications.

## Platform Specific Setup
Similar to scheduling notifications it requires a small amount of platform specific code.

### Android
When you schedule a notification in Android it is setup to reuse your existing activity but with a new intent.  Override the OnNewIntent in the MainActivity:

```C#
public class MainActivity : FormsAppCompatActivity
{

    protected override void OnCreate(Bundle bundle)
    {
        // Code we aren't intersted in.
    }
    
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
```

### iOS
In iOS override the ReceivedLocalNotification method in the AppDelete class:

```C#
public class AppDelegate : FormsApplicationDelegate
{

    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        // Code we aren't interested in.
    }

    public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
    {
        // The XPlugin know that we recieved a local notifiation.  It will
        // take are of the rest.
        var listener = new NotificationListener();
        listener.NotificationReceived(notification);
    }
}
```

## Handling Notifications
In your portable class project you need to subscribe to receive incoming notifications.  To do this create class that implments the INotificationObserver interface and it's one method NotificationRecieved.  For example:

```C#
public class NotificationObserver : INotificationObserver
{
    public void NotificationReceived(Notification notification)
    {
        Console.WriteLine(notification.Id);
        Console.WriteLine(notification.Title);
        Console.WriteLine(notification.Message);
    }
}
```

The below example displays a new page when a notification is recieved with the details of the notification.

```C#
public class NotificationObserver : INotificationObserver
{
    /// <summary>
    ///     When a notification is recieved update the repository and show
    ///     the recieved notification to the user.
    /// </summary>
    /// <param name="notification"></param>
    public void NotificationReceived(Notification notification)
    {
        // Copy the notification values to the view model
        var viewModel = new NotificationRecievedViewModel
        {
            Id = notification.Id,
            Title = notification.Title,
            Message = notification.Message
        };

        // Only copy the extra info if it exists.
        if (notification.ExtraInfo.ContainsKey("ExtraInfoOne"))
            viewModel.ExtraInfoOne = notification.ExtraInfo["ExtraInfoOne"];

        if (notification.ExtraInfo.ContainsKey("ExtraInfoTwo"))
            viewModel.ExtraInfoTwo = notification.ExtraInfo["ExtraInfoTwo"];


        // Save the recieved view model.
        ScheduledNotificationRepository.NotificationRecieved(notification.Id, viewModel.Title, viewModel.Message, viewModel.ExtraInfoOne, viewModel.ExtraInfoTwo);


        // Show the notifcation page.
        var notificationPage = new NotificationRecievedPage(viewModel);
        Application.Current.MainPage.Navigation.PushAsync(notificationPage);
    }
}
```

Then register your INotificationObserver class as shown below.  This example shows subscribing at the application start but that's not required.

```C#
public partial class App
{
    public App()
    {
        InitializeComponent();

        // Register the notification listener.
        NotificationListener.Subscribe(new NotificationObserver());

        MainPage = new NavigationPage(new Views.MainPage());
    }
}
```

## Known Issues
Don't forget to unsubscribe if you no longer wish to receive local notifications.  This usually won't be a problem because, I think, most applications will subscribe to receive notifications when they start and continue to receive them until terminated.

I'm also not 100% happy with the notification listener implementation.  If you have any suggestions please let me know.

# Acknowledgements
Project was inspired by [edsnider/Xamarin.Plugins](https://github.com/edsnider/Xamarin.Plugins) and [EgorBo/Toasts.Forms.Plugin](https://github.com/EgorBo/Toasts.Forms.Plugin).
