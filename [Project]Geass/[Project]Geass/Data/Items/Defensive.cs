using LeagueSharp.Common.Data;

namespace _Project_Geass.Data.Items
{
    public class Defensive
    {
        public LeagueSharp.Common.Items.Item Qss { get; } =
            new LeagueSharp.Common.Items.Item(ItemData.Quicksilver_Sash.GetItem().Id);

        public LeagueSharp.Common.Items.Item Merc { get; } =
            new LeagueSharp.Common.Items.Item(ItemData.Mercurial_Scimitar.GetItem().Id);
    }
}