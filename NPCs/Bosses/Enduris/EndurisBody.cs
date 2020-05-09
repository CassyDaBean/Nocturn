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

        #region Color glow thingy
        public override Color? GetAlpha(Color lightColor) // color of glow
        {
            return new Color(0.7f, 0.7f, 0.7f);
        }
        #endregion

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
        float ArmRot = 0f; 
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

            ArmRot = 0.5f;
            /*#region Spawn parts
            if (SpawnParts == 1)
            {
                for (int k = 0; k < 2; k++)
                {
                    int Armcount = 2;
                    int Arm = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisArm2>(), npc.whoAmI, 0, Armcount);
                    Main.npc[Arm].ai[0] = npc.whoAmI;
                    Main.npc[Arm].ai[1] = k;
                    EndurisArm2.SetPosition(Main.npc[Arm]);

                    SpawnParts = 2;
                    npc.netUpdate = true;
                }
                
            }
            if(SpawnParts == 2)
            {
                for (int k = 0; k < 2; k++)
                {
                    int Armcount = 2;
                    int Arm = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisArm1>(), npc.whoAmI, 0, Armcount);
                    Main.npc[Arm].ai[0] = npc.whoAmI;
                    Main.npc[Arm].ai[1] = k;
                    EndurisArm1.SetPosition(Main.npc[Arm]);

                    SpawnParts = 3;
                    npc.netUpdate = true;
                }

            }
            if (SpawnParts == 3)
            {
                for (int k = 0; k < 2; k++)
                {
                    int Pipecount = 2;
                    int Pipe = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisPipe>(), npc.whoAmI, 0, Pipecount);
                    Main.npc[Pipe].ai[0] = npc.whoAmI;
                    Main.npc[Pipe].ai[1] = k;
                    EndurisPipe.SetPosition(Main.npc[Pipe]);

                    SpawnParts = 4;
                    npc.netUpdate = true;
                }

            }
            if (SpawnParts == 0)
            {
                for (int k = 0; k < 2; k++)
                {
                    int Handcount = 2;
                    int Hand = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<EndurisHand>(), npc.whoAmI, 0, Handcount);
                    Main.npc[Hand].ai[0] = npc.whoAmI;
                    Main.npc[Hand].ai[1] = k;
                    EndurisHand.SetPosition(Main.npc[Hand]);

                    SpawnParts = 1;
                    npc.netUpdate = true;
                }
                

            }
            #endregion*/
        }

        #endregion
        public Arm1[] arm1s = null;

        public class Arm1info
        {
            public int ArmT = 0;
            public Vector2 position, oldPosition;
        }
        public class Arm1 : Arm1info
        {

        }
        public void DrawLimbs(SpriteBatch spriteBatch, Color drawColor)
        {
            drawColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            
            
            
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color DrawColor)
        {
            Texture2D Arm2 = mod.GetTexture("NPCs/Bosses/Enduris/EndurisArm2");
            Texture2D Arm1 = mod.GetTexture("NPCs/Bosses/Enduris/EndurisArm1");
            Vector2 Arm1Pos1 = new Vector2(npc.Center.X - 300, npc.Center.Y);
            Vector2 Arm1Pos2 = new Vector2(npc.Center.X +300, npc.Center.Y);
            Rectangle Arm = new Rectangle(0, 0, 260, 592);
            Vector2 Arm2Pos1 = new Vector2(Arm1Pos1.X, Arm1Pos1.Y + 300);

            Rectangle ForArm = new Rectangle(0, 0, 150, 800);

            for (int k = 0; k < 2; k++)
            {
                spriteBatch.Draw(Arm1, Arm1Pos1 - Main.screenPosition, Arm, DrawColor, ArmRot, new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Arm1, Arm1Pos2 - Main.screenPosition, Arm, DrawColor, ArmRot, new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);

                spriteBatch.Draw(Arm2, Arm2Pos1 - Main.screenPosition, ForArm, DrawColor, ArmRot, new Vector2(ForArm.Width * 0.5f, ForArm.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                //spriteBatch.Draw(Arm2, Arm1Pos2 - Main.screenPosition, Arm, DrawColor, ArmRot, new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);

                SpawnParts = 1;
                npc.netUpdate = true;
            }

            

            
            spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Enduris/EndurisBody"), npc.Center - Main.screenPosition, npc.frame, DrawColor, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}
