using System;
using Android.App;
using Android.Content;
using NUnit.Framework;
using SaturdayMP.XPlugins.Notifications.Droid;

namespace SaturdayMP.XPlugins.Notifications.Tests.Droid.Tests.NotificationListenerTests
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

            _listnerToTest = new Notifications.Droid.NotificationListener();
        }

        /// <summary>
        ///     The observer used to see if the test was successful.
        /// </summary>
        private MockObserver _resultObserver;

        /// <summary>
        ///     The listener to test.
        /// </summary>
        private Notifications.Droid.NotificationListener _listnerToTest;

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
        ///     Test that a intent with an invalid action won't trigger
        ///     a notification recieved.
        /// </summary>
        [Test]
        public void InvalidIntentAction()
        {
            // Run the test.
            _listnerToTest.NotificationRecieved(new Intent());


            // The observer should not have been called.
            Assert.That(!_resultObserver.WasNotificationRecieved);
        }

        /// <summary>
        ///     If the intent is valid then the notifictaion
        ///     information should be extracted from the intent
        ///     the observer notified.
        /// </summary>
        [Test]
        public void ObserverNotifiedOnValidNotificationIntent()
        {
            // Create a valid intent with some extra data.
            const string extraOneKey = "extraone";
            const string extraOneValue = "extra one value";
            const string extraTwoKey = "extratwo";
            const string extraTwoValue = "extra two value";

            var notificationId = new Guid().ToString();

            var validIntent = new Intent();
            validIntent.SetAction(Application.Context.PackageName + "." + "NOTIFICATION-" + notificationId);
            validIntent.PutExtra(NotificationScheduler.TitleExtrasKey, "Test title");
            validIntent.PutExtra(NotificationScheduler.MessageExtrasKey, "Test message.");
            validIntent.PutExtra(extraOneKey, extraOneValue);
            validIntent.PutExtra(extraTwoKey, extraTwoValue);


            // Run the test.
            _listnerToTest.NotificationRecieved(validIntent);


            // The observer should have been notitificed and called 
            Assert.That(_resultObserver.WasNotificationRecieved);

            var resultNotification = _resultObserver.LastNotificationRecieved;
            Assert.That(resultNotification.Id, Is.EqualTo(notificationId));
            Assert.That(resultNotification.Title, Is.EqualTo(validIntent.GetStringExtra(NotificationScheduler.TitleExtrasKey)));
            Assert.That(resultNotification.Message, Is.EqualTo(validIntent.GetStringExtra(NotificationScheduler.MessageExtrasKey)));

            // Extras.
            var extraInfo = resultNotification.ExtraInfo;
            Assert.That(extraInfo.Count, Is.EqualTo(2));

            Assert.That(extraInfo.ContainsKey(extraOneKey));
            Assert.That(extraInfo[extraOneKey], Is.EqualTo(extraOneValue));

            Assert.That(extraInfo.ContainsKey(extraTwoKey));
            Assert.That(extraInfo[extraTwoKey], Is.EqualTo(extraTwoValue));
        }
    }
}