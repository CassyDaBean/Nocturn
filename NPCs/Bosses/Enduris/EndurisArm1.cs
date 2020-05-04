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
   public class EndurisArm1 : ModNPC
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
            npc.width = 210;
            npc.height = 374;
            npc.dontTakeDamage = true;
            npc.lifeMax = 100;
            npc.noGravity = true;
            npc.behindTiles = true;
            npc.noTileCollide = true;
        }

        #region NPC Position prt 2: electricboogaloo
        public static void SetPosition(NPC npc)
        {
            
            EndurisArm1 endurisArm1 = npc.modNPC as EndurisArm1;
            if (endurisArm1 != null)
            {
                Vector2 center = Main.npc[endurisArm1.center].Center;
                npc.position = center;
            }
        }
        #endregion
        int ArmPosX = 212;
        int ArmPosY = 180;
        float ArmRot = 0f;
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
            EndurisArm1 endurisArm1 = npc.modNPC as EndurisArm1;
            if (endurisArm1 != null)
            {
                
                SetPosition(npc);
                npc.position = new Vector2(endurisHead.Center.X - ArmPosX, endurisHead.Center.Y + ArmPosY);
                npc.rotation = ArmRot;
                if (AttackPhase == 0 )
                {
                    if (npc.ai[1] == 0 && ArmPosX != 440)
                    {
                        ArmRot -= 0.01f;
                        ArmPosX -= 1;
                        ArmPosY = -84;
                    }
                    if (npc.ai[1] == 1 && ArmPosX != -234)
                    {
                        npc.spriteDirection = 1;
                        ArmPosX += 1;
                        ArmPosY = -84;
                        ArmRot += 0.01f;
                    }
                    
                }
                if (AttackPhase == 1)
                {
                    if(Main.netMode != 1 && attackCool > 150)
                    {
                       
                        if(npc.ai[1] == 0)
                        {
                            ArmPosX = 440;
                            ArmPosY = -84;
                            ArmRot = 0f;
                        }


                        if (npc.ai[1] == 1)
                        {
                            npc.spriteDirection = 1;
                            ArmPosX = -234;
                            ArmPosY = -84;
                            ArmRot = 0f;
                        }
                    }
                    if (attackCool < 150 && attackCool >90 && ArmPosY != 230)
                    {

                        ArmPosY = -88 ;
                        if (npc.ai[1] == 0)
                        {
                            ArmPosX += 1;
                            ArmRot += 0.01f;
                        }


                        if (npc.ai[1] == 1)
                        {
                            npc.spriteDirection = 1;
                            ArmPosX -= 1;
                            ArmRot -= 0.01f;
                            
                        }


                    }
                    if(attackCool < 10 && ArmPosY <= 230 && ArmPosY !=180)
                    {
                        
                        //ArmPosY -= 1;
                    }
                }
                
            }
        }
    }
}
