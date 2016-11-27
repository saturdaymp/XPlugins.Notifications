namespace ExampleClient.ViewModels
{
    /// <summary>
    ///     Show the scheudled notification and it it's been recived yet.
    ///     Only shows the values from the last time it's been received.
    /// </summary>
    internal class ScheduledNotificationViewModel
    {
        /// <summary>
        ///     The ID of the scheduled notification.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The number of times we have recieved this notification.
        /// </summary>
        public int NumberOfTimesRecieved { get; set; }

        /// <summary>
        ///     When the notification was created.
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        ///     When the notification was scheduled for.
        /// </summary>
        public string ScheduledFor { get; set; }

        /// <summary>
        ///     When the notification was recieved.
        /// </summary>
        public string RecievedOn { get; set; }

        /// <summary>
        ///     When title that was scheduled.
        /// </summary>
        public string ScheduledTitle { get; set; }

        /// <summary>
        ///     The title of the notifcation recieved.
        /// </summary>
        public string RecievedTitle { get; set; }

        /// <summary>
        ///     The message that was scheduled.
        /// </summary>
        public string ScheduledMessage { get; set; }

        /// <summary>
        ///     The actual message recieved.
        /// </summary>
        public string RecivedMessage { get; set; }

        /// <summary>
        ///     The 1st extra info value that was scheduled.  Can
        ///     be bank if no extra info was scheduled.
        /// </summary>
        public string ScheduledExtraInfoOne { get; set; }

        /// <summary>
        ///     The 1st extra info recieved.  Can be blank
        ///     if no extra info was scheduled.
        /// </summary>
        public string RecivedExtraInfoOne { get; set; }

        /// <summary>
        ///     The 2nd extra info value that was scheduled.  Can
        ///     be bank if no extra info was scheduled.
        /// </summary>
        public string ScheduledExtraInfoTwo { get; set; }

        /// <summary>
        ///     The 2nd extra info recieved.  Can be blank
        ///     if no extra info was scheduled.
        /// </summary>
        public string RecivedExtraInfoTwo { get; set; }
    }
}
