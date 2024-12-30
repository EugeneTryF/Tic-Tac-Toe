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
            //textBox.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(border.BorderColor.ToWindowsColor());

            //textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(
            //    border.BorderThickness);

            //textBox.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(border.CornerRadius);


            textBox.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(border.BorderColor.ToWindowsColor());
            
            textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(
                border.BorderThickness);

            textBox.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(border.CornerRadius);
            
            textBox.Resources["TextControlBackgroundFocused"] = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
            
            
            //var parentPanel = textBox.Parent as Panel;
            //if (parentPanel == null) return;

            //parentPanel.Children.Remove(textBox);

            //var canvas = new Canvas();
            //parentPanel.Children.Add(canvas);

            //var rectangle = new Rectangle
            //{
            //    Stroke = new Microsoft.UI.Xaml.Media.SolidColorBrush(border.BorderColor.ToWindowsColor()),
            //    StrokeThickness = border.BorderThickness,
            //    RadiusX = border.CornerRadius,
            //    RadiusY = border.CornerRadius,
            //    Width = textBox.Width - (2 * border.BorderThickness),
            //    Height = textBox.Height - (2 * border.BorderThickness),
            //};
            //Canvas.SetLeft(rectangle, 0);
            //Canvas.SetTop(rectangle, 0);
            //canvas.Children.Add(rectangle);

            //Canvas.SetLeft(textBox, border.BorderThickness);
            //Canvas.SetTop(textBox, border.BorderThickness);
            //canvas.Children.Add(textBox);
        }
    }
}
