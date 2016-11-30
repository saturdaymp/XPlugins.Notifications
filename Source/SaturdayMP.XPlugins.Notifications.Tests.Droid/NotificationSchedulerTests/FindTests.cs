using System;
using NUnit.Framework;

namespace SaturdayMP.XPlugins.Notifications.Tests.Droid.NotificationSchedulerTests
{
    /// <summary>
    ///     Tests to make sure notifications can be found.
    /// </summary>
    [TestFixture]
    internal class FindTests
    {
        /// <summary>
        ///     Should return null if a notification is not found.
        /// </summary>
        [Test]
        public void NotificationDoesNotExist()
        {
            var scheduler = new Notifications.Droid.NotificationScheduler();
            Assert.That(() => scheduler.Find(new Guid()), Is.Null);
        }

        /// <summary>
        ///     Should find a created notification.
        /// </summary>
        [Test]
        public void NotificationExists()
        {
            // Create the notifiaction to find.
            const string expectedNotificationTitle = "Test Notification";
            const string expectedNotificationMessage = "This is a test notification.";

            var scheduler = new Notifications.Droid.NotificationScheduler();
            var expectedNotificationId = scheduler.Create(expectedNotificationTitle, expectedNotificationMessage, DateTime.Now.AddHours(1));

            // Try to find it.
            var resultNotification = scheduler.Find(expectedNotificationId);

            Assert.That(resultNotification, Is.Not.Null);
            Assert.That(resultNotification.Title, Is.EqualTo(expectedNotificationTitle));
            Assert.That(resultNotification.Message, Is.EqualTo(expectedNotificationMessage));
        }
    }
}