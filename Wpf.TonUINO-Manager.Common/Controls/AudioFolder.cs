
namespace Wpf.TonUINOManager.Common.Controls
{
    using System.Drawing;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Wpf.Common.ViewModel;
    
    /// <summary>
    /// AudioFolder class
    /// </summary>
    public class AudioFolder : ViewModelBase
    {
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
            this.FolderPath = path;

            if (!Directory.Exists(this.FolderPath))
            {
                Directory.CreateDirectory(this.FolderPath ?? string.Empty);
            }

            this.Files = new ObservableCollection<AudioFile>();
            this.LoadFiles(path);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public ObservableCollection<AudioFile> Files { get; set; }

        /// <summary>
        /// Gets or sets the selected audio file.
        /// </summary>
        public AudioFile SelectedAudioFile { get; set; }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        public string FolderPath { get; }

        /// <summary>
        /// Gets the cover picture.
        /// </summary>
        public Image Cover => this.Files.FirstOrDefault()?.Cover;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => Path.GetFileNameWithoutExtension(this.FolderPath);

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title => this.Files.FirstOrDefault()?.Title;

        /// <summary>
        /// Gets the artist.
        /// </summary>
        public string Artist => this.Files.FirstOrDefault()?.AlbumArtist;

        /// <summary>
        /// Gets the album.
        /// </summary>
        public string Album => this.Files.FirstOrDefault()?.Album;

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

            this.UpdatePropertyChange();
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
                var fileName = Path.Combine(FolderPath, file.FileName);

                if (File.Exists(fileName));
                {
                    File.Delete(fileName);
                }
            }

            this.UpdatePropertyChange();
        }

        /// <summary>
        /// Removes the files.
        /// </summary>
        public void RemoveFiles()
        {
            foreach (var file in this.Files)
            {
                var fileName = Path.Combine(FolderPath, file.FileName);

                if (File.Exists(fileName)) ;
                {
                    File.Delete(fileName);
                }
            }

            this.Files.Clear();

            this.UpdatePropertyChange();
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

        private void UpdatePropertyChange()
        {
            this.OnPropertyChanged(nameof(this.Cover));
            this.OnPropertyChanged(nameof(this.Name));
            this.OnPropertyChanged(nameof(this.Artist));
            this.OnPropertyChanged(nameof(this.Album));
        }

        #endregion
    }
}
