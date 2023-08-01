


namespace Wpf.TonUINOManager.Views
{
    using System.Windows;
    using System.Windows.Media;
    using Wpf.TonUINOManager.Common.Controls;

    /// <summary>
    /// Interaction logic for EditAudioFileWindow.xaml
    /// </summary>
    public partial class EditAudioFileWindow
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditAudioFileWindow" /> class.
        /// </summary>
        /// <param name="file"></param>
        public EditAudioFileWindow(AudioFile file)
        {
            this.InitializeComponent();
            var editAudioFileWindowVm = new EditAudioFileWindowVm();
            editAudioFileWindowVm.SetAudioFile(this, file);
            this.DataContext = editAudioFileWindowVm;
        }

        #endregion

        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            ImageControl.Source = (ImageSource)e.Data.GetData(typeof(ImageSource));
        }
    }
}
