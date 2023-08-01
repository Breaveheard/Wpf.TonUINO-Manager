
using System.Windows.Media;

namespace Wpf.TonUINOManager.Common.Controls
{
    using System;
    using System.IO;
    using TagLib.Mpeg;

    /// <summary>
    /// Audio Repository Helper
    /// </summary>
    internal class AudioRepositoryHelper
    {
        #region Private Fields

        private static MediaPlayer _MediaPlayer = new MediaPlayer();
        private static AudioFile _audioFile;
        private static bool _IsPlaying;

        #endregion

        #region Constructors

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the audio repository.
        /// </summary>
        public static void CreateAudioRepository(string name, string path)
        {
            if (!Directory.Exists(path))
            {
                if (path != null)
                {
                    Path.Combine(path, name);
                    Directory.CreateDirectory(path);
                }
            }
        }

        /// <summary>
        /// Plays the audio file.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        public static bool PlayAudioFile(AudioFile audioFile)
        {
            if (audioFile != null)
            {
                if (_audioFile != null && _audioFile != audioFile)
                {
                    _audioFile.IsPlaying = false;
                }

                _audioFile = audioFile;

                var uri = new Uri(audioFile.FullFileName);

                if (_MediaPlayer.Source == uri)
                {
                    if (_IsPlaying)
                    {
                        _MediaPlayer.Stop();
                        _IsPlaying = false;
                    }
                    else
                    {
                        _MediaPlayer.Play();
                        _IsPlaying = true;
                    }
                }
                else
                {
                    _MediaPlayer.Open(uri);
                    _MediaPlayer.Play();
                    _IsPlaying = true;
                }
            }

            return _IsPlaying;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
