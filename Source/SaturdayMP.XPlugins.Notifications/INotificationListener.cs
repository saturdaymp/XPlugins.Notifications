using JetBrains.Annotations;

namespace SaturdayMP.XPlugins.Notifications
{
    /// <summary>
    ///     Used to handled recieved notifications.
    /// </summary>
    public interface INotificationListener
    {
        /// <summary>
        ///     A notification was recieved on the local platform.
        /// </summary>
        /// <param name="notification">The notification that was recived.</param>
        void Recieved([NotNull] Notification notification);
    }
}