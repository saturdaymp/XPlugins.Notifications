using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SaturdayMP.XPlugins.Notifications
{
    /// <summary>
    ///     Used to shedule and handle notifications.
    /// </summary>
    [UsedImplicitly]
    public class NotificationScheduler : INotificationScheduler
    {
        /// <summary>
        ///     The registered notification listener.
        /// </summary>
        private static INotificationListener _listener;

        /// <inheritdoc />
        public Guid Create(string title, string message)
        {
            throw new NotImplementedException("Should call the platform specific method.");
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, Dictionary<string, string> extraInfo)
        {
            throw new NotImplementedException("Should call the platform specific method.");
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, DateTime scheduleDate)
        {
            throw new NotImplementedException("Should call the platform specific method.");
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, DateTime scheduleDate, Dictionary<string, string> extraInfo)
        {
            throw new NotImplementedException();
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

        /// <summary>
        ///     Used to register a <see cref="INotificationListener" /> to handle
        ///     incoming notifications.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public static void RegisterListener([NotNull] INotificationListener listener)
        {
            if (listener == null) throw new ArgumentNullException(nameof(listener));

            _listener = listener;
        }

        /// <summary>
        ///     Called when a notification is recieved by the local platform.
        /// </summary>
        /// <param name="notification">
        ///     The notification that was recieved by the
        ///     local platform.
        /// </param>
        public static void Recieved([NotNull] Notification notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));

            _listener?.Recieved(notification);
        }
    }
}