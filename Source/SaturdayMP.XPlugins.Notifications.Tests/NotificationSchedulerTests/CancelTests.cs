using System;
using NUnit.Framework;

namespace SaturdayMP.XPlugins.Notifications.Tests.Droid.NotificationSchedulerTests
{
    [TestFixture]
    internal class CancelTests
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
        ///     Make sure a notification that exists is canceled.
        /// </summary>
        [Test]
        public void NotificationCanceled()
        {
            // Schedule a notifiaction.
            var notificationId = _schedulerToTest.Create("Test Notification", "Test notification message.", DateTime.Now.AddHours(1));

            // Cancel it.
            _schedulerToTest.Cancel(notificationId);

            // Try to find it.  Shouldn't find it because it has been cancelled.
            Assert.That(() => _schedulerToTest.Find(notificationId), Is.Null);
        }

        /// <summary>
        ///     Shouldn't throw any errors.  A notification that does
        ///     not exist is already canceled.
        /// </summary>
        [Test]
        public void NotificationDoesNotExist()
        {
            _schedulerToTest.Cancel("blah");
        }
    }
}