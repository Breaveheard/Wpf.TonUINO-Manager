using System.Windows;

namespace Wpf.TonUINOManager.Themes.Controls
{
    public class Switch : System.Windows.Controls.Primitives.ToggleButton
    {
        static Switch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Switch), new FrameworkPropertyMetadata(typeof(Switch)));
        }
    }
}
