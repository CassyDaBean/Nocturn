using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Events;
using Nocturn.NPCs.WE;




namespace Nocturn.NPCs
{
    class WEve
    {
        public static Mod mod = ModLoader.GetMod("Nocturn");
        public static List<int> Wave1worms = new List<int>() {
            mod.NPCType("BloodBringer_Head"),
            mod.NPCType("Crystalworm_Head"),
            mod.NPCType("Gigaworm_Head"),

        };

        public static void StartInvasion()
        {
            //Set to no invasion
            if (Main.invasionType != 0 && Main.invasionSize == 0)
            {
                Main.invasionType = 0;
            }
            if (Main.invasionType == 0)
            {
                //Checks amount of players
                int num = 0;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active)
                    {
                        num++;
                    }
                }
                if (num > 0)
                {
                    //Invasion setup
                    Main.invasionType = -1;
                    NocturnWorld.WormInvasionUp = true;
                    Main.invasionX = Main.maxTilesX;
                }
            }
        }

        public static void InvasionWarning()
        {
            string text = "";
            if (Main.invasionX == mod.NPCType("WEvent"))
            {
                text = "The Worm Regime has Arrived!";
            }
            if (Main.netMode == 0)
            {
                Main.NewText(text, 170, 222, 150, false);
                return;
            }
            if (Main.netMode == 2)
            {
                //Sync with net               
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(170, 222, 150));
            }
        }
        public static void UpdateInvasion()
        {
            if (NocturnWorld.WormInvasionUp)
            {

            }
        }
    }
}
