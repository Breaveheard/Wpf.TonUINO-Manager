
namespace Wpf.TonUINOManager
{
    using Wpf.TonUINOManager.Views;
    using Wpf.Themes;
    using System.Windows;
    using ControlzEx.Theming;
    using System;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Public Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;
            ThemeManager.Current.SyncTheme();

            ThemesController.SyncTheme();

            var mainWindowVm = new MainWindowVm();
            if (e.Args.Length > 0)
            {
                if (Directory.Exists(e.Args[0]))
                {
                    mainWindowVm.OpenRepository(e.Args[0]);
                }
            }
            
            var mainWindow = new MainWindow(mainWindowVm);
            mainWindow.Show();
        }

        #endregion
    }
}