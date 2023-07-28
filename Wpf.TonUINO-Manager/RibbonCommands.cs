
using Wpf.TonUINOManager.Common.Controls;

namespace Wpf.TonUINOManager
{
    using Microsoft.WindowsAPICodePack.Dialogs;
    using Wpf.Common.ViewModel;
    using Wpf.TonUINOManager.Views;

    /// <summary>
    /// RibbonCommands class
    /// </summary>
    public class RibbonCommands
    {
        #region Private Fields

        private readonly MainWindowVm _mainWindowVm;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonCommands"/> class.
        /// </summary>
        /// <param name="mainWindowVm">The main window vm.</param>
        public RibbonCommands(MainWindowVm mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;

            this.NewCommand = new DelegateCommand(NewCommand_Execute);
            this.OpenCommand = new DelegateCommand(OpenCommand_Execute);
            this.CloseCommand = new DelegateCommand(CloseCommand_Execute);
            this.OptionsCommand = new DelegateCommand(OptionsCommand_Execute);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Creates new command.
        /// </summary>
        public IDelegateCommand NewCommand { get; set; }

        /// <summary>
        /// Gets or sets the open command.
        /// </summary>
        public IDelegateCommand OpenCommand { get; set; }

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        public IDelegateCommand CloseCommand { get; set; }

        /// <summary>
        /// Gets or sets the options command.
        /// </summary>
        public IDelegateCommand OptionsCommand { get; set; }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void NewCommand_Execute(object obj)
        {
            throw new System.NotImplementedException();
        }

        private void OpenCommand_Execute(object obj)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var repository = new AudioRepository();
                repository.OpenRepository(dialog.FileName);
                //this._mainWindowVm.Repository = repository;
            }
        }

        private void CloseCommand_Execute(object obj)
        {
            throw new System.NotImplementedException();
        }

        private void OptionsCommand_Execute(object obj)
        {
            var optionsWindow = new OptionsWindow();
            optionsWindow.Show();
        }

        #endregion
    }
}
