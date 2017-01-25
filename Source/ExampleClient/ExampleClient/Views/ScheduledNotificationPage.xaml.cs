using System;
using ExampleClient.Repositories;
using ExampleClient.ViewModels;

namespace ExampleClient.Views
{
    /// <summary>
    ///     Shows a scheduled notification and if it's been recieved or or not.
    /// </summary>
    public partial class ScheduledNotificationPage
    {
        /// <summary>
        ///     Creates a new page without a scheduled notification.
        /// </summary>
        public ScheduledNotificationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Creates a new page with the scheduled notification loaded.
        /// </summary>
        /// <param name="scheduledNotificationId"></param>
        public ScheduledNotificationPage(string scheduledNotificationId) : this()
        {
            var scheduledNotification = ScheduledNotificationRepository.FindNotification(scheduledNotificationId);

            BindingContext = new ScheduledNotificationViewModel
            {
                Id = scheduledNotification.Id.ToString(),
                NumberOfTimesRecieved = scheduledNotification.NumberTimesRecieved,
                CreatedOn = scheduledNotification.CreatedOn.ToString("G"),
                ScheduledFor = scheduledNotification.ScheduledFor.ToString("G"),
                RecievedOn = scheduledNotification.LastRecievedOn?.ToString("G") ?? "",
                ScheduledTitle = scheduledNotification.ScheduledDetails?.Title,
                RecievedTitle = scheduledNotification.RecievedDetails?.Title,
                ScheduledMessage = scheduledNotification.ScheduledDetails?.Message,
                RecivedMessage = scheduledNotification.RecievedDetails?.Message,
                ScheduledExtraInfoOne = scheduledNotification.ScheduledDetails?.ExtraInfoOne,
                RecivedExtraInfoOne = scheduledNotification.RecievedDetails?.ExtraInfoOne,
                ScheduledExtraInfoTwo = scheduledNotification.ScheduledDetails?.ExtraInfoTwo,
                RecivedExtraInfoTwo = scheduledNotification.RecievedDetails?.ExtraInfoTwo
            };
        }
    }
}