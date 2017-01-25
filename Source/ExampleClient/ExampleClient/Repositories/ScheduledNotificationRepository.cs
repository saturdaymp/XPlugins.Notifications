using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleClient.Repositories
{
    internal class ScheduledNotificationRepository
    {
        private static readonly List<ScheduledNotificationModel> ScheduledNotifications = new List<ScheduledNotificationModel>();

        /// <summary>
        ///     Save newly scheduled notification.
        /// </summary>
        /// <param name="id">The ID of the notification scheduled.</param>
        /// <param name="title">The title of the scheduled notification.</param>
        /// <param name="message">The message ot the scheduled notification.</param>
        /// <param name="scheduledFor">When the notification is scheduled for.</param>
        /// <param name="extraInfoOne">The 1st extra infor scheduled for this notification.</param>
        /// <param name="extraInfoTwo">The 2nd extra infor scheduled for this notification.</param>
        public static void NotificationScheduled(string id, string title, string message, DateTime scheduledFor, string extraInfoOne, string extraInfoTwo)
        {
            var newScheduledNotification = new ScheduledNotificationModel
            {
                CreatedOn = DateTime.Now,
                Id = id,
                NumberTimesRecieved = 0,
                ScheduledFor = scheduledFor,
                ScheduledDetails = new NotificationDetailsModel
                {
                    ExtraInfoOne = extraInfoOne,
                    ExtraInfoTwo = extraInfoTwo,
                    Title = title,
                    Message = message
                }
            };


            ScheduledNotifications.Add(newScheduledNotification);
        }

        /// <summary>
        ///     Save the details of a received notification.  If possible will match the
        ///     recieved values to an existing scheduled notification.
        /// </summary>
        /// <param name="id">The ID of the notification recieved.</param>
        /// <param name="title">The title of the notification received.</param>
        /// <param name="message">The mesage of the notification received.</param>
        /// <param name="extraInfoOne">The extra info of the recieved notification.</param>
        /// <param name="extraInfoTwo">The extra info of the recieved notification.</param>
        public static void NotificationRecieved(string id, string title, string message, string extraInfoOne, string extraInfoTwo)
        {
            // See if the notification was scheduled.  If we don't find one then create a new one.
            var existingScheduledNotification = ScheduledNotifications.SingleOrDefault(x => x.Id == id);
            if (existingScheduledNotification == null)
            {
                existingScheduledNotification = new ScheduledNotificationModel();
                ScheduledNotifications.Add(existingScheduledNotification);
            }

            // Update the recieved values.
            existingScheduledNotification.NumberTimesRecieved++;
            existingScheduledNotification.RecievedDetails = new NotificationDetailsModel
            {
                ExtraInfoOne = extraInfoOne,
                ExtraInfoTwo = extraInfoTwo,
                Message = message,
                Title = title
            };
        }

        /// <summary>
        ///     Finds the scheduled notification with the given ID.
        /// </summary>
        /// <param name="id">The ID of the notification to find.</param>
        /// <returns>The scheduled notification or null if the notification does not exist.</returns>
        public static ScheduledNotificationModel FindNotification(string id)
        {
            return ScheduledNotifications.SingleOrDefault(x => x.Id == id);
        }
    }
}