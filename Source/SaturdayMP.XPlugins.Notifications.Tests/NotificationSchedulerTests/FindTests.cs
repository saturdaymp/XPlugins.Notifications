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
        ///     The schedule to test.
        /// </summary>
        private INotificationScheduler _schedulerToTest;

        /// <summary>
        ///     Load the correct scheduler based on the environment we are in.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
#if __ANDROID__
            _schedulerToTest = new Notifications.Droid.NotificationScheduler();
#elif __IOS__
            _schedulerToTest = new Notifications.iOS.NotificationScheduler();
#else
            throw new Exception("Invalid envrionment.")
#endif
        }

        /// <summary>
        ///     Should return null if a notification is not found.
        /// </summary>
        [Test]
        public void NotificationDoesNotExist()
        {
            Assert.That(() => _schedulerToTest.Find("blah"), Is.Null);
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

            var expectedNotificationId = _schedulerToTest.Create(expectedNotificationTitle, expectedNotificationMessage,
                DateTime.Now.AddHours(1));

            // Try to find it.
            var resultNotification = _schedulerToTest.Find(expectedNotificationId);

            Assert.That(resultNotification, Is.Not.Null);
            Assert.That(resultNotification.Title, Is.EqualTo(expectedNotificationTitle));
            Assert.That(resultNotification.Message, Is.EqualTo(expectedNotificationMessage));
        }
    }
}