
namespace Wpf.TonUINOManager.Views
{
    using Wpf.Themes;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using ControlzEx.Theming;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Private Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow(MainWindowVm mainWindowVm)
        {
            this.InitializeComponent();
            this.DataContext = mainWindowVm;
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}