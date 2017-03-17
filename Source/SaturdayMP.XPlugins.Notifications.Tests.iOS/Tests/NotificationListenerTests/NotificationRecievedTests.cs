using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using NUnit.Framework;
using SaturdayMP.XPlugins.Notifications.iOS;
using UIKit;

namespace SaturdayMP.XPlugins.Notifications.Tests.iOS.Tests.NotificationListenerTests
{
    /// <summary>
    ///     Test that the listener for Andorid correctly hears
    ///     notifications and parses them correctly.
    /// </summary>
    [TestFixture]
    internal class NotificationRecievedTests
    {
        /// <summary>
        ///     Setup the listener and observer for testing.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Setup the observer.
            _resultObserver = new MockObserver();
            NotificationListener.Subscribe(_resultObserver);

            _listnerToTest = new Notifications.iOS.NotificationListener();
        }

        /// <summary>
        ///     The observer used to see if the test was successful.
        /// </summary>
        private MockObserver _resultObserver;

        /// <summary>
        ///     The listener to test.
        /// </summary>
        private Notifications.iOS.NotificationListener _listnerToTest;

        /// <summary>
        ///     Used to test the listener.
        /// </summary>
        private class MockObserver : INotificationObserver
        {
            /// <summary>
            ///     The last notification this observer recieved.
            /// </summary>
            public Notification LastNotificationRecieved { get; private set; }

            /// <summary>
            ///     True if at least one notification was recieved, false otherwise.
            /// </summary>
            public bool WasNotificationRecieved => LastNotificationRecieved != null;

            /// <summary>
            ///     Overriden so we can track what notifications where recieved
            ///     for testing.
            /// </summary>
            /// <param name="notification">The notification recieved.</param>
            public void NotificationReceived(Notification notification)
            {
                LastNotificationRecieved = notification;
            }
        }


        /// <summary>
        ///     The observer should be called when a notification is recieved.
        /// </summary>
        [Test]
        public void ValidUiNOtifiactionObserverNotified()
        {
            // Expected values.
            var expectedNotificationId = new Guid().ToString();

            const string expectedTitle = "Title";
            const string exptedMessage = "Message";

            var expectedExtraInfo = new Dictionary<string, string> { {"KeyOne", "Extra value one."}, {"KeyTwo", "Extra value tow."}};


            // Run the test.
            var validUiNotification = BuildUiNotification(expectedNotificationId, expectedTitle, exptedMessage, expectedExtraInfo);
            _listnerToTest.NotificationReceived(validUiNotification);


            // Make sure the observer was notified.
            Assert.That(_resultObserver.WasNotificationRecieved);

            var resultNotification = _resultObserver.LastNotificationRecieved;
            Assert.That(resultNotification.Id, Is.EqualTo(expectedNotificationId));
            Assert.That(resultNotification.Title, Is.EqualTo(expectedTitle));
            Assert.That(resultNotification.Message, Is.EqualTo(exptedMessage));

            foreach (var ei in expectedExtraInfo)
            {
                Assert.That(resultNotification.ExtraInfo.ContainsKey(ei.Key));
                Assert.That(resultNotification.ExtraInfo[ei.Key], Is.EqualTo(ei.Value));
            }
        }

        private UILocalNotification BuildUiNotification(string notificationId, string title, string message, Dictionary<string, string> extraInfo)
        {

            if (!NSThread.IsMain)
            {
                UILocalNotification createdNotification = null;
                UIApplication.SharedApplication.InvokeOnMainThread(() => createdNotification = BuildUiNotification(notificationId, title, message, extraInfo));

                return createdNotification;
            }

            // Create a valid UI Notification.
            var validUiNotification = new UILocalNotification
            {
                AlertTitle = title,
                AlertBody = message
            };

            // The notifiaction ID exists in the user info.
            var userInfoDictionary = new Dictionary<string, string>(extraInfo) { { NotificationScheduler.NotificationIdKey, notificationId } };
            validUiNotification.UserInfo = NSDictionary.FromObjectsAndKeys(userInfoDictionary.Values.ToArray<object>(), userInfoDictionary.Keys.ToArray<object>());


            // All done.
            return validUiNotification;
        }
    }
}