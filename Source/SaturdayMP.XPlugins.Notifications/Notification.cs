using System.Collections.Generic;
using JetBrains.Annotations;

namespace SaturdayMP.XPlugins.Notifications
{
    /// <summary>
    ///     A notification that was recived.
    /// </summary>
    public class Notification
    {
        /// <summary>
        ///     Any extra information attached to the notification.
        /// </summary>
        [NotNull] public Dictionary<string, object> ExtraInfo;

        /// <summary>
        ///     Create an new empty notification and initialize
        ///     the extra info.
        /// </summary>
        public Notification()
        {
            ExtraInfo = new Dictionary<string, object>();
        }

        /// <summary>
        ///     The unique ID of the notification.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The message of the notification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The title of the notification.
        /// </summary>
        public string Title { get; set; }
    }
}