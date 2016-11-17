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
            var viewModel = new NotificationDisplayViewModel
            {
                Title = notification.Title,
                Message = notification.Message
            };

            // Only copy the extra info if it exists.
            if (notification.ExtraInfo.ContainsKey("Number"))
            {
                viewModel.ExtraInfoNumber = (int) notification.ExtraInfo["Number"];
            }

            if (notification.ExtraInfo.ContainsKey("Text"))
            {
                viewModel.ExtraInfoText = (string) notification.ExtraInfo["Text"];
            }

            // Show the notifcation page.
            var notificationPage = new NotificationDisplayPage(viewModel);
            Application.Current.MainPage.Navigation.PushAsync(notificationPage);
        }
    }
}
