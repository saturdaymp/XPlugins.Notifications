namespace SaturdayMP.XPlugins.Notifications
{
    /// <summary>
    /// Interface for scheduling a notification.
    /// </summary>
    public interface INotificationScheduler
    {
        /// <summary>
        /// Creates a new notification that is shown now.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <returns>A unique identification number for this notification.</returns>
        int Create(string title, string message);
    }
}
