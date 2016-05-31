using Item = LeagueSharp.Common.Items.Item;
using ItemData = LeagueSharp.Common.Data.ItemData;

namespace GeassLib.Data.Items
{
    class Defensive
    {
        public Item Qss { get; } = new Item(ItemData.Quicksilver_Sash.GetItem().Id);
        public Item Merc { get; } = new Item(ItemData.Mercurial_Scimitar.GetItem().Id);
    }
}
