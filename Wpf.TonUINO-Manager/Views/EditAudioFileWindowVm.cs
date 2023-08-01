
namespace Wpf.TonUINOManager.Views
{
    using MvvmGen;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using Wpf.TonUINOManager.Common.Controls;

    /// <summary>
    /// OptionsWindowVm class.
    /// </summary>
    [ViewModel]
    public partial class EditAudioFileWindowVm
    {
        #region Public Fields

        public AudioFile File { get; set; }

        #endregion

        #region Private Fields

        private EditAudioFileWindow _window;

        [Property] private string _title;
        [Property] private string _albumArtist;
        [Property] private uint _track;
        [Property] private Image _cover;

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the audio file.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="file">The file.</param>
        public void SetAudioFile(EditAudioFileWindow window, AudioFile file)
        {
            this._window = window;

            this.File = file;
            this.Title = this.File.TagFile.Tag.Title;
            this.AlbumArtist = this.File.TagFile.Tag.FirstAlbumArtist;
            this.Track = this.File.TagFile.Tag.Track;

            var cover = this.File.TagFile.Tag.Pictures.FirstOrDefault();
            if (cover?.Data.Data != null)
            {
                using MemoryStream ms = new MemoryStream(cover.Data.Data);
                if (cover.MimeType == "image/png")
                {
                    PngBitmapEncoder enc = new PngBitmapEncoder();
                    enc.Interlace = PngInterlaceOption.Off;
                    enc.Frames.Add(BitmapFrame.Create(ms));
                }

                this.Cover = Image.FromStream(ms).GetThumbnailImage(300, 300, null, System.IntPtr.Zero);
            }
        }

        #endregion

        #region Private Methods

        [Command]
        private void Save()
        {
            this.File.Title = this.Title;
            this.File.AlbumArtist = this.AlbumArtist;
            this.File.Track = this.Track.ToString();
            
            this.File.TagFile.Save();

            this._window.Close();
        }

        [Command]
        private void Cancel()
        {
            this._window.Close();
        }
        
        #endregion
    }
}
