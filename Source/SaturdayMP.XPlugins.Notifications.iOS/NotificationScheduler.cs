using Foundation;
using UIKit;

namespace SaturdayMP.XPlugins.Notifications.iOS
{
    /// <summary>
    /// Used to schedule local notifications on iOS.
    /// </summary>
    public class NotificationScheduler : INotificationScheduler
    {
        // TODO: Comments
        /// <inheritdoc />
        public int Create(string title, string message)
        {
            var notification = new UILocalNotification
            {
                AlertAction = title,
                AlertBody = message,
                FireDate = NSDate.Now
            };

            UIApplication.SharedApplication.ScheduleLocalNotification(notification);

            return 0;

        }
    }
}