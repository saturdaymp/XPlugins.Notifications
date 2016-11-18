using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SaturdayMP.XPlugins.Notifications
{
    /// <summary>
    ///     Interface for scheduling a notification.
    /// </summary>
    public interface INotificationScheduler
    {
        /// <summary>
        ///     Creates a new notification that is shown now.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <returns>A unique ID for the notification scheduled.</returns>
        int Create([NotNull] string title, [NotNull] string message);

        /// <summary>
        ///     Creates a new notification with extra information that is shown right away.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <param name="extraInfo">Any extra information you want to include in the notification.</param>
        /// <returns>A unique ID for the notification scheduled.</returns>
        int Create([NotNull] string title, [NotNull] string message, [NotNull] Dictionary<string, object> extraInfo);

        /// <summary>
        ///     Creates a new notification that is at the scheduled time.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <param name="scheduleDate">When to show the notification.</param>
        /// <returns>A unique ID for the notification scheduled.</returns>
        int Create([NotNull] string title, [NotNull] string message, DateTime scheduleDate);
    }
}