using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SaturdayMP.XPlugins.Notifications.Tests.NotificationSchedulerTests
{
    [TestFixture]
    internal class CreateTests
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

        [Test]
        public void CreateNotification()
        {

            // Expected values.
            var expectedTitle = "Test Notification";
            var expectedMessage = DateTime.Now.Ticks.ToString();
            var expectedScheduledDate = DateTime.Now.AddHours(1);
            var expectedExtraInfo = new Dictionary<string, string>()
            {
                {"First Info", "First Value"},
                {"Second Info", "Second Value"}
            };


            // Create the notification.
            var notificationId = _schedulerToTest.Create(expectedTitle, expectedMessage, expectedScheduledDate, expectedExtraInfo);


            // See if we can find it and make sure all the values are set correctly.
            var foundNotification = _schedulerToTest.Find(notificationId);

            Assert.That(foundNotification, Is.Not.Null);
            Assert.That(foundNotification.Title, Is.EqualTo(expectedTitle));
            Assert.That(foundNotification.Message, Is.EqualTo(expectedMessage));

            var foundExtraInfo = foundNotification.ExtraInfo;

            Assert.That(foundExtraInfo, Is.Not.Null);
            Assert.AreEqual(expectedExtraInfo.Count, foundExtraInfo.Count);

            foreach (var ei in expectedExtraInfo)
            {
                Assert.That(foundExtraInfo.ContainsKey(ei.Key));
                Assert.That(foundExtraInfo[ei.Key], Is.EqualTo(ei.Value));
            }
        }
    }
}