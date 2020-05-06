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
    public class EndurisNeck : ModNPC
    {
        private int center // Center
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }

        Byte Position = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enduris Neck");
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
            npc.behindTiles = false;
        }
        #region Color glow thingy
        public override Color? GetAlpha(Color lightColor) // color of glow
        {
            return new Color(0.7f, 0.7f, 0.7f,1f);
        }
        #endregion
        #region NPC Position prt 2: electricboogaloo
        public static void SetPosition(NPC npc)
        {

            EndurisNeck endurisNeck = npc.modNPC as EndurisNeck;
            if (endurisNeck != null)
            {
                Vector2 center = Main.npc[endurisNeck.center].Center;
                npc.position = center;
            }
        }
        #endregion

        #region AI
        int NeckPosX = 0;
        int NeckPosY = 0;
        public override void AI()
        {
            #region Positioning
            NPC endurisHead = Main.npc[center];
            EndurisNeck endurisNeck = npc.modNPC as EndurisNeck;
            if (endurisNeck != null)
            {
                SetPosition(npc);
                npc.position = new Vector2(endurisHead.Center.X - NeckPosX, endurisHead.Center.Y - NeckPosY);
                if (Position == 0)
                {
                    if (npc.ai[1] == 0)
                    {
                        NeckPosX = 352;
                        NeckPosY = 110;
                        
                        npc.scale = 1;
                    }
                    if (npc.ai[1] == 1)
                    {
                        NeckPosX = 352;
                        NeckPosY = 64;
                        npc.scale = 1;
                    }
                    if (npc.ai[1] == 2)
                    {
                        NeckPosX = 352;
                        NeckPosY = 16;
                        npc.scale = 1;
                    }
                }

            }

            #endregion
        }

        #endregion
    }
}