
namespace Wpf.TonUINOManager.Common.Controls
{
    using System.Collections.ObjectModel;
    using System.IO;
    
    /// <summary>
    /// AudioFolder class
    /// </summary>
    public class AudioFolder
    {
        #region Private Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFolder"/> class.
        /// </summary>
        public AudioFolder() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFolder"/> class.
        /// </summary>
        public AudioFolder(string path)
        {
            this.Files = new ObservableCollection<AudioFile>();
            this.LoadFiles(path);

            var directoryInfo = new DirectoryInfo(path);
            this.Name = directoryInfo.Name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public ObservableCollection<AudioFile> Files { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a audio file to the list.
        /// </summary>
        public void AddFile(AudioFile file)
        {
            if (file != null)
            {
                this.Files.Add(file);
            }
        }

        /// <summary>
        /// Removes a audio file from the list.
        /// </summary>
        /// <param name="file">The file.</param>
        public void RemoveFile(AudioFile file)
        {
            if (Files.Contains(file))
            {
                this.Files.Remove(file);
            }
        }

        #endregion

        #region Private Methods

        private void LoadFiles(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                this.AddFile(new AudioFile(file));
            }
        }

        #endregion
    }
}
