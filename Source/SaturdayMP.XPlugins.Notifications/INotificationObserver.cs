namespace SaturdayMP.XPlugins.Notifications
{
    public interface INotificationObserver
    {
        void NotificationReceived(Notification notification);
    }
}
