namespace Wpf.TonUINOManager.Common.Controls
{
    using System.Collections.ObjectModel;
    using System.IO;

    /// <summary>
    /// Repository class.
    /// </summary>
    public class AudioRepository
    {
        #region Private Fields

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

        public ObservableCollection<AudioFolder> Folders { get; set; }

        #endregion

        #region Public Methods

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
                Folders.Remove(folder);
            }
        }

        /// <summary>
        /// Loads the repository.
        /// </summary>
        /// <param name="path">The path.</param>
        public void OpenRepository(string path)
        {
            LoadFolder(path);
        }

        #endregion

        #region Private Methods

        private void LoadFolder(string path)
        {
            var folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folder);
                if (int.TryParse(directoryInfo.Name, out int number) & number >= 0 & number <= 255)
                {
                    AddFolder(new AudioFolder(folder));
                }
            }
        }

        #endregion
    }
}
