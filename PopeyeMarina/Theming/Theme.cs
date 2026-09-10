using System.Drawing;
using System.Windows.Forms;

namespace PopeyeMarina.Theming
{
    // Centralized styling values so screens don't hardcode colors/fonts/spacing
    // individually. Intended to be the single place to change during the
    // visual polish pass later — screens should reference these, not
    // Color.FromArgb(...) or new Font(...) directly.
    public static class Theme
    {
        // Colors
        public static readonly Color SidebarColor = Color.FromArgb(105, 68, 142);      // #0C2233 - Btn Colors
        public static readonly Color AccentColor = Color.FromArgb(93, 202, 165);     // #5DCAA5 - Navlink active color
        public static readonly Color MutedTextColor = Color.FromArgb(155, 170, 181); // #9BAAB5 - Navlink non-active color
        public static readonly Color ContentBackColor = Color.White;
        public static readonly Color CardBackColor = Color.White;
        public static readonly Color PrimaryTextColor = Color.FromArgb(20, 20, 20);
        public static readonly Color SecondaryTextColor = Color.FromArgb(100, 100, 100);

        // Buttons
        public static readonly Color PrimaryButtonBackColor = SidebarColor;
        public static readonly Color PrimaryButtonForeColor = Color.White;

        // Fonts
        public const string FontFamily = "Segoe UI";

        public static Font LogoFont => new Font(FontFamily, 16F, FontStyle.Bold);
        public static Font SubtitleFont => new Font(FontFamily, 9F, FontStyle.Regular);
        public static Font NavFont(bool selected) =>
            new Font(FontFamily, 10F, selected ? FontStyle.Bold : FontStyle.Regular);
        public static Font ScreenHeaderFont => new Font(FontFamily, 18F, FontStyle.Bold);
        public static Font CardTitleFont => new Font(FontFamily, 11F, FontStyle.Bold);
        public static Font FieldLabelFont => new Font(FontFamily, 8.5F, FontStyle.Regular);
        public static Font InputFont => new Font(FontFamily, 10F, FontStyle.Regular);
        public static Font ButtonFont => new Font(FontFamily, 9.5F, FontStyle.Bold);

        // Layout
        public const int SidebarWidth = 220;
        public const int NavButtonHeight = 44;
        public const int ContentPadding = 24;
        public const BorderStyle CardBorderStyle = BorderStyle.FixedSingle;
    }
}