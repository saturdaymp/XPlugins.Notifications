using System;

namespace SaturdayMP.XPlugins.Notifications
{
    // TODO: Comments
    public interface INotificationScheduler
    {
        int Create(string title, string message);
    }
}
