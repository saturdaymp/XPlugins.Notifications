using ExampleClient.ViewModels;
using JetBrains.Annotations;

namespace ExampleClient.Views
{
    /// <summary>
    ///     Shows a recived notification.
    /// </summary>
    public partial class NotificationRecievedPage
    {
        /// <summary>
        ///     Create a new notification page and bind it to the view model.
        /// </summary>
        /// <param name="viewModel">The notification values.</param>
        public NotificationRecievedPage([NotNull] NotificationRecievedViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}