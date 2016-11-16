using System;
using System.Collections.Generic;
using SaturdayMP.XPlugins.Notifications;
using Xamarin.Forms;

namespace ExampleClient
{
    /// <summary>
    ///     A bunch of buttons to test the sending of notifications.
    /// </summary>
    public partial class MainPage
    {
        /// <summary>
        ///     The scheduler used to send notifications.
        /// </summary>
        private readonly INotificationScheduler _notificationScheduler;

        /// <summary>
        ///     Create the main page and iniailize the notification scheduler.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            // So we can send notifications.
            _notificationScheduler = DependencyService.Get<INotificationScheduler>();

            // To handle incoming notifications.
            NotificationScheduler.RegisterListener(new NotificationListener());
        }

        /// <summary>
        ///     Shedule a notification for now.  Like right now!
        /// </summary>
        /// <param name="sender">See <see cref="Button.Clicked" /> for more details.</param>
        /// <param name="e">See <see cref="Button.Clicked" /> for more details.</param>
        private void ScheduleNowButton_OnClicked(object sender, EventArgs e)
        {
            ScheduleNowNotification();
        }

        /// <summary>
        ///     Schedule a notification for now.   Not tomororw but now!
        /// </summary>
        /// <remarks>
        ///     Will check the extra info switch and if it's on will include the extra
        ///     info in the notifications.  Otherwise the extra info is not included.
        /// </remarks>
        private void ScheduleNowNotification()
        {
            if (ExtraInfoSwitch.IsToggled)
            {
                var extraInfo = new Dictionary<string, object> {{"Number", int.Parse(ExtraInfoNumberEntry.Text)}, {"Text", ExtraInfoTextEntry.Text}};

                _notificationScheduler.Create("Schedule Now", $"Created: {DateTime.Now:G}, Scheduled: {DateTime.Now:G}", extraInfo);
            }
            else
            {
                _notificationScheduler.Create("Schedule Now", $"Created: {DateTime.Now:G}, Scheduled: {DateTime.Now:G}");
            }
        }
    }
}