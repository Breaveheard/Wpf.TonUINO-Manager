
namespace Wpf.TonUINOManager.Views
{
    using ControlzEx.Theming;
    using Microsoft.WindowsAPICodePack.Dialogs;
    using MvvmGen;
    using System;
    using System.Windows;
    using Wpf.Themes;
    using Wpf.TonUINOManager.Common.Controls;

    [ViewModel]
    public partial class MainWindowVm
    {
        #region Private Fields

        [Property] private AudioRepository _audioRepository;
        private ThemeTypes _selectedThemeType;

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets the possible theme types.
        /// </summary>
        public ThemeTypes[] PossibleThemeTypes => new ThemeTypes[] { ThemeTypes.Dark, ThemeTypes.Light };

        /// <summary>
        /// Gets or sets the type of the selected theme.
        /// </summary>
        public ThemeTypes SelectedThemeType
        {
            get
            {
                var theme = ThemeManager.Current.DetectTheme(Application.Current);
                _selectedThemeType = Enum.Parse<ThemeTypes>(theme.BaseColorScheme);

                return _selectedThemeType;
            }

            set
            {
                _selectedThemeType = value;

                ThemesController.SetTheme(value);
                ThemeManager.Current.ChangeTheme(Application.Current, $"{value}.Sienna");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Opens the repository.
        /// </summary>
        /// <param name="repositoryPath">The repository path.</param>
        public void OpenRepository(string repositoryPath)
        {
            var repository = new AudioRepository();
            repository.OpenRepository(repositoryPath);
            this.AudioRepository = repository;
        }

        #endregion

        #region Private Methods

        [Command]
        private void NewRepositoryDialog()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.AudioRepository = new AudioRepository();
            }
        }

        [Command]
        private void OpenRepositoryDialog()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.OpenRepository(dialog.FileName);
            }
        }

        [Command]
        private void OptionsDialog()
        {
            var optionsWindow = new OptionsWindow();
            optionsWindow.ShowDialog();
        }

        #endregion
    }
}
