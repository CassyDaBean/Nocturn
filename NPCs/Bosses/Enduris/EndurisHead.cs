using Nocturn.Projectiles.Bosses.Aqua;
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
using Nocturn;

namespace Nocturn.NPCs.Bosses.Enduris
{
    public class EndurisHead : ModNPC
    {
        #region attack cool
        private float attackCool
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private int center
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        #endregion

        #region Int
        public int spawnparts = 0;
        #endregion

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enduris");

        }
        public override void SetDefaults()
        {
            spawnparts = 0;

            npc.aiStyle = 0;
            npc.width = 542;
            npc.height = 474;
            npc.lifeMax = 100;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void AI()
        {
            #region spawn body parts
            if (spawnparts == 0)
            {
                spawnparts = 1;
            }
            if (spawnparts == 1)
            {
                for (int k = 0; k < 2; k++) //this if for crating and centering the AFL's
                {
                    int Jawcount = 2;
                    int Jaw = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisJaw>(), npc.whoAmI,0,Jawcount);
                    
                    Main.npc[Jaw].ai[0] = npc.whoAmI;
                    Main.npc[Jaw].ai[1] = k;
                    EndurisJaw.SetPosition(Main.npc[Jaw]);
                    spawnparts = 2;
                    npc.netUpdate = true;
                }
            }
            if (spawnparts == 2)
            {
                
            }
            #endregion
        }

    }
}
