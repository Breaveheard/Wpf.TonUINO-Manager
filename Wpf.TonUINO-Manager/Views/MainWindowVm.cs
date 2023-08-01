
namespace Wpf.TonUINOManager.Views
{
    using Microsoft.WindowsAPICodePack.Dialogs;
    using MvvmGen;
    using Microsoft.Win32;
    using Wpf.TonUINOManager.Common.Controls;

    [ViewModel]
    public partial class MainWindowVm
    {
        #region Private Fields

        [Property] private AudioRepository _audioRepository;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selected audio folder.
        /// </summary>
        public AudioFolder SelectedAudioFolder
        {
            get => this.AudioRepository?.SelectedAudioFolder;
            set
            {
                this.AudioRepository.SelectedAudioFolder = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected audio file.
        /// </summary>
        public AudioFile SelectedAudioFile
        {
            get => this.AudioRepository?.SelectedAudioFolder?.SelectedAudioFile;
            set
            {
                if (this.SelectedAudioFolder != null)
                {
                    this.AudioRepository.SelectedAudioFolder.SelectedAudioFile = value;
                }

                this.OnPropertyChanged();
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

        [Command(CanExecuteMethod = nameof(CanEditFiles))]
        private void EditFiles()
        {
            if (this.SelectedAudioFile != null)
            {
                var editAudioFileWindow = new EditAudioFileWindow(this.SelectedAudioFile);
                editAudioFileWindow.ShowDialog();
                //this.OnPropertyChanged(nameof(this.AudioRepository));
                //this.OnPropertyChanged(nameof(this.SelectedAudioFile));

                this.AudioRepository.ReloadFolder();
            }
        }

        [CommandInvalidate(nameof(SelectedAudioFile))]
        private bool CanEditFiles()
        {
            return this.SelectedAudioFile != null;
        }

        [Command]
        private void NewFolder()
        {
            this.AudioRepository.CreateFolder();
        }

        [Command]
        private void RemoveFolder()
        {
            this.AudioRepository.RemoveFolder(SelectedAudioFolder);
        }

        [Command]
        private void AddFiles(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                // Check if file needs to be converted into mp3

                this.AudioRepository.AddFile(openFileDialog.FileName);
                this.OnPropertyChanged(nameof(this.AudioRepository));
                this.OnPropertyChanged(nameof(this.SelectedAudioFolder));
            }
        }

        [Command]
        private void RemoveFiles(object obj)
        {
            if (obj is AudioFolder folder)
            {
                folder.RemoveFile(folder.SelectedAudioFile);
            }
        }

        #endregion
    }
}
