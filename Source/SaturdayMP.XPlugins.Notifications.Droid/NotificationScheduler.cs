using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;

namespace SaturdayMP.XPlugins.Notifications.Droid
{
    /// <summary>
    ///     Used to schedule local notificaions in Android.
    /// </summary>
    public class NotificationScheduler : INotificationScheduler
    {
        /// <summary>
        ///     The key used to hold the notificiation ID in the alarms
        ///     extra info dictonary.  Is hopefully unique enough that
        ///     the caller won't use the same key.
        /// </summary>
        private const string NotificationIdKey = "notificationidkey";

        /// <inheritdoc />
        public Guid Create(string title, string message)
        {
            return Create(title, message, DateTime.Now);
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, Dictionary<string, string> extraInfo)
        {
            return Create(title, message, DateTime.Now, extraInfo);
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, DateTime scheduleDate)
        {
            return Create(title, message, scheduleDate, new Dictionary<string, string>());
        }

        /// <inheritdoc />
        public Guid Create(string title, string message, DateTime scheduleDate, Dictionary<string, string> extraInfo)
        {
            // Create the unique identifier for this notifications.
            var notificationId = Guid.NewGuid();


            // Create the intent to be called when the alarm triggers.  Make sure
            // to add the id so we can find it later if the user wants to update or
            // cancel.
            var alarmIntent = new Intent(Application.Context, typeof(NotificationAlarmHandler));
            alarmIntent.PutExtra(NotificationIdKey, notificationId.ToString("N"));
            alarmIntent.PutExtra("title", title);
            alarmIntent.PutExtra("message", message);

            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);


            // Figure out the alaram in milliseconds.
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(scheduleDate);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            var notifyTimeInInMilliseconds = utcTime.AddSeconds(-epochDif).Ticks/10000;


            // Set the notification.
            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.Set(AlarmType.RtcWakeup, notifyTimeInInMilliseconds, pendingIntent);


            // All done.
            return notificationId;
        }
    }
}