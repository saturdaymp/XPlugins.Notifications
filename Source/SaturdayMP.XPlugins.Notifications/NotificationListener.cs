using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SaturdayMP.XPlugins.Notifications
{
    /// <summary>
    ///     Allows clients to listen for notifications.
    /// </summary>
    public abstract class NotificationListener
    {
        #region Subscribe/Unsubscribe

        /// <summary>
        ///     A list of who is listening for notifications.
        /// </summary>
        [NotNull]
        private static readonly List<INotificationObserver> Observers = new List<INotificationObserver>();

        /// <summary>
        ///     Subscribe to be notified when an notification is recieved.  Don't forget to unsubscibe
        ///     when you no longer wish to receive notifications.
        /// </summary>
        /// <param name="observer">The class that should recieve the notification.</param>
        public static void Subscribe([NotNull] INotificationObserver observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            Observers.Add(observer);
        }

        /// <summary>
        ///     Unsubscribe from recieving notifications.
        /// </summary>
        /// <param name="observer">The observer to unsubscibe.</param>
        public static void UnSubscribe([NotNull] INotificationObserver observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            Observers.Remove(observer);
        }

        #endregion

        #region NotificationRecived

        /// <summary>
        ///     Called when a local notifiaction is recieved by a specific device.
        /// </summary>
        /// <param name="notification">The notification the device recieved.</param>
        /// <remarks>
        ///     Each device specific implmeneation of the XPlugin is reponsible for
        ///     translating the device specific notification to a XPlugin notifiation
        ///     then calling this method.  For example:
        ///     <code>
        ///         public void NotificationReceived(DeviceNotifiaction deviceNotification)
        ///         {
        ///             // Translate the notification.
        ///             var xpluginNotifiaction = TranslateDeviceNotification(deviceNotification);
        /// 
        ///             // Notifiy the observers that notifiaction was recived.
        ///             NotificationReceived(xpluginNotification);
        ///     </code>
        /// </remarks>
        protected static void NotificationReceived([NotNull] Notification notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));

            foreach (var o in Observers)
                o.NotificationReceived(notification);
        }

        #endregion

    }
}