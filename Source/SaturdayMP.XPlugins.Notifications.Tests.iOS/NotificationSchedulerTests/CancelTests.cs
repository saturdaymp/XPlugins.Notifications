using System;
using NUnit.Framework;

namespace SaturdayMP.XPlugins.Notifications.Tests.iOS.NotificationSchedulerTests
{
    [TestFixture]
    internal class CancelTests
    {
        /// <summary>
        ///     Make sure a notification that exists is canceled.
        /// </summary>
        [Test]
        public void NotificationCanceled()
        {
            // Schedule a notifiaction.
            var scheduler = new Notifications.iOS.NotificationScheduler();
            var notificationId = scheduler.Create("Test Notification", "Test notification message.", DateTime.Now.AddHours(1));

            // Cancel it.
            scheduler.Cancel(notificationId);

            // Try to find it.  Shouldn't find it because it has been cancelled.
            Assert.That(() => scheduler.Find(notificationId), Is.Null);
        }

        /// <summary>
        ///     Shouldn't throw any errors.  A notification that does
        ///     not exist is already canceled.
        /// </summary>
        [Test]
        public void NotificationDoesNotExist()
        {
            var scheduler = new Notifications.iOS.NotificationScheduler();
            scheduler.Cancel("Blah");
        }
    }
}