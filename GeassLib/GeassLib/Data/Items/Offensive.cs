using Item = LeagueSharp.Common.Items.Item;
using ItemData = LeagueSharp.Common.Data.ItemData;

namespace GeassLib.Data.Items
{
    public class Offensive
    {
        public Item Botrk { get; } = new Item(ItemData.Blade_of_the_Ruined_King.GetItem().Id);
        public Item Cutless { get; } = new Item(ItemData.Bilgewater_Cutlass.GetItem().Id);
        public Item Hydra { get; } = new Item(ItemData.Ravenous_Hydra_Melee_Only.GetItem().Id);
        public Item Tiamat { get; } = new Item(ItemData.Tiamat_Melee_Only.GetItem().Id);
        public Item GunBlade { get; } = new Item(ItemData.Hextech_Gunblade.GetItem().Id);
        public Item Muraman { get; } = new Item(ItemData.Muramana.GetItem().Id);
        public Item GhostBlade { get; } = new Item(ItemData.Youmuus_Ghostblade.GetItem().Id);
    }
}
