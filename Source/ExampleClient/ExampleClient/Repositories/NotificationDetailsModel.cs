namespace ExampleClient.Repositories
{

    /// <summary>
    ///     Details about a notification not related
    ///     to when it was scheduled or recieved.
    /// </summary>
    internal class NotificationDetailsModel
    {
        /// <summary>
        ///     The title of the notification.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     The message in the notification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The first extra info included in this notification, can be
        ///     blank if no extra info was included.
        /// </summary>
        public string ExtraInfoOne { get; set; }

        /// <summary>
        ///     The second extra info included in this notification, can be
        ///     blank if no extra info was included.
        /// </summary>
        public string ExtraInfoTwo { get; set; }
        

    }
}
