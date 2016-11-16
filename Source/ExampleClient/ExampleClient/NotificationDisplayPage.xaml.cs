using JetBrains.Annotations;

namespace ExampleClient
{
    /// <summary>
    ///     Shows a recived notification.
    /// </summary>
    public partial class NotificationDisplayPage
    {
        /// <summary>
        ///     Create a new notification page and bind it to the view model.
        /// </summary>
        /// <param name="viewModel">The notification values.</param>
        public NotificationDisplayPage([NotNull] NotificationDisplayViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}