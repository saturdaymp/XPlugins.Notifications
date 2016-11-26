namespace ExampleClient
{
    /// <summary>
    ///     A bunch of buttons to test the sending of notifications.
    /// </summary>
    public partial class MainPage
    {
        /// <summary>
        ///     Create the main page and intialize the view model.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }
    }
}