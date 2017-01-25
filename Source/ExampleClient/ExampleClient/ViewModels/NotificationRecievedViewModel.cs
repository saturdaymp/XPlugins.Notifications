using System;

namespace ExampleClient.ViewModels
{
    /// <summary>
    ///     View model for the Notification Display page.  Has
    ///     values parsed from the notification.
    /// </summary>
    public class NotificationRecievedViewModel
    {
        /// <summary>
        ///     The id of the recieved notification.
        /// </summary>
        public string Id { get; set; }

        public string IdAsString { get; set; }

        /// <summary>
        ///     Title of the notification.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     The message in the notification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The numberic extra info in the notification.  If not
        ///     supplied then will be null.
        /// </summary>
        public string ExtraInfoOne { get; set; }

        /// <summary>
        ///     The text extra info in the notification.  Can
        ///     be blank not supplied in the notification.
        /// </summary>
        public string ExtraInfoTwo { get; set; }
    }
}