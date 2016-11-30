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
        #region Exists

        /// <summary>
        ///     Finds a specific notification.
        /// </summary>
        /// <param name="notificationId">The notification to find.</param>
        Notification Find(Guid notificationId);

        #endregion

        #region Cancel

        /// <summary>
        ///     Cancels an existing notification.
        /// </summary>
        /// <param name="notificationId">The notification to cancel.</param>
        /// <remarks>
        ///     If the notification exists it will be canceled.  If the notification does
        ///     not exist then it has been canceled so no need to raise an error.
        /// </remarks>
        void Cancel(Guid notificationId);

        #endregion

        #region Create

        /// <summary>
        ///     Creates a new notification that is shown now.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <returns>A unique ID for the notification scheduled.</returns>
        Guid Create([NotNull] string title, [NotNull] string message);

        /// <summary>
        ///     Creates a new notification with extra information that is shown right away.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <param name="extraInfo">Any extra information you want to include in the notification.</param>
        /// <returns>A unique ID for the notification scheduled.</returns>
        Guid Create([NotNull] string title, [NotNull] string message, [NotNull] Dictionary<string, string> extraInfo);

        /// <summary>
        ///     Creates a new notification that is at the scheduled time.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <param name="scheduleDate">When to show the notification.</param>
        /// <returns>A unique ID for the notification scheduled.</returns>
        Guid Create([NotNull] string title, [NotNull] string message, DateTime scheduleDate);

        /// <summary>
        ///     Creates a new notification to appear at the scheduled time.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The content of the notification.</param>
        /// <param name="scheduleDate">When to show the notification.</param>
        /// <param name="extraInfo">Any extra information you want to include in the notification.</param>
        /// <returns>A unique ID for the notification scheduled.</returns>
        Guid Create([NotNull] string title, [NotNull] string message, DateTime scheduleDate, [NotNull] Dictionary<string, string> extraInfo);

        #endregion
    }
}