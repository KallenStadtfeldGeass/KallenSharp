using System.Linq;
using LeagueSharp.Common;
using LeagueSharp;

namespace S__Class_Tristana.Events
{
    internal partial class OrbwalkerEvents
    {
        private void Combo()
        {

        }

        private void Mixed(Libaries.Champion champion)
        {
            if (SMenu.Item(MenuNameBase + "Mixed.Boolean.UseE").GetValue<bool>() && Champion.GetSpellE.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.HealthPercent))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!enemy.IsValidTarget(champion.GetSpellE.Range - SMenu.Item(MenuNameBase + "Mixed.Slider.MaxDistance").GetValue<Slider>().Value)) continue;
                    if (!SMenu.Item(MenuItemBase + "Mixed.Boolean.UseE.On." + enemy.ChampionName).GetValue<bool>()) continue;

                    champion.GetSpellE.Cast(enemy);
                    CommonOrbwalker.ForceTarget(enemy);

                    if (SMenu.Item(MenuNameBase + "Mixed.Boolean.UseQ").GetValue<bool>())
                    {
                        if (champion.GetSpellQ.IsReady())
                            champion.GetSpellQ.Cast();

                        return;
                    }
                }
            }

            else if (SMenu.Item(MenuNameBase + "Mixed.Boolean.UseQ").GetValue<bool>() && Champion.GetSpellQ.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.HealthPercent))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!enemy.IsValidTarget(champion.GetSpellQ.Range - SMenu.Item(MenuNameBase + "Mixed.Slider.MaxDistance").GetValue<Slider>().Value)) continue;
                    champion.GetSpellQ.Cast();
                    CommonOrbwalker.ForceTarget(enemy);
                }
            }
        }



        private void LaneClear()
        {

        }

        private void LastHit()
        {


        }
    }
}