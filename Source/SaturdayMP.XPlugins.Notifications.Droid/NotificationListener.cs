using System;
using System.Linq;
using Android.Content;
using JetBrains.Annotations;

namespace SaturdayMP.XPlugins.Notifications.Droid
{
    public class NotificationListener : Notifications.NotificationListener
    {
        public void NotificationRecieved([NotNull] Intent notificationIntent)
        {
            // Is this a notification intent?  The action should look like:
            //
            //  <packagename>.NOTIFICATION-<ID>
            //
            var expectedActionName = NotificationScheduler.BuildActionName("");
            if (notificationIntent.Action == null || !notificationIntent.Action.StartsWith(expectedActionName))
            {
                // Not a notification intent.  Do nothing.
                return;
            }


            // The new empty notification.
            var extractedNotification = new Notification
            {
                Id = notificationIntent.Action.Substring(notificationIntent.Action.IndexOf("-", StringComparison.Ordinal) + 1), // The ID is part of the action.  
                Title = notificationIntent.GetStringExtra(NotificationScheduler.TitleExtrasKey), // These fields are extras but should exist.
                Message = notificationIntent.GetStringExtra(NotificationScheduler.MessageExtrasKey) // These fields are extras but should exist.
            };


            // Any other extras, remember to ignore the title and message keys.
            var extraKeys = notificationIntent.Extras.KeySet();
            extraKeys.Remove(NotificationScheduler.TitleExtrasKey);
            extraKeys.Remove(NotificationScheduler.MessageExtrasKey);

            extractedNotification.ExtraInfo = extraKeys.ToDictionary(key => key, notificationIntent.GetStringExtra);


            // Let everyone know that a notification was received.
            NotificationReceived(extractedNotification);
        }
    }
}