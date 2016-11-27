using System;

namespace ExampleClient.Repositories
{
    /// <summary>
    ///     Tracks notfications that have been scheduled and received.
    /// </summary>
    internal class ScheduledNotificationModel
    {
        /// <summary>
        ///     The unique identifier for this notification.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Return the ID as string.
        /// </summary>
        public string IdAsString => Id.ToString();

        /// <summary>
        ///     The number of times the notification has been recieved.
        /// </summary>
        public int NumberTimesRecieved { get; set; }

        /// <summary>
        ///     When the notification was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     When the notification was scheduled to fire.
        /// </summary>
        public DateTime ScheduledFor { get; set; }

        /// <summary>
        ///     Then was the notification last recieved.
        /// </summary>
        public DateTime? LastRecievedOn { get; set; }

        /// <summary>
        ///     The details in the notification when it was orginailly scheduled.
        /// </summary>
        public NotificationDetailsModel ScheduledDetails { get; set; }

        /// <summary>
        ///     The details of the notification when it was recived.  Only stores the
        ///     values from the latest notification with this ID that was recieved.
        /// </summary>
        public NotificationDetailsModel RecievedDetails { get; set; }
    }
}