using System;
using Android.App;
using Android.Content;
using Android.OS;
using SaturdayMP.XPlugins.Notifications.Droid;

namespace SaturdayMP.XPlugins.Notifications
{
    public class NotificationScheduler : INotificationScheduler
    {
        /// <inheritdoc />
        public int Create(string title, string message)
        {
            // https://nnish.com/2014/12/16/scheduled-notifications-in-android-using-alarm-manager/
            // https://github.com/edsnider/Xamarin.Plugins/blob/master/Notifier/Plugin.LocalNotifications.Android/LocalNotificationsImplementation.cs
            // https://developer.android.com/reference/android/app/AlarmManager.html

            // Create the intent to be called when the alarm triggers.
            var alarmIntent = new Intent(Application.Context, typeof(NotificationAlarmHandler));
            alarmIntent.PutExtra("title", title);
            alarmIntent.PutExtra("message", message);

            // TODO: Comments
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            // Figure out the alaram in milliseconds.
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            var notifyTimeInInMilliseconds = utcTime.AddSeconds(-epochDif).Ticks/10000;

            // Set the notification.
            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.Set(AlarmType.RtcWakeup, notifyTimeInInMilliseconds, pendingIntent);

            return 0;

        }
    }
}