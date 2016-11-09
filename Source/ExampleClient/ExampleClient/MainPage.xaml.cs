using SaturdayMP.XPlugins.Notifications;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace ExampleClient
{
    public partial class MainPage
    {
        private readonly INotificationScheduler _notificationScheduler;

        public MainPage()
        {
            InitializeComponent();

            _notificationScheduler = DependencyService.Get<INotificationScheduler>();
        }

        private void ScheduleNowButton_OnClicked(object sender, EventArgs e)
        {
            var id = _notificationScheduler.Create("Schedule Now", $"Created: {DateTime.Now:G}, Scheduled: {DateTime.Now:G}");
            Debug.WriteLine(id);
        }
    }
}
