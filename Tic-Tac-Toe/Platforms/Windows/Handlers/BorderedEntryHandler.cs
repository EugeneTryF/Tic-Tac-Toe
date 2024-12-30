using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Tic_Tac_Toe.Elements;

namespace Tic_Tac_Toe.Handlers;

public partial class BorderedEntryHandler : EntryHandler
{

    public BorderedEntryHandler() : base()
    {
        Mapper.AppendToMapping(nameof(BorderedEntry.BorderColor), MapBorderProperties);
        Mapper.AppendToMapping(nameof(BorderedEntry.BorderThickness), MapBorderProperties);
        Mapper.AppendToMapping(nameof(BorderedEntry.CornerRadius), MapBorderProperties);
    }

    private void MapBorderProperties(IEntryHandler handler, IEntry entry)
    {
        if (entry is BorderedEntry border && handler.PlatformView is TextBox textBox)
        {
            textBox.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(border.BorderColor.ToWindowsColor());
            
            textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(border.BorderThickness);

            textBox.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(border.CornerRadius);
            
            //Remove underline
            //textBox.Resources["TextControlBorderBrushFocused"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
            //textBox.Resources["TextControlBackgroundFocused"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);

            //textBox.GotFocus += (sender, args) =>
            //{
            //    textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(border.BorderThickness);
            //    textBox.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(border.BorderColor.ToWindowsColor());
            //    textBox.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(border.CornerRadius);

            //    textBox.Resources["TextControlBorderBrushFocused"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
            //    textBox.Resources["TextControlBackgroundFocused"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
            //};
        }
    }
}
