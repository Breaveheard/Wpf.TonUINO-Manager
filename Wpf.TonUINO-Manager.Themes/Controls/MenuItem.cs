using System.Windows;

namespace Wpf.TonUINOManager.Themes.Controls
{
    public class MenuItem : System.Windows.Controls.MenuItem
    {
        static MenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem), new FrameworkPropertyMetadata(typeof(MenuItem)));
        }
    }
}
