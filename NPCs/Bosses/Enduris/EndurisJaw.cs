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
   public class EndurisJaw : ModNPC
    {
        #region Center
        private int center
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        #endregion

        #region AttackCool
        private float attackCool
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        #endregion
        float _blip = -1f;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 212;
            npc.height = 174;
            npc.dontTakeDamage = true;
            npc.lifeMax = 100;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        #region NPC Position prt 2: electricboogaloo
        public static void SetPosition(NPC npc)
        {
            
            EndurisJaw endurisJaw = npc.modNPC as EndurisJaw;
            if (endurisJaw != null)
            {
                Vector2 center = Main.npc[endurisJaw.center].Center;
                npc.position = center;
            }
        }
        #endregion
        int JawPosX = 212;
        int JawPosY = 180;
        int AttackPhase = 0;
        public override void AI()
        {
            #region Attack loop
            attackCool -= 1f;

            if (Main.netMode != 1 && attackCool <= 0f)
            {
                attackCool = 200f;
                AttackPhase += 1;
                npc.netUpdate = true;

            }
            if (AttackPhase > 1)
            {
                AttackPhase = 0;
            }
            #endregion  


            if (_blip == -1)
            {
                _blip = npc.ai[1] * 8;
            }
            NPC endurisHead = Main.npc[center];
            EndurisJaw endurisJaw = npc.modNPC as EndurisJaw;
            if (endurisJaw != null)
            {
                
                SetPosition(npc);
                npc.position = new Vector2(endurisHead.Center.X - JawPosX, endurisHead.Center.Y + JawPosY);
                if (AttackPhase == 0)
                {
                    if (npc.ai[0] == 0)
                    {
                        JawPosX = 212;
                        JawPosY = 180;
                    }
                    if (npc.ai[1] == 1)
                    {
                        npc.spriteDirection = 1;
                        JawPosX = -2;
                        JawPosY = 180;
                    }
                    
                }
                if (AttackPhase == 1)
                {
                    if(Main.netMode != 1 && attackCool > 190)
                    {
                       
                        if(npc.ai[0] == 0)
                        {
                            JawPosX = 212;
                            JawPosY = 180;
                        }


                        if (npc.ai[1] == 1)
                        {
                            npc.spriteDirection = 1;
                            JawPosX = -2;
                            JawPosY = 180;
                        }
                    }
                    if (attackCool < 190 && attackCool >100 && JawPosY != 230)
                    {

                        JawPosY += 1;
                        if (npc.ai[1] == 1)
                        {
                            JawPosX -= 1;
                        }
                        if (npc.ai[0] == 0)
                        {
                            JawPosX += 1;
                        }
                    }
                    if(attackCool < 100 && JawPosY <= 230 && JawPosY !=180)
                    {
                        
                        JawPosY -= 1;
                    }
                }
                
            }
        }
    }
}
