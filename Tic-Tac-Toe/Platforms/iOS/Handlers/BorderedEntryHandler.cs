using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Tic_Tac_Toe.Elements;
using UIKit;

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
        if (entry is BorderedEntry border && handler.PlatformView is UITextField textField)
        {
            textField.Layer.BorderColor = border.BorderColor.ToCGColor();
            textField.Layer.BorderWidth = (float)border.BorderThickness;
            textField.Layer.CornerRadius = (float)border.CornerRadius;

            textField.Layer.MasksToBounds = true;

        }
    }
}
