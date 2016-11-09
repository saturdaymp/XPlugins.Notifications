using Android.App;
using Android.Content;
using Android.Support.V4.App;
using TaskStackBuilder = Android.App.TaskStackBuilder;

namespace SaturdayMP.XPlugins.Notifications.Droid
{
    /// <summary>
    ///     Used to handled alarms that should show a notification.
    /// </summary>
    /// <remarks>
    ///     Assumes the alarm was trigged because you want to show a notification.  If
    ///     it was trigged by something else then who knows what till happen.  Assumes
    ///     the intent is set with "message" and "title" information that can be accessed by
    ///     the <see cref="Android.Content.Intent.GetStringExtra" /> GetStringExtra method.
    /// </remarks>
    [BroadcastReceiver]
    internal class NotificationAlarmHandler : BroadcastReceiver
    {
        /// <inheritdoc />
        public override void OnReceive(Context context, Intent intent)
        {
            // Pull out the parameters from the alarm.
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            // Create the notification.
            var builder = new NotificationCompat.Builder(Application.Context)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.logo_square_22x22)
                .SetAutoCancel(true);


            // Set this application to open when the notification is clicked.
            var resultIntent = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            var stackBuilder = TaskStackBuilder.Create(Application.Context);
            stackBuilder.AddNextIntent(resultIntent);

            var resultPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);
            builder.SetContentIntent(resultPendingIntent);


            // Show the notification.
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(0, builder.Build());
        }
    }
}