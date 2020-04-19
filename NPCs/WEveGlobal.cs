using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Nocturn.NPCs
{
    class WEveGlobal : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) // run on clients only
        {
            if (NocturnWorld.WormInvasionUp && (Main.invasionX == mod.NPCType("WEvent"))) //If the invasion is up and the invasion has reached the spawn pos
            {
                pool.Clear();

                if (!Main.dayTime && Main.time <= 16220) // before midnight
                {
                    int numBB = 0;
                    int numCW = 0;
                    int numGW = 0;


                    for (int i = 0; i < Main.maxNPCs; i++) // to reduce the amount of loops we are doing 
                    {
                        NPC checkNPC = Main.npc[i];
                        if (checkNPC.active)
                        {
                            if (checkNPC.type == mod.NPCType("Gigaworm_Head")) numGW++;
                            else if (checkNPC.type == mod.NPCType("Crystalworm_Head")) numCW++;
                            else if (checkNPC.type == mod.NPCType("BloodBringer_Head")) numBB++;

                        }
                    }

                    pool.Clear();
                    if (Main.time >= 5400)
                    {
                        pool.Add(mod.NPCType("Gigaworm_Head"), 0.1f);

                        pool.Add(mod.NPCType("Crystalworm_Head"), 0.2f);

                        pool.Add(mod.NPCType("BloodBringer_Head"), 0.05f);
                    }

                }
            }
        }

        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (NocturnWorld.WormInvasionUp)
            {

                spawnRate = 100; //Lower the number, the more spawns
                maxSpawns = 50; //Max spawns of NPCs depending on NPC value

            }
        }

        public override void PostAI(NPC npc)
        {
            if (NocturnWorld.WormInvasionUp)
            {
                //Changes NPCs so they do not despawn
                bool tooFar = false;

                if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) >= 2000)
                {
                    tooFar = true;
                }
                else
                {
                    tooFar = false;
                }

                if (Main.invasionX == (double)mod.NPCType("WEvent"))
                {
                    if (!tooFar)
                    {
                        npc.timeLeft = 500;
                    }
                    else if (tooFar && !npc.townNPC)
                    {
                        npc.active = false;
                    }
                }
            }

        }
    }
}
