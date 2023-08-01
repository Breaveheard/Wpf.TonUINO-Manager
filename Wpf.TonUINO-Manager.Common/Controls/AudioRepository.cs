
namespace Wpf.TonUINOManager.Common.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;

    /// <summary>
    /// Repository class.
    /// </summary>
    public class AudioRepository
    {
        #region Private Fields

        private string _path;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioRepository"/> class.
        /// </summary>
        public AudioRepository()
        {
            Folders = new ObservableCollection<AudioFolder>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the folders.
        /// </summary>
        public ObservableCollection<AudioFolder> Folders { get; set; }

        /// <summary>
        /// Gets or sets the selected audio folder.
        /// </summary>
        public AudioFolder SelectedAudioFolder { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the folder.
        /// </summary>
        public void CreateFolder()
        {
            int value = 0;

            foreach (var audioRepositoryFolder in this.Folders)
            {
                int.TryParse(audioRepositoryFolder.Name, out var localValue);
                value = Math.Max(value, localValue);
            }

            value++;

            var path = Path.Combine(this._path, value.ToString("D2"));
            this.AddFolder(new AudioFolder(path));
        }

        /// <summary>
        /// Adds a folder to the list.
        /// </summary>
        /// <param name="folder">The folder.</param>
        public void AddFolder(AudioFolder folder)
        {
            if (folder != null)
            {
                Folders.Add(folder);
            }
        }

        /// <summary>
        /// Removes a folder from the list.
        /// </summary>
        /// <param name="folder">The folder.</param>
        public void RemoveFolder(AudioFolder folder)
        {
            if (Folders.Contains(folder))
            {
                folder.RemoveFiles();
                Folders.Remove(folder);
            }

            Directory.Delete(folder.FolderPath);
        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void AddFile(string file)
        {
            int destNumber = 0;
            if (this.SelectedAudioFolder.Files != null)
            {
                foreach (var aFile in this.SelectedAudioFolder.Files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(aFile.FileName);
                    int.TryParse(fileName, out int number);
                    destNumber = Math.Max(destNumber, number);
                }
            }

            destNumber++;

            var destination = Path.Combine(this.SelectedAudioFolder.FolderPath, destNumber.ToString("D3") + ".mp3");
            var audioFile = new AudioFile(file, destination);
            this.SelectedAudioFolder?.AddFile(audioFile);
        }

        /// <summary>
        /// Loads the repository.
        /// </summary>
        /// <param name="path">The path.</param>
        public void OpenRepository(string path)
        {
            this._path = path;
            LoadFolder(path);
        }

        /// <summary>
        /// Reloads the folder.
        /// </summary>
        public void ReloadFolder()
        {
            if (Directory.Exists(this._path))
            {
                this.Folders.Clear();
                this.LoadFolder(this._path);
            }
        }


        #endregion

        #region Private Methods

        private void LoadFolder(string path)
        {
            var folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                var directoryInfo = new DirectoryInfo(folder);
                if (int.TryParse(directoryInfo.Name, out var number) & number >= 0 & number <= 99)
                {
                    AddFolder(new AudioFolder(folder));
                }
            }
        }

        #endregion
    }
}
