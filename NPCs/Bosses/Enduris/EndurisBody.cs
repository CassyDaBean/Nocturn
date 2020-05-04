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
    public class EndurisBody : ModNPC
    {
        private int center // Center
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }

        Byte Position = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enduris Body");
        }
        public override void SetDefaults()
        {

            npc.width = 706;
            npc.height = 370;
            npc.dontTakeDamage = true;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
        }

        #region NPC Position prt 2: electricboogaloo
        public static void SetPosition(NPC npc)
        {

            EndurisBody endurisBody = npc.modNPC as EndurisBody;
            if (endurisBody != null)
            {
                Vector2 center = Main.npc[endurisBody.center].Center;
                npc.position = center;
            }
        }
        #endregion

        #region AI
        int BodyPosX = 0;
        int BodyPosY = 0;
        Byte SpawnParts = 0;
        public override void AI()
        {
            #region Positioning
            NPC endurisHead = Main.npc[center];
            EndurisBody endurisBody = npc.modNPC as EndurisBody;
            if (endurisBody != null)
            {
                SetPosition(npc);
                npc.position = new Vector2(endurisHead.Center.X - BodyPosX, endurisHead.Center.Y - BodyPosY);
                if (Position == 0)
                {
                    if (npc.ai[1] == 0)
                    {
                        BodyPosX = 352;
                        BodyPosY = -300;
                        npc.scale = 1;
                    }
                }

            }

            #endregion


            #region Spawn parts
            if (SpawnParts == 0)
            {
                for (int k = 0; k < 2; k++)
                {
                    int Armcount = 2;
                    int Arm = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisArm1>(), npc.whoAmI, 0, Armcount);
                    Main.npc[Arm].ai[0] = npc.whoAmI;
                    Main.npc[Arm].ai[1] = k;
                    EndurisArm1.SetPosition(Main.npc[Arm]);

                    SpawnParts = 1;
                    npc.netUpdate = true;
                }
            }
            #endregion
        }

        #endregion
    }
}
