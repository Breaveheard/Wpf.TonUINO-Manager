namespace Wpf.TonUINOManager.Common.Controls
{
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
        }

        #endregion

        #region Properties



        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => _file.Tag.Title;
            set
            {
                if (value != _file.Tag.Title)
                {
                    _file.Tag.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}