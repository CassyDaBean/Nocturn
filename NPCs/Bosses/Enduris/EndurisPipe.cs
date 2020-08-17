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
using Terraria.Graphics.Shaders;
namespace Nocturn.NPCs.Bosses.Enduris
{
   public class EndurisPipe : ModNPC
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
        


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enduris Pipe");
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 260;
            npc.height = 592;
            npc.dontTakeDamage = true;
            npc.lifeMax = 100;
            npc.noGravity = true;
            npc.behindTiles = true;
            npc.noTileCollide = true;
        }

        #region NPC Position prt 2: electricboogaloo
        public static void SetPosition(NPC npc)
        {
            
            EndurisPipe endurisArm1 = npc.modNPC as EndurisPipe;
            if (endurisArm1 != null)
            {
                Vector2 center = Main.npc[endurisArm1.center].Center;
                npc.position = center;
            }
        }
        #endregion

        #region Color glow thingy
        public override Color? GetAlpha(Color lightColor) // color of glow
        {
            return new Color(0.7f, 0.7f, 0.7f, 1f);
        }
        #endregion


        int ArmPosX = 440;
        int ArmPosY = -700;
        float ArmRot = 0f;
        int AttackPhase = 0;
        Byte Spawn = 0;
        Vector2 PartPos;
        public override void AI()
        {
            #region Attack loop
            attackCool -= 1f;

            if (Main.netMode != 1 && attackCool <= 0f)
            {
                attackCool = 300f;
                AttackPhase += 1;
                npc.netUpdate = true;

            }
            if (AttackPhase > 1)
            {
                AttackPhase = 0;
            }
            #endregion  


            #region Movment
            NPC endurisHead = Main.npc[center];
            EndurisPipe endurisArm1 = npc.modNPC as EndurisPipe;
            if (endurisArm1 != null)
            {

                SetPosition(npc);
                npc.position = new Vector2(endurisHead.Center.X - ArmPosX, endurisHead.Center.Y + ArmPosY);
                npc.rotation = ArmRot;
                if (AttackPhase <= 0)
                {
                    if (npc.ai[1] == 0)
                    {

                        if (Main.rand.NextFloat() < 0.2f)
                        {
                            Dust dust;
                            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                            PartPos = new Vector2(npc.Center.X - 10, npc.Center.Y + 15);
                            dust = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                            dust.noLight = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                            dust.noGravity = true;
                        }
                    }


                    if (npc.ai[1] == 1)
                    {

                        if (Main.rand.NextFloat() < 0.2f)
                        {
                            Dust dust2;
                            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                            PartPos = new Vector2(npc.Center.X, npc.Center.Y + 15);
                            dust2 = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                            dust2.noLight = true;
                            dust2.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                            dust2.fadeIn = 3f;
                            dust2.noGravity = true;
                        }
                    }

                }
                if (AttackPhase == 1)
                {
                    if (Main.netMode !=1 && attackCool > 190)
                    {

                        if (npc.ai[1] == 0)
                        {
                            ArmPosX = 440;
                            ArmPosY = -700;
                            ArmRot = 0f;
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Dust dust;
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                PartPos = new Vector2(npc.Center.X + 76, npc.Center.Y + 86);
                                dust = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                                dust.noLight = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                                dust.fadeIn = 3f;
                                dust.noGravity = true;
                            }
                        }
                        if (npc.ai[1] == 1)
                        {
                            npc.spriteDirection = 1;
                            ArmPosX = -180;
                            ArmPosY = -700;
                            ArmRot = 0f;
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Dust dust2;
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                PartPos = new Vector2(npc.Center.X - 86, npc.Center.Y + 86);
                                dust2 = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                                dust2.noLight = true;
                                dust2.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                                dust2.fadeIn = 3f;
                                dust2.noGravity = true;
                            }

                        }
                        
                            /*Dust dust;
                            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                            Vector2 position = Main.LocalPlayer.Center;
                            dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 240, 0f, -10f, 221, new Color(255, 0, 50), 1.710526f)];
                            dust.noLight = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                            dust.fadeIn = 3f;*/
                        

                    }
                    if (attackCool <190 && attackCool > 150)
                    {

                        if (npc.ai[1] == 0)
                        {
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Dust dust;
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                PartPos = new Vector2(npc.Center.X - 80, npc.Center.Y + 28);
                                dust = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                                dust.noLight = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                                dust.fadeIn = 3f;
                                dust.noGravity = true;
                            }
                        }
                        if (npc.ai[1] == 1)
                        {
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Dust dust2;
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                PartPos = new Vector2(npc.Center.X + 70, npc.Center.Y + 28);
                                dust2 = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                                dust2.noLight = true;
                                dust2.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                                dust2.fadeIn = 3f;
                                dust2.noGravity = true;
                            }
                        }


                    }
                    if (attackCool < 150 )
                    {

                        if (npc.ai[1] == 0)
                        {
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Dust dust;
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                PartPos = new Vector2(npc.Center.X - 10, npc.Center.Y + 15);
                                dust = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                                dust.noLight = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                                dust.fadeIn = 3f;
                                dust.noGravity = true;
                            }
                        }


                        if (npc.ai[1] == 1)
                        {
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Dust dust2;
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                PartPos = new Vector2(npc.Center.X, npc.Center.Y + 15);
                                dust2 = Main.dust[Terraria.Dust.NewDust(PartPos, 0, 0, 240, 0f, -20f, 221, new Color(255, 0, 0), 0.8f)];
                                dust2.noLight = true;
                                dust2.shader = GameShaders.Armor.GetSecondaryShader(42, Main.LocalPlayer);
                                dust2.fadeIn = 3f;
                                dust2.noGravity = true;
                            }
                        }

                    }

                }
            }
            #endregion


        }
    }
}
