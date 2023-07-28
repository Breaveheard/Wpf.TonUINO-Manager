using System.Windows;

namespace Wpf.TonUINOManager.Themes.Controls
{
    public class Menu : System.Windows.Controls.Menu
    {
        static Menu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Menu), new FrameworkPropertyMetadata(typeof(Menu)));
        }
    }
}
