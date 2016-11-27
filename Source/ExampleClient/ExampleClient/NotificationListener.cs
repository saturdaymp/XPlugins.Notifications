using ExampleClient.Repositories;
using ExampleClient.ViewModels;
using SaturdayMP.XPlugins.Notifications;
using Xamarin.Forms;

namespace ExampleClient
{
    /// <summary>
    /// Handle notifications.
    /// </summary>
    public class NotificationListener : INotificationListener
    {
        /// <inheritdoc />
        /// <remarks>
        /// Parse out the 
        /// </remarks>
        public void Recieved(Notification notification)
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
            {
                viewModel.ExtraInfoOne = (string) notification.ExtraInfo["ExtraInfoOne"];
            }

            if (notification.ExtraInfo.ContainsKey("ExtraInfoTwo"))
            {
                viewModel.ExtraInfoTwo = (string) notification.ExtraInfo["ExtraInfoTwo"];
            }


            // Save the recieved view model.
            var repo = new ScheduledNotificationRepository();
            ScheduledNotificationRepository.NotificationRecieved(notification.Id, viewModel.Title, viewModel.Message, viewModel.ExtraInfoOne, viewModel.ExtraInfoTwo);


            // Show the notifcation page.
            var notificationPage = new Views.NotificationRecievedPage(viewModel);
            Application.Current.MainPage.Navigation.PushAsync(notificationPage);
        }
    }
}
