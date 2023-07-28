namespace Wpf.TonUINOManager.Views
{
    using Wpf.Common.ViewModel;
    using Wpf.TonUINOManager.Common.Controls;

    /// <summary>
    /// Interaction logic for MainWindowVm 
    /// </summary>
    /// <seealso cref="Wpf.Common.ViewModel.ViewModelBase" />
    public class MainWindowVmTemp : ViewModelBase
    {
        #region Private Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowVm"/> class.
        /// </summary>
        public MainWindowVmTemp()
        {
            //this.Commands = new RibbonCommands(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        public RibbonCommands Commands { get; set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        public AudioRepository Repository { get; set; }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}