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

            npc.width = 686;
            npc.height = 362;
            npc.dontTakeDamage = true;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
        }
        private static float X(float t,
    float x0, float x1, float x2, float x3)
        {
            return (float)(
                x0 * Math.Pow((1 - t), 3) +
                x1 * 3 * t * Math.Pow((1 - t), 2) +
                x2 * 3 * Math.Pow(t, 2) * (1 - t) +
                x3 * Math.Pow(t, 3)
            );
        }
        private static float Y(float t,
            float y0, float y1, float y2, float y3)
        {
            return (float)(
                 y0 * Math.Pow((1 - t), 3) +
                 y1 * 3 * t * Math.Pow((1 - t), 2) +
                 y2 * 3 * Math.Pow(t, 2) * (1 - t) +
                 y3 * Math.Pow(t, 3)
             );
        }
        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor, Vector2 ifYouReallyWantToo)
        {
            if (head != null && head.active && head.modNPC != null && head.modNPC is EndurisBody)
            {
                string neckTex = "NPCs/Bosses/Enduris/EndurisLeg";
                Texture2D neckTex2D = mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y);
                Vector2 connector = ifYouReallyWantToo;
                float chainsPerUse = 0.015f;
                for (float i = 0; i <= 1; i += chainsPerUse)
                {
                    Vector2 distBetween;
                    float projTrueRotation;
                    if (i != 0)
                    {
                        distBetween = new Vector2(X(i, neckOrigin.X, (connector.X - neckOrigin.X)*0.33f + neckOrigin.X, (connector.X - neckOrigin.X)*0.86f + neckOrigin.X, connector.X) -
                        X(i - chainsPerUse, neckOrigin.X, (connector.X - neckOrigin.X) / 3 + neckOrigin.X, (connector.X - neckOrigin.X) * 0.86f + neckOrigin.X, connector.X),
                        Y(i, neckOrigin.Y, (neckOrigin.Y + connector.Y)/2 +300, (neckOrigin.Y + connector.Y) / 2 -300, connector.Y) -
                        Y(i - chainsPerUse,neckOrigin.Y, (neckOrigin.Y + 300), (neckOrigin.Y - 300), connector.Y));
                        projTrueRotation = distBetween.ToRotation() - (float)Math.PI / 2;
                        spriteBatch.Draw(neckTex2D, new Vector2(X(i, neckOrigin.X, (connector.X - neckOrigin.X) / 3 + neckOrigin.X, (connector.X - neckOrigin.X) * 0.86f + neckOrigin.X, connector.X) - Main.screenPosition.X, Y(i, neckOrigin.Y, (neckOrigin.Y + connector.Y) / 2 + 300, (neckOrigin.Y + connector.Y) / 2 - 300, connector.Y) - Main.screenPosition.Y),
                        new Rectangle(0, 0, neckTex2D.Width, neckTex2D.Height), drawColor, projTrueRotation,
                        new Vector2(neckTex2D.Width * 0.5f, neckTex2D.Height * 0.5f), 0.7f + i, SpriteEffects.None, 0f);
                    }
                }
              //  spriteBatch.Draw(neckTex2D, new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                //spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }
        #region Color glow thingy
        public override Color? GetAlpha(Color lightColor) // color of glow
        {
            return new Color(1f, 1f, 1f);
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
        //Wall of Ints
        int BodyPosX = 0;
        int BodyPosY = 0;
        Byte SpawnParts = 0;
        Byte AttackPhase = 0;
        //Set in arrays of size 2. One for the left hand, and one for the right hand.
        //The following variables are just the variables that hold the arms together. You wont need to worry about these when creating attacks
        float[] ArmRot = new float[2];
        float[] ArmRot2 = new float[2];
        float[] jointPosX = new float[2];
        float[] jointPosY = new float[2];
        Vector2[] realJointPos = new Vector2[2];
        Vector2[] realHandPos = new Vector2[2];
        Vector2[] legPositions = new Vector2[3];
        bool[] onGround = new bool[3];
        int[] getTheFuckUp = new int[3];
        float highest;
        bool isnpcNearGround;
        int noOfTiles;
        int Hand1MotionX = 350;
        int Hand1MotionY = 0;
        int Hand2MotionX = 350;
        int Hand2MotionY = 0;
        Byte HandFrame = 0;
        int AttackSubset = 0;
        // Whatever floats ya boat
        float Hand1Rot = 0f;
        float Hand2Rot = 0f;
        float FR1 = 0f;
        float FR2 = 0f;
        float attackCool;
        public override void AI()
        {
            //initialising the ongrounds
            onGround[2] = false;
            onGround[1] = false;
            onGround[0] = false;
            #region 2ndPhaseConditions
            //checking for the highest leg
            for (int i = 0; i<3; i++)
            {
                highest = legPositions[0].Y;
                if(i>0)
                {
                    if (legPositions[i].Y < highest)
                        highest = legPositions[i].Y;   
                }
            }
            //checking to see if the distance between the highest leg and the actually body is too large to trigger second phase
            if(highest - npc.Center.Y > 900) //should be 520
            {
                //npc.ai[2] 0 = leg phase
                //npc.ai[2] 1 = fly phase
                npc.ai[2] = 1;
            }
            if(npc.ai[2] != 0)
            {
                
                npc.ai[2]++;
              for(int i = 0; i<3; i++)
                {
                    //retracts all legs if in this phase
                    legPositions[i] += (npc.Center - legPositions[i]) / 16;
                }
                if (npc.ai[2] > 150)
                {
                    if(isnpcNearGround && noOfTiles >24)
                    {
                        //comes back to the new phase after countdown and if it is near the ground
                        npc.ai[2] = 0;
                    }
                }
            }
            #endregion
            #region TileCheck1
            for (int s = 0; s < 3; s++)
            {
                int minTilePosX = (int)(legPositions[s].X / 16.0) - 5;
                int maxTilePosX = (int)((legPositions[s].X + 98) / 16.0) + 5;
                int minTilePosY = (int)(legPositions[s].Y / 16.0) - 5;
                int maxTilePosY = (int)((legPositions[s].Y + 46) / 16.0);
                if (minTilePosX < 0)
                {
                    minTilePosX = 0;
                }

                if (maxTilePosX > Main.maxTilesX)
                {
                    maxTilePosX = Main.maxTilesX;
                }

                if (minTilePosY < 0)
                {
                    minTilePosY = 0;
                }

                if (maxTilePosY > Main.maxTilesY)
                {
                    maxTilePosY = Main.maxTilesY;
                }
                for (int i = minTilePosX + 5; i < maxTilePosX - 5; ++i)
                {
                    for (int j = minTilePosY + 5; j < maxTilePosY + 5; ++j)
                    {
                        if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
                        {
                            Vector2 vector2;
                            vector2.X = (float)(i * 16);
                            vector2.Y = (float)(j * 16);

                            if (Math.Abs(legPositions[s].Y - vector2.Y) <= 5)
                            {
                                onGround[s] = true;
                            }
                        }
                    }
                }
            }
            #endregion
            #region TileCheck2
            isnpcNearGround = false;
                int minTilePosX2 = (int)(npc.Center.X / 16.0) - 3;
                int maxTilePosX2 = (int)(npc.Center.X / 16.0) + 3;
                int minTilePosY2 = (int)(npc.Center.Y / 16.0);
                int maxTilePosY2 = (int)(npc.Center.Y / 16.0)+500;
            noOfTiles = 0;
                if (minTilePosX2 < 0)
                {
                minTilePosX2 = 0;
                }

                if (maxTilePosX2 > Main.maxTilesX)
                {
                maxTilePosX2 = Main.maxTilesX;
                }

                if (minTilePosY2 < 0)
                {
                minTilePosY2 = 0;
                }

                if (maxTilePosY2 > Main.maxTilesY)
                {
                maxTilePosY2 = Main.maxTilesY;
                }
                for (int i = minTilePosX2; i < maxTilePosX2 ; ++i)
                {
                    for (int j = minTilePosY2; j < maxTilePosY2; ++j)
                    {
                        if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
                        {
                            Vector2 vector2;
                            vector2.X = (float)(i * 16);
                            vector2.Y = (float)(j * 16);
                            
                            if (Math.Abs(npc.Center.Y - vector2.Y) <= 500)
                            {
                                isnpcNearGround = true;
                            noOfTiles++;
                            }
                            
                        }
                    }
                }
            #endregion
            #region LegMovement
            npc.ai[3]++;
            if (npc.ai[2] == 0)
            {
                if (npc.ai[3] == 1)
                {
                    //initialising leg positions
                    legPositions[0] = (npc.Center + new Vector2(-400, 400));
                    legPositions[1] = (npc.Center + new Vector2(0, 400));
                    legPositions[2] = (npc.Center + new Vector2(400, 400));
                }
                for (int i = 0; i < 3; i++)
                {

                    if (npc.ai[3] % (60+((i-1)*40)) == 0 && Math.Abs(legPositions[i].X - (npc.Center.X + ((i - 1) * 400))) > 160 + ((i - 1) * 30) && getTheFuckUp[i] == 0)
                    {
                        getTheFuckUp[i] = 1;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (!onGround[i])
                        legPositions[i].Y += 13;
                }
                for (int i = 0; i < 3; i++)
                {
                    if (getTheFuckUp[i] != 0)
                    {
                        getTheFuckUp[i]++;
                        legPositions[i].Y -= (float)(1 / (float)getTheFuckUp[i]) * 200;
                        legPositions[i].X += ((npc.Center.X + ((i - 1) * 400)) - (legPositions[i].X)) / 9;
                        if (getTheFuckUp[i] > 30 && onGround[i])
                            getTheFuckUp[i] = 0;
                        if (getTheFuckUp[i] == 100)
                            getTheFuckUp[i] = 0;
                    }
                }
            }
            #endregion
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
                AttackSubset += 1;
            }
            if (AttackSubset >= 3)
            {
                AttackSubset = 0;
            }
           

            if (AttackPhase == 0)
            {
                //Attack Right hand swipe (Basic)
                if (AttackSubset == 2)
                {
                    Hand1Rot = 0f;
                    HandFrame = 0;
                    Hand1MotionX = 350;
                    Hand1MotionY = 0;

                    if (attackCool > 180)
                    {
                        Hand2MotionX += 20;
                        Hand2MotionY -= 5;
                        HandFrame = 2;
                        Hand2Rot += 0.06f;
                    }
                    if (attackCool > 100 && attackCool < 180)
                    {
                        Hand2MotionX -= 12;
                        Hand2MotionY = 0;
                        HandFrame = 1;
                        Hand2Rot -= 0.03f;
                    }

                    if (attackCool > 0 && attackCool < 100)
                    {
                        Hand2MotionX += 6;
                        Hand2MotionY = 0;
                        HandFrame = 2;
                        Hand2Rot += 0.01f;

                    }
                }

                //Attack Both hand swipe (Basic)
                if (AttackSubset == 1)
                {
                    if (attackCool > 190)
                    {
                        HandFrame = 0;
                        Hand1MotionX = 350;
                        Hand1MotionY = 0;
                        Hand1Rot = 0f;
                        Hand2MotionX = 350;
                        Hand2MotionY = 0;
                        Hand2Rot = 0f;
                    }
                    if (attackCool > 180 && attackCool < 190)
                    {
                        HandFrame = 2;
                        Hand1MotionX += 10;
                        Hand1MotionY -= 3;
                        Hand1Rot -= 0.06f;
                        Hand2MotionX += 10;
                        Hand2MotionY -= 3;
                        Hand2Rot += 0.06f;
                    }
                    if (attackCool > 100 && attackCool < 180)
                    {
                        HandFrame = 1;
                        Hand1MotionX -= 10;
                        Hand1MotionY = 0;
                        Hand1Rot += 0.02f;
                        Hand2MotionX -= 10;
                        Hand2MotionY = 0;
                        Hand2Rot -= 0.02f;
                    }
                    if (attackCool > 2 && attackCool < 100)
                    {
                        HandFrame = 2;
                        Hand1MotionX += 7;
                        Hand1MotionY = 0;
                        Hand1Rot -= 0.01f;
                        Hand2MotionX += 7;
                        Hand2MotionY = 0;
                        Hand2Rot += 0.01f;
                    }
                    if (attackCool > 0 && attackCool < 2)
                    {
                        HandFrame = 0;
                        Hand1MotionX = 350;
                        Hand1MotionY = 0;
                        Hand1Rot = 0f;
                        Hand2MotionX = 350;
                        Hand2MotionY = 0;
                        Hand2Rot = 0f;
                    }

                }

                //Attack Slam (Basic)

            }
            if (AttackPhase == 1)
            {
                Hand2Rot = 0f;
                HandFrame = 0;
                Hand2MotionX = 350;
                Hand2MotionY = 0;
                if (attackCool > 180)
                {
                    Hand1Rot -= 0.06f;
                    Hand1MotionX += 20;
                    Hand1MotionY -= 5;
                    HandFrame = 2;
                }
                if (attackCool > 100 && attackCool < 180)
                {
                    Hand1MotionX -= 12;
                    Hand1MotionY = 0;
                    HandFrame = 1;
                    Hand1Rot += 0.03f;

                }
                if (attackCool > 2 && attackCool < 100)
                {
                    Hand1MotionX += 5;
                    Hand1MotionY = 0;
                    HandFrame = 2;
                    Hand1Rot -= 0.01f;
                }

            }
            #endregion
            #endregion
            #region Maths


            //What you do have to worry about, is the following. 
            //The first (HandPosition) is for the left hand, the second is for the right (HandPosition2), and it basically tells then hands where to go.
            //Change these for your own specific hand movement. For now, the left hand always follows the mouse, and the right one, the first player.
            Vector2 HandPosition = new Vector2(Main.player[0].position.X - Hand1MotionX, Main.player[0].position.Y - Hand1MotionY);
            Vector2 HandPosition2 = new Vector2(Main.player[0].position.X + Hand2MotionX, Main.player[0].position.Y - Hand2MotionY);
            //The following is the maths. Dont worry about this
            float addon = HandPosition.Y - npc.Center.Y;
            float addon2 = HandPosition2.Y - npc.Center.Y;
           
            if (addon > 0)
                addon = 0;
            if (addon2 > 0)
                addon2 = 0;
            jointPosX[0] = (npc.Center.X -400 + HandPosition.X) / 2;
            jointPosX[1] = (npc.Center.X +400 + HandPosition2.X) / 2;
            double indicator = Math.Pow(400, 2) - Math.Pow(Math.Abs(jointPosX[0] - (npc.Center.X-400)), 2);
            double indicator2 = Math.Pow(400, 2) - Math.Pow(Math.Abs(jointPosX[1] - (npc.Center.X + 400)), 2);
            if (indicator>0)
            jointPosY[0] = npc.Center.Y + (float)Math.Sqrt(indicator) + (addon * 1.5f);
            if (indicator2 > 0)
            jointPosY[1] = npc.Center.Y + (float)Math.Sqrt(indicator2) + (addon2 * 1.5f);
            ArmRot[0] = new Vector2(npc.Center.X - jointPosX[0], npc.Center.Y - jointPosY[0]).ToRotation() + (float)Math.PI / 4;
            ArmRot[1] = new Vector2(npc.Center.X - jointPosX[1], npc.Center.Y - jointPosY[1]).ToRotation() + (3*(float)Math.PI / 4);
            realJointPos[0] = new Vector2(npc.Center.X - 300, npc.Center.Y) + new Vector2(-296 * (float)Math.Sin(ArmRot[0]), 296 * (float)Math.Cos(ArmRot[0]));
            realJointPos[1] = new Vector2(npc.Center.X + 300, npc.Center.Y) + new Vector2(-296 * (float)Math.Sin(ArmRot[1]), 296 * (float)Math.Cos(ArmRot[1]));
            ArmRot2[0] = new Vector2(realJointPos[0].X - HandPosition.X, realJointPos[0].Y - HandPosition.Y).ToRotation() + (float)Math.PI/2;
            ArmRot2[1] = new Vector2(realJointPos[1].X - HandPosition2.X, realJointPos[1].Y - HandPosition2.Y).ToRotation() + (float)Math.PI / 2;
            realHandPos[0] = new Vector2(realJointPos[0].X, realJointPos[0].Y) + new Vector2(-360 * (float)Math.Sin(ArmRot2[0]), 360 * (float)Math.Cos(ArmRot2[0]));
            realHandPos[1] = new Vector2(realJointPos[1].X, realJointPos[1].Y) + new Vector2(-360 * (float)Math.Sin(ArmRot2[1]), 360 * (float)Math.Cos(ArmRot2[1]));
            #endregion

            
        }

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
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            DrawColor = npc.GetAlpha(DrawColor);
            DrawHead(spriteBatch, "NPCs/Bosses/Enduris/EndurisLeg", "NPCs/Bosses/Enduris/EndurisLeg", npc, DrawColor, legPositions[0]);
            DrawHead(spriteBatch, "NPCs/Bosses/Enduris/EndurisLeg", "NPCs/Bosses/Enduris/EndurisLeg", npc, DrawColor, legPositions[1]);
            DrawHead(spriteBatch, "NPCs/Bosses/Enduris/EndurisLeg", "NPCs/Bosses/Enduris/EndurisLeg", npc, DrawColor, legPositions[2]);
            Texture2D Arm2 = mod.GetTexture("NPCs/Bosses/Enduris/EndurisArm2");
            Texture2D Arm1 = mod.GetTexture("NPCs/Bosses/Enduris/EndurisArm1");
            Texture2D Hand = mod.GetTexture("NPCs/Bosses/Enduris/EndurisHand");
            Texture2D HandS = mod.GetTexture("NPCs/Bosses/Enduris/EndurisHandSwipe");
            Texture2D HandR = mod.GetTexture("NPCs/Bosses/Enduris/EndurisHandRetract");

            Vector2 Arm1Pos1 = new Vector2(npc.Center.X - 300, npc.Center.Y);
            Vector2 Arm1Pos2 = new Vector2(npc.Center.X +300, npc.Center.Y);
            
            Rectangle Arm = new Rectangle(0, 0, 260, 592);
            Vector2 Arm2Pos1 = new Vector2(Arm1Pos1.X, Arm1Pos1.Y + 300);

            Rectangle ForArm = new Rectangle(0, 0, 204, 800);
            Rectangle HandRect = new Rectangle(0, 0, 170, 600);
            for (int k = 0; k < 2; k++)
            {
                spriteBatch.Draw(Arm1, Arm1Pos1 - Main.screenPosition, Arm, DrawColor, ArmRot[0], new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Arm1, Arm1Pos2 - Main.screenPosition, Arm, DrawColor, ArmRot[1], new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);

                //spriteBatch.Draw(Arm2, Arm1Pos2 - Main.screenPosition, Arm, DrawColor, ArmRot, new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);


                npc.netUpdate = true;
            }
            spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Enduris/EndurisBody"), npc.Center - Main.screenPosition, npc.frame, DrawColor, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Arm2, realJointPos[0] - Main.screenPosition, ForArm, DrawColor, ArmRot2[0], new Vector2(ForArm.Width * 0.5f, ForArm.Height * 0.5f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Arm2, realJointPos[1] - Main.screenPosition, ForArm, DrawColor, ArmRot2[1], new Vector2(ForArm.Width * 0.5f, ForArm.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);
            if (HandFrame == 0)
            {
                spriteBatch.Draw(Hand, realHandPos[0] - Main.screenPosition, HandRect, DrawColor, ArmRot2[0] + (float)Math.PI + Hand1Rot, new Vector2(HandRect.Width * 0.5f, HandRect.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Hand, realHandPos[1] - Main.screenPosition, HandRect, DrawColor, ArmRot2[1] + (float)Math.PI + Hand2Rot, new Vector2(HandRect.Width * 0.5f, HandRect.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            if (HandFrame == 1)
            {
                spriteBatch.Draw(HandS, realHandPos[0] - Main.screenPosition, HandRect, DrawColor, ArmRot2[0] + (float)Math.PI + Hand1Rot, new Vector2(HandRect.Width * 0.5f, HandRect.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(HandS, realHandPos[1] - Main.screenPosition, HandRect, DrawColor, ArmRot2[1] + (float)Math.PI + Hand2Rot, new Vector2(HandRect.Width * 0.5f, HandRect.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            if (HandFrame == 2)
            {
                spriteBatch.Draw(HandR, realHandPos[0] - Main.screenPosition, HandRect, DrawColor, ArmRot2[0] + (float)Math.PI + Hand1Rot, new Vector2(HandRect.Width * 0.5f, HandRect.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(HandR, realHandPos[1] - Main.screenPosition, HandRect, DrawColor, ArmRot2[1] + (float)Math.PI + Hand2Rot, new Vector2(HandRect.Width * 0.5f, HandRect.Height * 0.5f), 1f, SpriteEffects.FlipHorizontally, 0f);
            }

            return false;
        }
    }
}
