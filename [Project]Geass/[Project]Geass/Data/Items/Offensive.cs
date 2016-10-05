using LeagueSharp.Common.Data;

namespace _Project_Geass.Data.Items
{
    public class Offensive
    {
        #region Public Properties

        public LeagueSharp.Common.Items.Item Botrk { get; } =
            new LeagueSharp.Common.Items.Item(ItemData.Blade_of_the_Ruined_King.GetItem().Id);

        public LeagueSharp.Common.Items.Item Cutless { get; } =
            new LeagueSharp.Common.Items.Item(ItemData.Bilgewater_Cutlass.GetItem().Id);

        public LeagueSharp.Common.Items.Item GhostBlade { get; } =
            new LeagueSharp.Common.Items.Item(ItemData.Youmuus_Ghostblade.GetItem().Id);

        public LeagueSharp.Common.Items.Item GunBlade { get; } =
            new LeagueSharp.Common.Items.Item(ItemData.Hextech_Gunblade.GetItem().Id);

        public LeagueSharp.Common.Items.Item Hydra { get; } =
                            new LeagueSharp.Common.Items.Item(ItemData.Ravenous_Hydra_Melee_Only.GetItem().Id);

        public LeagueSharp.Common.Items.Item Muraman { get; } =
            new LeagueSharp.Common.Items.Item(ItemData.Muramana.GetItem().Id);

        public LeagueSharp.Common.Items.Item Tiamat { get; } =
                    new LeagueSharp.Common.Items.Item(ItemData.Tiamat_Melee_Only.GetItem().Id);

        #endregion Public Properties
    }
}