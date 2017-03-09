using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using JetBrains.Annotations;

namespace SaturdayMP.XPlugins.Notifications.Droid
{
    /// <summary>
    ///     Used to schedule local notificaions in Android.
    /// </summary>
    /// <remarks>
    ///     The action of the Android Intent is set to a GUID
    ///     so that it is unqiue and can be found.
    ///     http://stackoverflow.com/questions/20204284/is-it-possible-to-create-multiple-pendingintents-with-the-same-requestcode-and-d
    /// </remarks>
    public class NotificationScheduler : INotificationScheduler
    {
        #region Cancel

        /// <inheritdoc />
        public void Cancel(string notificationId)
        {
            // Find the intent.  If we don't find one then
            // nothing to cancel.
            var foundPendingIntent = FindPendingInetnt(notificationId);
            if (foundPendingIntent == null)
                return;

            // Cancel the intent.  Make sure we cancel the intent in the alarm manager
            // then cancel the intent as well.
            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.Cancel(foundPendingIntent);

            foundPendingIntent.Cancel();
        }

        #endregion

        #region Key Constants

        // TODO
        private const string ActionKey = "NOTIFICATION";

        /// <summary>
        ///     Key used to store the title part of the notification
        ///     in the intent.
        /// </summary>
        private const string TitleKey = "title";

        /// <summary>
        ///     Key used to store the message part of the notification
        ///     in the intent.
        /// </summary>
        private const string MessageKey = "message";

        #endregion

        #region Create

        /// <inheritdoc />
        public string Create(string title, string message)
        {
            return Create(title, message, DateTime.Now);
        }

        /// <inheritdoc />
        public string Create(string title, string message, Dictionary<string, string> extraInfo)
        {
            return Create(title, message, DateTime.Now, extraInfo);
        }

        /// <inheritdoc />
        public string Create(string title, string message, DateTime scheduleDate)
        {
            return Create(title, message, scheduleDate, new Dictionary<string, string>());
        }

        
        // TODO
        private static string BuildActionName(string notificationId)
        {
            return Application.Context.PackageName + "." + ActionKey + "-" + notificationId;
        }

        /// <inheritdoc />
        public string Create(string title, string message, DateTime scheduleDate, Dictionary<string, string> extraInfo)
        {
            // Create the unique identifier for this notifications.
            var notificationId = Guid.NewGuid().ToString();


            // Create the alarm intent to be called when the alarm triggers.  Make sure
            // to add the id so we can find it later if the user wants to update or
            // cancel.
            var alarmIntent = new Intent(Application.Context, typeof(NotificationAlarmHandler));
            alarmIntent.SetAction(BuildActionName(notificationId));
            alarmIntent.PutExtra(TitleKey, title);
            alarmIntent.PutExtra(MessageKey, message);

            // Add the extra info.
            foreach (var ei in extraInfo)
                alarmIntent.PutExtra(ei.Key, ei.Value);


            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent,
                PendingIntentFlags.UpdateCurrent);


            // Figure out the alaram in milliseconds.
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(scheduleDate);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            var notifyTimeInInMilliseconds = utcTime.AddSeconds(-epochDif).Ticks / 10000;


            // Set the notification.
            var alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.Set(AlarmType.RtcWakeup, notifyTimeInInMilliseconds, pendingIntent);

            // All done.
            return notificationId;
        }

        #endregion

        #region Find

        /// <inheritdoc />
        public Notification Find(string notificationId)
        {
            // Find the inent, if it exists.  If it dosen't exist then
            // return null.
            var foundIntent = FindIntent(notificationId);
            if (foundIntent == null)
                return null;


            // Intent exists, translate it into a notification.
            var foundNotification = new Notification
            {
                Id = notificationId,
                Title = foundIntent.GetStringExtra(TitleKey),
                Message = foundIntent.GetStringExtra(MessageKey)
            };

            // Populate with extra info.
            foreach (var key in foundIntent.Extras.KeySet())
            {
                if (key != TitleKey && key != MessageKey)
                {
                    foundNotification.ExtraInfo.Add(key, foundIntent.Extras.GetString(key));
                }
                
            }
            


            // All done.
            return foundNotification;
        }

        /// <summary>
        ///     Finds the Android Intent based on the notification ID.
        /// </summary>
        /// <param name="notificationId">
        ///     The intent with this notification ID to find.  Assumes this ID
        ///     is stored in the <see cref="Intent.Action" /> property.
        /// </param>
        /// <returns>The found intent or null if the intent can't be found.</returns>
        [CanBeNull]
        private static Intent FindIntent(string notificationId)
        {
            // Find the pending intent.
            var foundPendingIntent = FindPendingInetnt(notificationId);

            // Translate the pending intent to a normal intent, if not null, and return it.  Use reflection to
            // get the intent from the pending intent.
            return foundPendingIntent?.Class.GetDeclaredMethod("getIntent").Invoke(foundPendingIntent) as Intent;
        }

        /// <summary>
        ///     Finds the pending intent based on the notification ID.
        /// </summary>
        /// <param name="notificationId">
        ///     The intent with this notification ID to find.  Assumes this ID
        ///     is stored in the <see cref="Intent.Action" /> property.
        /// </param>
        /// <returns></returns>
        [CanBeNull]
        private static PendingIntent FindPendingInetnt(string notificationId)
        {
            // Intent we want to find.  Should have the action set to the GUID to make it unquie.
            // Not that we don't have to set the extra info.  The things that make a intent unquie
            // are the action, data, type of data, package/component, and catagories.  In our case
            // we only set the action to the notification GUID.
            var intentToFind = new Intent(Application.Context, typeof(NotificationAlarmHandler));
            intentToFind.SetAction(BuildActionName(notificationId));

            // Try to find the intend.
            return PendingIntent.GetBroadcast(Application.Context, 0, intentToFind, PendingIntentFlags.NoCreate);
        }

        #endregion
    }
}