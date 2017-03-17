using Android.App;
using Android.Content;
using Android.Support.V4.App;

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


            // Set this application to open when the notification is clicked.  If the application
            // is already open it will reuse the same activity.
            var resultIntent = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            resultIntent.SetAction(intent.Action);
            resultIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            resultIntent.PutExtras(intent.Extras);

            var resultPendingIntent = PendingIntent.GetActivity(Application.Context, 0, resultIntent, PendingIntentFlags.UpdateCurrent);
            builder.SetContentIntent(resultPendingIntent);

            // Show the notification.
            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(0, builder.Build());
        }
    }
}