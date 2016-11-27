using System;
using ExampleClient.Repositories;
using ExampleClient.ViewModels;

namespace ExampleClient.Views
{
    public partial class ScheduledNotificationPage
    {
        public ScheduledNotificationPage()
        {
            InitializeComponent();
        }

        public ScheduledNotificationPage(Guid scheduledNotificationId)
        {
            InitializeComponent();

            var repo = new ScheduledNotificationRepository();
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