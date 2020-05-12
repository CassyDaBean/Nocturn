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
        #region Color glow thingy
        public override Color? GetAlpha(Color lightColor) // color of glow
        {
            return new Color(0.7f, 0.7f, 0.7f);
        }
        #endregion

        float tvel = 20f;
        public override void AI()
        {
            #region spawn body parts
            if (spawnparts == 0)
            {
                spawnparts = 1;
            }
            if (spawnparts == 1)
            {
                for (int k = 0; k < 2; k++) 
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
                for (int e=0; e <1; e++)
                {
                    int Eye = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisEye1>(), npc.whoAmI, 0, 2);
                    Main.npc[Eye].ai[0] = npc.whoAmI;
                    Main.npc[Eye].ai[1] = e;
                    EndurisEye1.SetPosition(Main.npc[Eye]);
                    npc.netUpdate = true;
                    spawnparts = 3;
                }                
            }
            if(spawnparts == 3)
            {
                for (int B = 0; B < 1; B++)
                {
                    int Body = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisBody>(), npc.whoAmI, 0, 2);
                    Main.npc[Body].ai[0] = npc.whoAmI;
                    Main.npc[Body].ai[1] = B;
                    EndurisBody.SetPosition(Main.npc[Body]);
                    npc.netUpdate = true;
                    spawnparts = 4;
                }
            }
            if (spawnparts == 4)
            {
                for (int N = 0; N < 3; N++)
                {
                    int Neck = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisNeck>(), npc.whoAmI, 0, 2);
                    Main.npc[Neck].ai[0] = npc.whoAmI;
                    Main.npc[Neck].ai[1] = N;
                    EndurisNeck.SetPosition(Main.npc[Neck]);
                    npc.netUpdate = true;
                    spawnparts = 5;
                }

            }
            #endregion

            Player player = Main.player[npc.target];
            npc.position = new Vector2 (player.Center.X - 270, player.Center.Y - 500);
            float speed = tvel; // pixels per second
            npc.velocity = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y - 400).SafeNormalize(Vector2.Zero) * speed;

            tvel = 0f;
            

        }
    }    
}
