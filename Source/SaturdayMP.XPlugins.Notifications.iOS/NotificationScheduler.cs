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

        #region Create Notification
        
        /// <inheritdoc />
        public Guid Create(string title, string message)
        {
            // Create a notification with no extra info.
            return Create(title, message, new Dictionary<string, string>());
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, Dictionary<string, string> extraInfo)
        {
            return Create(title, message, DateTime.Now, extraInfo);
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, DateTime scheduleDate)
        {
            return Create(title, message, new Dictionary<string, string>());
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, DateTime scheduleDate, Dictionary<string, string> extraInfo)
        {
            // Make sure the extra info is supplied but can be empty.
            if (extraInfo == null) throw new ArgumentNullException(nameof(extraInfo));


            // Create the unique ID for this notification.
            var notificationId = Guid.NewGuid();
            extraInfo.Add(NotificationIdKey, notificationId.ToString("N"));


            // Create the iOS notification.
            var notification = new UILocalNotification
            {
                AlertTitle = title,
                AlertBody = message,
                FireDate = NSDate.Now,
                UserInfo = NSDictionary.FromObjectsAndKeys(extraInfo.Values.ToArray<object>(), extraInfo.Keys.Cast<object>().ToArray())
            };


            // Schedule the notification.
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);


            // All done.
            return notificationId;
        }

        /// <inheritdoc />
        public Notification Find(Guid notificationId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Cancel(Guid notificationId)
        {
            throw new NotImplementedException();
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
            var notification = ConvertLocalNotification(localNotification);

            Notifications.NotificationScheduler.Recieved(notification);
        }

        /// <summary>
        ///     Converts a <see cref="UILocalNotification" /> to a <see cref="Notification" />
        /// </summary>
        /// <param name="localNotification">
        ///     The local notification to convert to a
        ///     <see cref="Notification" />
        /// </param>
        /// <returns>The converted notification.</returns>
        /// <remarks>
        ///     For more information about the extra info types
        ///     supported see <see cref="CastNsObject" />.
        /// </remarks>
        private static Notification ConvertLocalNotification(UILocalNotification localNotification)
        {
            // Copy over the title and message.
            var notification = new Notification
            {
                Title = localNotification.AlertTitle,
                Message = localNotification.AlertBody
            };

            // Get the extra info.
            var d = new Dictionary<string, object>();
            foreach (var item in localNotification.UserInfo)
                d.Add((NSString) item.Key, CastNsObject(item.Value));
            notification.ExtraInfo = d;

            return notification;
        }

        /// <summary>
        ///     Casts a NS object to a .NET object.
        /// </summary>
        /// <param name="valueToCast">The <see cref="NSObject" /> to case.</param>
        /// <returns>Returns the cast object.  If the object cannot be cast then null is returned.</returns>
        /// <remarks>
        ///     For extra info the following conversions are supported:
        ///     <example>
        ///         <see cref="NSNumber" /> to <see cref="int" />
        ///         <see cref="NSString" /> to <see cref="string" />
        ///     </example>
        /// </remarks>
        private static object CastNsObject(NSObject valueToCast)
        {
            if (valueToCast.GetType() == typeof(NSNumber))
                return (int) (NSNumber) valueToCast;

            if (valueToCast.GetType() == typeof(NSString))
                return (string) (NSString) valueToCast;

            return null;
        }

        #endregion
    }
}