using UIKit;

namespace SaturdayMP.XPlugins.Notifications.iOS
{

    public class NotificationListener : Notifications.NotificationListener
    {
        public void NotificationReceived(UILocalNotification uiNotification)
        {
            // Translate from iOS notification to XPlugin notification.
            var notification = NotificationScheduler.ConvertToNotification(uiNotification);

            // Let the observers know we got a notification.
            NotificationReceived(notification);
        }
    }
}
