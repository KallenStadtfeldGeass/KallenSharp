using Item = LeagueSharp.Common.Items.Item;
using ItemData = LeagueSharp.Common.Data.ItemData;

namespace S__Class_Tristana.Data
{
    internal sealed class Items
    {
        public class Offensive
        {
            public readonly Item Botrk = new Item(ItemData.Blade_of_the_Ruined_King.GetItem().Id);
            public readonly Item Cutless = new Item(ItemData.Bilgewater_Cutlass.GetItem().Id);
            public readonly Item Hydra = new Item(ItemData.Ravenous_Hydra_Melee_Only.GetItem().Id);
            public readonly Item Tiamat = new Item(ItemData.Tiamat_Melee_Only.GetItem().Id);
            public readonly Item GunBlade = new Item(ItemData.Hextech_Gunblade.GetItem().Id);
            public readonly Item Muraman = new Item(ItemData.Muramana.GetItem().Id);
            public readonly Item GhostBlade = new Item(ItemData.Youmuus_Ghostblade.GetItem().Id);
        }

        public class Defensive
        {
            public readonly Item Qss = new Item(ItemData.Quicksilver_Sash.GetItem().Id);
            public readonly Item Merc = new Item(ItemData.Mercurial_Scimitar.GetItem().Id);
        }

        public class Trinkets
        {
            public readonly Item Orb = new Item(3363);
        }
    }
}