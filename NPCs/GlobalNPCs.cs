
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
using Nocturn.NPCs.Enemies.Worms;

namespace Nocturn.NPCs
{

    public class NocturnGlobalNPC : GlobalNPC
    {
        #region Worm Regime
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) // run on clients only
        {
            if (NocturnWorld.WormInvasionUp && spawnInfo.player.position.Y / 16 < Main.worldSurface)
            {
                pool.Clear();
                pool.Add(NPCType<BloodBringer_Head>(), 0.1f);
                pool.Add(NPCType<CrystalWorm_Head>(), 0.05f);
                pool.Add(NPCType<Gigaworm_Head>(), 0.05f);
               // pool.Add(NPCType<AllKnowerHead>(), 0.025f);
               // pool.Add(NPCType<StarlightGlobule>(), 0.02f);
            }
        }
        #endregion

        public override void NPCLoot(NPC npc)
        {
            #region Wormblood shit
            if (npc.type == NPCID.DiggerHead)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Wormblood"), Main.rand.Next(2, 5));
            }
            if (npc.type == NPCID.GiantWormHead)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Wormblood"), Main.rand.Next(2, 5));
            }
            if (npc.type == NPCID.TombCrawlerHead)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Wormblood"), Main.rand.Next(2, 5));
            }
            if (npc.type == NPCID.DevourerHead)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Wormblood"), Main.rand.Next(2, 5));
            }
            if (npc.type == NPCID.DuneSplicerHead)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Wormblood"), Main.rand.Next(2, 5));
            }
            if (npc.type == NPCID.SeekerHead)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Wormblood"), Main.rand.Next(2, 5));
            }
            #endregion
        }
       
    }
}