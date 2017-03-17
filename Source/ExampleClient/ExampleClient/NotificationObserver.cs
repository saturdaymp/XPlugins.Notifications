using ExampleClient.Repositories;
using ExampleClient.ViewModels;
using ExampleClient.Views;
using SaturdayMP.XPlugins.Notifications;
using Xamarin.Forms;

namespace ExampleClient
{
    /// <summary>
    ///     Handle notifications.
    /// </summary>
    public class NotificationObserver : INotificationObserver
    {
        /// <summary>
        ///     When a notification is recieved update the repository and show
        ///     the recieved notification to the user.
        /// </summary>
        /// <param name="notification"></param>
        public void NotificationReceived(Notification notification)
        {
            // Copy the notification values to the view model
            var viewModel = new NotificationRecievedViewModel
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message
            };

            // Only copy the extra info if it exists.
            if (notification.ExtraInfo.ContainsKey("ExtraInfoOne"))
                viewModel.ExtraInfoOne = notification.ExtraInfo["ExtraInfoOne"];

            if (notification.ExtraInfo.ContainsKey("ExtraInfoTwo"))
                viewModel.ExtraInfoTwo = notification.ExtraInfo["ExtraInfoTwo"];


            // Save the recieved view model.
            ScheduledNotificationRepository.NotificationRecieved(notification.Id, viewModel.Title, viewModel.Message, viewModel.ExtraInfoOne, viewModel.ExtraInfoTwo);


            // Show the notifcation page.
            var notificationPage = new NotificationRecievedPage(viewModel);
            Application.Current.MainPage.Navigation.PushAsync(notificationPage);
        }
    }
}