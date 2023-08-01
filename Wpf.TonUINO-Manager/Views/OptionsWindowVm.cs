
namespace Wpf.TonUINOManager.Views
{
    using ControlzEx.Theming;
    using MvvmGen;
    using System;
    using Wpf.Themes;

    /// <summary>
    /// OptionsWindowVm class.
    /// </summary>
    [ViewModel]
    public partial class OptionsWindowVm
    {
        #region Private Fields

        private ThemeTypes _selectedThemeType;
        
        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets the possible theme types.
        /// </summary>
        public ThemeTypes[] PossibleThemeTypes => new[] { ThemeTypes.Dark, ThemeTypes.Light };

        /// <summary>
        /// Gets or sets the type of the selected theme.
        /// </summary>
        public ThemeTypes SelectedThemeType
        {
            get
            {
                var theme = ThemeManager.Current.DetectTheme(System.Windows.Application.Current);
                if (theme != null) _selectedThemeType = Enum.Parse<ThemeTypes>(theme.BaseColorScheme);

                return _selectedThemeType;
            }

            set
            {
                _selectedThemeType = value;

                ThemesController.SetTheme(value);
                ThemeManager.Current.ChangeTheme(System.Windows.Application.Current, $"{value}.Sienna");
            }
        }


        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}
