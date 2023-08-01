
namespace Wpf.TonUINOManager.Common.Controls
{
    using System.Drawing;
    using System.Linq;
    using System.IO;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using Wpf.Common.ViewModel;

    /// <summary>
    /// AudioFile class.
    /// </summary>
    public class AudioFile : ViewModelBase
    {
        #region Private Fields

        private readonly TagLib.File _file;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFile" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public AudioFile(string file)
        {
            _file = new TagLib.Mpeg.AudioFile(file);
            this.PlayButtonCommand = new DelegateCommand(PlayButtonCommand_Execute);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFile"/> class.
        /// </summary>
        /// <param name="source">The file.</param>
        /// <param name="dest">The dest.</param>
        public AudioFile(string source, string dest)
        {
            if (!File.Exists(dest))
            {
                if (dest != null) File.Copy(source, dest);
            }

            _file = new TagLib.Mpeg.AudioFile(dest);
            this.PlayButtonCommand = new DelegateCommand(PlayButtonCommand_Execute);
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// The play button command
        /// </summary>
        public ICommand PlayButtonCommand { get; set; }

        /// <summary>
        /// Gets the file.
        /// </summary>
        public TagLib.File TagFile => this._file;

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string FileName => Path.GetFileName(_file.Name);

        /// <summary>
        /// Gets the full name of the file.
        /// </summary>
        public string FullFileName => _file.Name;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is playing.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => _file.Tag.Title;
            set
            {
                _file.Tag.Title = value;
                this.Update();
            }
        }

        /// <summary>
        /// Gets or sets the album artist.
        /// </summary>
        public string AlbumArtist
        {
            get
            {
                if (_file.Tag?.AlbumArtists?.Length > 0)
                {
                    return (string)_file.Tag?.AlbumArtists?.GetValue(0);
                }

                return string.Empty;
            }
            set
            {
                _file.Tag.AlbumArtists = new string[] { value };
                this.Update();
            }
        }

        /// <summary>
        /// Gets the album.
        /// </summary>
        public string Album
        {
            get => _file.Tag.Album;
            set
            {
                _file.Tag.Album = value;
                this.Update();
            }
        }

        /// <summary>
        /// Gets the cover.
        /// </summary>
        public Image Cover
        {
            get
            {
                var cover = _file.Tag.Pictures.FirstOrDefault();

                if (cover?.Data?.Data != null)
                {
                    using MemoryStream ms = new MemoryStream(cover.Data.Data);
                    if (cover.MimeType == "image/png")
                    {
                        PngBitmapEncoder enc = new PngBitmapEncoder();
                        enc.Interlace = PngInterlaceOption.Off;
                        enc.Frames.Add(BitmapFrame.Create(ms));
                    }
                    
                    return Image.FromStream(ms).GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the track.
        /// </summary>
        public string Track
        {
            get => _file.Tag.Track.ToString();
            set
            {
                uint.TryParse(value, out uint track);
                _file.Tag.Track = track;
                this.Update();
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            this.OnPropertyChanged(nameof(this.FileName));
            this.OnPropertyChanged(nameof(this.Title));
            this.OnPropertyChanged(nameof(this.AlbumArtist));
            this.OnPropertyChanged(nameof(this.Album));
        }

        #endregion

        #region Public Methods

        private void PlayButtonCommand_Execute(object obj)
        {
            this.IsPlaying = AudioRepositoryHelper.PlayAudioFile(this);
        }

        #endregion
    }
}