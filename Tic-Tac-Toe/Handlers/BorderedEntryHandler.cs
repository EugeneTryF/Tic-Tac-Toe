using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Tic_Tac_Toe.Elements;
using Microsoft.Maui;
using Microsoft.Maui.Platform;

#if ANDROID
using Android.Graphics.Drawables;
#endif

namespace Tic_Tac_Toe.Handlers;

public partial class BorderedEntryHandler : EntryHandler
{

    public BorderedEntryHandler() : base()
    {
#if ANDROID
        Mapper.AppendToMapping(nameof(BorderedEntry.BorderColor), MapBorderProperties);
        Mapper.AppendToMapping(nameof(BorderedEntry.BorderThickness), MapBorderProperties);
        Mapper.AppendToMapping(nameof(BorderedEntry.CornerRadius), MapBorderProperties);
#endif
    }

#if ANDROID
    private void MapBorderProperties(IEntryHandler handler, IEntry entry)
    {
        
        BorderedEntry border = entry as BorderedEntry;

        if (handler.PlatformView is not null)
        {
            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius((float)border.CornerRadius);
            gradientDrawable.SetStroke((int)border.BorderThickness, border.BorderColor.ToPlatform(), 1.0f, 1.0f);
            gradientDrawable.SetColor(Android.Graphics.Color.Transparent);

            handler.PlatformView.SetBackgroundDrawable(gradientDrawable);

            int padding = 20;
            handler.PlatformView.SetPadding(padding, padding, padding, padding);
        }
    }
#endif

}
