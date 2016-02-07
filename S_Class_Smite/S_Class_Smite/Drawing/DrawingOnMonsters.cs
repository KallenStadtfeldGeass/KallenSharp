using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using S_Class_Smite.Libary;
using Color = System.Drawing.Color;

namespace S_Class_Smite.Drawing
{
    internal class DrawingOnMonsters : Core
    {
        private const string _MenuNameBase = ".Monster Menu";
        private const string _MenuItemBase = ".Monster.";

        public static Menu DrawingMonsterMenu()
        {
            var menu = new Menu(_MenuNameBase, "monsterMenu");
            menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMonsters", "Draw On Monsters").SetValue(true));

            foreach (var key in MonsterStructures.MonsterBarDictionary.Keys)
            {
                menu.AddItem(new MenuItem(_MenuItemBase + $"Boolean.Draw.{key}", $"Draw Damage On {key}").SetValue(true));
            }
            menu.AddItem(
                new MenuItem(_MenuItemBase + "Float.RenderDistance",
                    "Render Distance (Display Damage to monsters within)").SetValue(new Slider(550, 200, 1000)));
            menu.AddItem(
                new MenuItem(_MenuItemBase + "Boolean.InnerColor", "Inner Market Color").SetValue(new Circle(true,
                    Color.DeepSkyBlue)));

            menu.AddItem(
                new MenuItem(_MenuItemBase + "Boolean.OuterColor", "Outer Marker Color").SetValue(new Circle(true,
                    Color.Crimson)));

            menu.AddItem(
                new MenuItem(_MenuItemBase + "Boolean.MarkerKillableColor", "Killable Marker Color").SetValue(
                    new Circle(true, Color.Green)));

            menu.AddItem(
                new MenuItem(_MenuItemBase + "Boolean.NonFill", "NonFill Damage Color").SetValue(new Circle(false,
                    Color.LightSlateGray)));

            menu.AddItem(
                new MenuItem(_MenuItemBase + "Boolean.KillableColor", "Killable Text").SetValue(new Circle(true,
                    Color.Red)));
            return menu;
        }

        //foreach (var minion in ObjectManager.Get<Obj_AI_Minion>())
        //         {

        //             if (minion.Distance(Player) > SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.Distance").GetValue<Slider>().Value) continue; // Out of render range
        //             if (minion.IsAlly) continue; //This is not Dota2
        //             if (minion.IsDead) continue;//Dont poke the dead
        //             if (!minion.IsMinion) continue; //Differect Function

        //             if (Player.GetAutoAttackDamage(minion) > minion.Health) // Is killable
        //             {
        //                 Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MarkerKillableColor").GetValue<Circle>().Color, 2);
        //             }


        //             else // Not killable
        //             {
        //                 Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MarkerInnerColor").GetValue<Circle>().Color, 2);

        //                 var remainingHp = (int)100 * (minion.Health - Player.GetAutoAttackDamage(minion)) / minion.Health;

        // Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + (float)remainingHp + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MakerOuterColor").GetValue<Circle>().Color, 2);
        //             }
        //         }

        //    menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMonsters", "Draw On Monsters").SetValue(true));

        //        foreach (var key in MonsterStructures.MonsterBarDictionary.Keys)
        //        {
        //            menu.AddItem(new MenuItem(_MenuItemBase + $"Boolean.Draw.{key}", $"Draw Damage On {key}").SetValue(true));
        //        }
        //menu.AddItem(new MenuItem(_MenuItemBase + "Float.RenderDistance", "Render Distance (Display Damage to monsters within)").SetValue(new Slider(550, 200, 1000)));
        //        menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.InnerColor", "Inner Market Color").SetValue(new Circle(true, Color.DeepSkyBlue)));

        //        menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.OuterColor", "Outer Marker Color").SetValue(new Circle(true, Color.Crimson)));

        //        menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.MarkerKillableColor", "Killable Marker Color").SetValue(new Circle(true, Color.Green)));

        //        menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.NonFill", "NonFill Damage Color").SetValue(new Circle(false, Color.LightSlateGray)));

        //        menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.KillableColor", "Killable Text").SetValue(new Circle(true, Color.Red)));
        public static void OnDrawMonster(EventArgs args)
        {
            if (!SMenu.Item(_MenuItemBase + "Boolean.DrawOnMonsters").GetValue<bool>()) return; // Drawing Disabled
            if (Player.IsDead) return;

            foreach (var monster in ObjectManager.Get<Obj_AI_Minion>())
            {
                if (monster.Team != GameObjectTeam.Neutral || !monster.IsValid || !monster.IsHPBarRendered) continue;

                var dmg = Smite.SmiteDamage(monster);
                if (dmg > 0)
                {
                    var found = false;


                    foreach (var key in MonsterStructures.MonsterBarDictionary.Keys)
                    {
                        if (string.Equals(monster.CharData.BaseSkinName, key, StringComparison.CurrentCultureIgnoreCase))
                            // is a monster we care about
                        {
                            if (SMenu.Item(_MenuItemBase + $"Boolean.Draw.{key}").GetValue<bool>()) // draw on monster
                                found = true;
                        }
                    }

                    if (!found) continue;

                    if (dmg > monster.Health) // Is killable
                        Render.Circle.DrawCircle(monster.Position, monster.BoundingRadius + 50,
                            SMenu.Item(_MenuItemBase + "Boolean.MarkerKillableColor").GetValue<Circle>().Color, 2);


                    else // Not killable
                    {
                        Render.Circle.DrawCircle(monster.Position, monster.BoundingRadius + 50,
                            SMenu.Item(_MenuItemBase + "Boolean.MarkerInnerColor").GetValue<Circle>().Color, 2);

                        var remainingHp = (int) 100*(monster.Health - Smite.SmiteDamage(monster))/monster.Health;

                        Render.Circle.DrawCircle(monster.Position, monster.BoundingRadius + (float) remainingHp + 50,
                            SMenu.Item(_MenuItemBase + "Boolean.MakerOuterColor").GetValue<Circle>().Color, 2);
                    }

                    if (SMenu.Item(_MenuItemBase + "Boolean.KillableColor").GetValue<Circle>().Active)
                    {
                        if (dmg > monster.Health)
                            LeagueSharp.Drawing.DrawText(monster.Position.X, monster.Position.Y + 100,
                                SMenu.Item(_MenuItemBase + "Boolean.KillableColor").GetValue<Circle>().Color, "Killable");
                    }
                }
            }
        }
    }
}
