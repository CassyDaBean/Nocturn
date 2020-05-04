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
    public class EndurisEye1 : ModNPC
    {
        public int Animation = 0;

        private int center
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }

        #region attack cool
        private float attackCool
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }        
        #endregion

        int positionEye = 0;

        public float Color = 0f;
        public float Alpha = 0f;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enduris Eye");
            Main.npcFrameCount[npc.type] = 9;
        }
        public override void SetDefaults()
        {
            positionEye = 0;
            Animation = 0;
            npc.width = 150;
            npc.height = 146;
            npc.dontTakeDamage = true;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }
        #region frames
        public override void FindFrame(int frameHight)
        {

            if (npc.frameCounter > 4) //Failsafe
            {
                //npc.frame.X = 0;
                //npc.frame.Width = npc.width;
                npc.frameCounter = 0;

                npc.frame.Y += 146;
                if (npc.frame.Y >= frameHight * Main.npcFrameCount[npc.type]) npc.frame.Y = 0;
            }
            //npc.frame.Y = 0;
            frameHight = 146;
            npc.frame.Height = 146;
            npc.frameCounter++;
            
            if (Animation == 0)
            {

                if (npc.frame.Y > 146 * 8)
                {
                    npc.frame.Y = 0;
                }
            }
            if (Animation == 1)
            {
                if (npc.frame.Y > 146 * 5)
                {
                    npc.frame.Y = 0;
                }
            }
        }
        #endregion

        #region PreDraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            drawColor = new Color(Color, Color, Color, Alpha);
            Texture2D CurrentAttack = null;
            Texture2D Idle = Main.npcTexture[npc.type];
            Texture2D eye1 = mod.GetTexture("NPCs/Bosses/Enduris/EndurisEye1");
            Texture2D eye2 = mod.GetTexture("NPCs/Bosses/Enduris/EndurisEye2");
            

            if (Animation == 0)
            {
                Color = 0.7f;
                Alpha = 1f;
                CurrentAttack = eye1;
            }
            if (Animation == 1)
            {
                Color = 0.7f;
                Alpha = 1f;
                CurrentAttack = eye2;
            }
           
            //var FlipthySprite = npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(CurrentAttack, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1.2f, SpriteEffects.None, 0f);

            return false;
        }
        #endregion

        #region NPC Position prt 2: electricboogaloo
        public static void SetPosition(NPC npc)
        {

            EndurisEye1 endurisEye = npc.modNPC as EndurisEye1;
            if (endurisEye != null)
            {
                Vector2 center = Main.npc[endurisEye.center].Center;
                npc.position = center;
            }
        }
        #endregion
        int AttackPhase = 0;
        int EyeposX = 140;
        int EyeposY = 100;
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

            if (AttackPhase == 0)
            {
                Animation = 0;
            }
            if (AttackPhase == 1)
            {
                Animation = 1;
            }

            NPC endurisHead = Main.npc[center];
            EndurisEye1 endurisEye = npc.modNPC as EndurisEye1;
            if (endurisEye != null)
            {
                SetPosition(npc);
                npc.position = new Vector2(endurisHead.Center.X - EyeposX, endurisHead.Center.Y - EyeposY);
                if (positionEye == 0)
                {
                    if (npc.ai[1] == 0)
                    {
                        EyeposX = 75;
                        EyeposY = 38;
                        npc.scale = 1.2f;
                    }
                }

            }

        }
    }
}