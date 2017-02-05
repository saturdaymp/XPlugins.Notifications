using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace SaturdayMP.XPlugins.Notifications.iOS
{
    /// <summary>
    ///     Used to schedule local notifications on iOS.
    /// </summary>
    public class NotificationScheduler : INotificationScheduler
    {
        /// <summary>
        ///     The key used to hold the notificiation ID in the user info
        ///     info dictonary.  Is hopefully unique enough that
        ///     the caller won't use the same key.
        /// </summary>
        private const string NotificationIdKey = "notificationidkey";

        #region Cancel

        /// <inheritdoc />
        public void Cancel(string notificationId)
        {
            // If we make UI calls outside of the main thread then an exception
            // will be thrown.  So make sure we are in the main thread.
            if (!NSThread.IsMain)
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() => Cancel(notificationId));

                return;
            }


            // Find the notification then cancel it.
            var foundUiNotification = FindUiNotifiaction(notificationId);
            if (foundUiNotification != null)
                UIApplication.SharedApplication.CancelLocalNotification(foundUiNotification);
        }

        #endregion

        #region Recieved Notification

        /// <summary>
        ///     A iOS notification was recieved.
        /// </summary>
        /// <param name="localNotification">The notifictaion recieved by iOS.</param>
        /// <remarks>
        ///     Take the iOS notification and transform it into a <see cref="SaturdayMP.XPlugins.Notifications.Notification" />
        ///     befoce calling <see cref="SaturdayMP.XPlugins.Notifications.NotificationScheduler.Recieved(Notification)" />.
        /// </remarks>
        public static void Recieved(UILocalNotification localNotification)
        {
            var notification = ConvertToNotification(localNotification);

            Notifications.NotificationScheduler.Recieved(notification);
        }

        #endregion

        #region Conversion

        /// <summary>
        ///     Converts a <see cref="UILocalNotification" /> to a <see cref="Notification" />
        /// </summary>
        /// <param name="uiNotification">
        ///     The local notification to convert to a
        ///     <see cref="Notification" />
        /// </param>
        /// <returns>The converted notification.</returns>
        /// <remarks>
        ///     For more information about the extra info types
        ///     supported see <see cref="CastNsObject" />.
        /// </remarks>
        private static Notification ConvertToNotification(UILocalNotification uiNotification)
        {
            // Copy over the title and message.
            var notification = new Notification
            {
                Title = uiNotification.AlertTitle,
                Message = uiNotification.AlertBody
            };

            // Get the extra info.
            var d = new Dictionary<string, object>();
            foreach (var item in uiNotification.UserInfo)
                d.Add(item.Key.ToString(), item.Value.ToString());
            notification.ExtraInfo = d;

            return notification;
        }

        #endregion

        #region Create

        /// <inheritdoc />
        public string Create(string title, string message)
        {
            // Create a notification with no extra info.
            return Create(title, message, new Dictionary<string, string>());
        }

        /// <inheritdoc />
        public string Create(string title, string message, Dictionary<string, string> extraInfo)
        {
            return Create(title, message, DateTime.Now, extraInfo);
        }

        /// <inheritdoc />
        public string Create(string title, string message, DateTime scheduleDate)
        {
            return Create(title, message, scheduleDate, new Dictionary<string, string>());
        }

        /// <inheritdoc />
        public string Create(string title, string message, DateTime scheduleDate, Dictionary<string, string> extraInfo)
        {
            // If we make UI calls outside of the main thread then an exception
            // will be thrown.  So make sure we are in the main thread.
            if (!NSThread.IsMain)
            {
                var returnNotificationId = "";
                UIApplication.SharedApplication.InvokeOnMainThread(
                    () => returnNotificationId = Create(title, message, scheduleDate, extraInfo));

                return returnNotificationId;
            }


            // Make sure the extra info is supplied but can be empty.
            if (extraInfo == null) throw new ArgumentNullException(nameof(extraInfo));


            // Create the unique ID for this notification.
            var notificationId = Guid.NewGuid().ToString();
            extraInfo.Add(NotificationIdKey, notificationId);


            // Create the iOS notification.  Make sure you 
            // convert the schedule date to a local date so the
            // NSDate will be create.
            var notification = new UILocalNotification
            {
                AlertTitle = title,
                AlertBody = message,
                FireDate = (NSDate) DateTime.SpecifyKind(scheduleDate, DateTimeKind.Local),
                UserInfo =
                    NSDictionary.FromObjectsAndKeys(extraInfo.Values.ToArray<object>(), extraInfo.Keys.ToArray<object>())
            };


            // Schedule the notification.
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);


            // All done.
            return notificationId;
        }

        #endregion

        #region Find

        /// <inheritdoc />
        public Notification Find(string notificationId)
        {
            // If we make UI calls outside of the main thread then an exception
            // will be thrown.  So make sure we are in the main thread.
            if (!NSThread.IsMain)
            {
                Notification returnNotification = null;
                UIApplication.SharedApplication.InvokeOnMainThread(() => returnNotification = Find(notificationId));

                return returnNotification;
            }

            // Try to find the UI notification.  If we find it then convert it
            // to a notification.
            var foundNotification = FindUiNotifiaction(notificationId);
            return foundNotification == null ? null : ConvertToNotification(foundNotification);
        }

        /// <summary>
        ///     Finds an existing notification.
        /// </summary>
        /// <param name="notificationId">Then notification to find.</param>
        /// <returns>The found notification or null if the notification was not found.</returns>
        /// <remarks>
        ///     If the notification has already passed then it won't be found.
        /// </remarks>
        private static UILocalNotification FindUiNotifiaction(string notificationId)
        {
            // Loop through all the notifications looking for ours.  Note that the local
            // notifications list only contains notifications that are in the future.  You won't
            // find past notifications.
            var localNotifications = UIApplication.SharedApplication.ScheduledLocalNotifications;
            foreach (var ln in localNotifications)
                if (ln.UserInfo.ContainsKey((NSString) NotificationIdKey))
                    if ((NSString) ln.UserInfo[NotificationIdKey] == notificationId)
                        return ln;

            // Didn't find the notification.
            return null;
        }

        #endregion
    }
}