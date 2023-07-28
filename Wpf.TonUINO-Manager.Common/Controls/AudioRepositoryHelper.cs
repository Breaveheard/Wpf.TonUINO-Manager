
namespace Wpf.TonUINOManager.Common.Controls
{
    using System.IO;
    
    /// <summary>
    /// Audio Repository Helper
    /// </summary>
    internal class AudioRepositoryHelper
    {
        #region Private Fields

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

        #endregion

        #region Private Methods

        #endregion
    }
}
