using System;

namespace ExampleClient
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
        ///     When the notification was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     When the notification was scheduled to fire.
        /// </summary>
        public DateTime ScheduledFor { get; set; }

        /// <summary>
        ///     The title to show on the list view.
        /// </summary>
        public string Title => $"Created: {CreatedOn:G}\nScheduled For: {ScheduledFor:G}";
    }
}