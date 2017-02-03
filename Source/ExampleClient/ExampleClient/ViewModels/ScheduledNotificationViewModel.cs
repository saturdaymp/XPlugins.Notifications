using System;
using ExampleClient.Repositories;
using JetBrains.Annotations;
using SaturdayMP.XPlugins.Notifications;
using Xamarin.Forms;

namespace ExampleClient.ViewModels
{
    /// <summary>
    ///     Show the scheudled notification and it it's been recived yet.
    ///     Only shows the values from the last time it's been received.
    /// </summary>
    internal class ScheduledNotificationViewModel : PageViewModelBase
    {
        /// <summary>
        ///     Used the schedule and cancel notifications.
        /// </summary>
        private readonly INotificationScheduler _notificationScheduler;

        /// <summary>
        ///     Creates a new view model with default values set.
        /// </summary>
        public ScheduledNotificationViewModel()
        {
            // So we can send notifications.
            _notificationScheduler = DependencyService.Get<INotificationScheduler>();

            // Handle notification cancelling.
            CancelNotificationCommand = new Command(CancelNotification);

            // Set some default values.
            CreatedOn = "";
            ScheduledFor = "";
            RecievedOn = "";
            ScheduledTitle = "";
            RecievedTitle = "";
            ScheduledMessage = "";
            RecivedMessage = "";
            ScheduledExtraInfoOne = "";
            RecivedExtraInfoOne = "";
            ScheduledExtraInfoTwo = "";
            RecivedExtraInfoTwo = "";
            _canceledOn = "";
            CanceledOn = "";
        }

        /// <summary>
        ///     The ID of the scheduled notification.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The number of times we have recieved this notification.
        /// </summary>
        [UsedImplicitly]
        public int NumberOfTimesRecieved { get; set; }

        /// <summary>
        ///     When the notification was created.
        /// </summary>
        [NotNull]
        public string CreatedOn { get; set; }

        /// <summary>
        ///     When the notification was scheduled for.
        /// </summary>
        [NotNull]
        public string ScheduledFor { get; set; }

        /// <summary>
        ///     When the notification was recieved.
        /// </summary>
        [NotNull]
        public string RecievedOn { get; set; }

        /// <summary>
        ///     When title that was scheduled.
        /// </summary>
        [NotNull]
        public string ScheduledTitle { get; set; }

        /// <summary>
        ///     The title of the notifcation recieved.
        /// </summary>
        [NotNull]
        public string RecievedTitle { get; set; }

        /// <summary>
        ///     The message that was scheduled.
        /// </summary>
        [NotNull]
        public string ScheduledMessage { get; set; }

        /// <summary>
        ///     The actual message recieved.
        /// </summary>
        [NotNull]
        public string RecivedMessage { get; set; }

        /// <summary>
        ///     The 1st extra info value that was scheduled.  Can
        ///     be bank if no extra info was scheduled.
        /// </summary>
        [NotNull]
        public string ScheduledExtraInfoOne { get; set; }

        /// <summary>
        ///     The 1st extra info recieved.  Can be blank
        ///     if no extra info was scheduled.
        /// </summary>
        [NotNull]
        public string RecivedExtraInfoOne { get; set; }

        /// <summary>
        ///     The 2nd extra info value that was scheduled.  Can
        ///     be bank if no extra info was scheduled.
        /// </summary>
        [NotNull]
        public string ScheduledExtraInfoTwo { get; set; }

        /// <summary>
        ///     The 2nd extra info recieved.  Can be blank
        ///     if no extra info was scheduled.
        /// </summary>
        [NotNull]
        public string RecivedExtraInfoTwo { get; set; }


        /// <summary>
        ///     Command to cancel an existing notification.
        /// </summary>
        public Command CancelNotificationCommand { get; set; }

        /// <summary>
        ///     Cancels an existing notification.
        /// </summary>
        private void CancelNotification()
        {
            try
            {
                _notificationScheduler.Cancel(Id);

                var canceledTime = DateTime.Now;
                ScheduledNotificationRepository.NotificationCancled(Id, canceledTime);
                CanceledOn = canceledTime.ToString("G");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error Trying to Cancel Notification", $"{ex.Message}", "OK");
            }
        }

        /// <summary>
        ///     When the notification was canceled.
        /// </summary>
        [NotNull]
        private string _canceledOn;

        /// <summary>
        ///     When the notification was canceled.
        /// </summary>
        [NotNull]
        public string CanceledOn
        {
            get { return _canceledOn; }
            set
            {
                if (_canceledOn == value) return;

                _canceledOn = value;
                OnPropertyChanged();
            }
        }

    }
}
