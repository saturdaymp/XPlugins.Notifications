using SaturdayMP.XPlugins.Notifications;
using Xamarin.Forms;

namespace ExampleClient
{
    public class NotificationListener : INotificationListener
    {
        /// <inheritdoc />
        public void Recieved(Notification notification)
        {
            var viewModel = new NotificationDisplayViewModel();
            viewModel.Title = notification.Title;
            viewModel.Message = notification.Message;

            if (notification.ExtraInfo.ContainsKey("Number"))
            {
                viewModel.ExtraInfoNumber = (int) notification.ExtraInfo["Number"];
            }

            if (notification.ExtraInfo.ContainsKey("Text"))
            {
                viewModel.ExtraInfoText = (string) notification.ExtraInfo["Text"];
            }



            var notificationPage = new NotificationDisplayPage(viewModel);
            Application.Current.MainPage.Navigation.PushAsync(notificationPage);
        }
    }
}
