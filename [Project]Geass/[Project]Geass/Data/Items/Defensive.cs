using Item = LeagueSharp.Common.Items.Item;
using ItemData = LeagueSharp.Common.Data.ItemData;

namespace _Project_Geass.Data.Items
{
    public class Defensive
    {
        public Item Qss { get; } = new Item(ItemData.Quicksilver_Sash.GetItem().Id);
        public Item Merc { get; } = new Item(ItemData.Mercurial_Scimitar.GetItem().Id);
    }
}