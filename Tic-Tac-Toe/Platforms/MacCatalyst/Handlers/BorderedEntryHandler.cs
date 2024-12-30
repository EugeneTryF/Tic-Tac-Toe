using Microsoft.Maui.Handlers;
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

    }
}
