using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nocturn.NPCs
{
    class SkeletonSmith : ModNPC
    {

        public bool InAir = false;
        public bool Jump = false;
        public byte FrameSelection = 0;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seth the Skeleton Smith");
            Main.npcFrameCount[npc.type] = 9;
        }
        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 52;
            npc.friendly = true;
            npc.lifeMax = 300;
            npc.aiStyle = -1;
            npc.damage = 45;
            npc.defense = 30;
            npc.dontTakeDamageFromHostiles = true;
        }


        #region Chat things
        public override bool CanChat()
        {
            return true;
        }
        public override string GetChat()
        {            
            switch (Main.rand.Next(0,4))
            {
                case 0:
                    return "Where's the Blacksmith?!?! Oh. that's me.";
                case 1:
                    return "Come and chat for a while, it gets so lonely sometimes I feel like I'm talking to spirits!";
                case 2:
                    return "Swords? Guns? Flails? all of it is up for tempering! How do I temper a gun? Don't get me started...";
                default:
                    return "If you ever see a Skeleton with a hat, bag, and Slaphand underground, tell them I said hi!";
            }
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Temper a Weapon";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                ModContent.GetInstance<Nocturn>().Smith.SetState(new UI.BaseUI());
                Main.playerInventory = true;
            }
        }
        #endregion

        #region Find Frame
        public override void FindFrame(int frameHeight)
        {
            frameHeight = npc.height;
            if (npc.frameCounter > 4)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
            }
            
            npc.frameCounter++;
            if (FrameSelection == 0)
            {
                npc.frame.Y = 0;
            }
            if (FrameSelection == 0 && Jump)
            {
                npc.frame.Y = frameHeight;
            }
            if (FrameSelection == 1)
            {
                //npc.frame.Y += frameHeight;
                if (npc.frame.Y >= 416)
                    npc.frame.Y = 104;
            }
        }
        #endregion

        #region PreDraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D Spront = null;
            Texture2D Hammer = null;
            Texture2D Skele = Main.npcTexture[npc.type];
            Texture2D SkeleH = mod.GetTexture("Misc/SkeleHamNPC2");


            if (Spront == null)
            {
                Spront = Skele;
                Hammer = SkeleH;
            }
            
            var FlipthySprite = npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(Hammer, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, FlipthySprite, 0f);
            spriteBatch.Draw(Spront, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, FlipthySprite, 0f);

            return false;

        }
        #endregion

        #region AI
        #region Values
        int MovementCount = 0;
        bool StopFuckinMovin = false;
        int AttackCool = 0;
        byte Walktheotherway = 0;
        #endregion

        public override void AI()
        {
            Player player = Main.player[npc.target];
            Vector2 delta = Main.player[npc.target].Center - npc.Center;
            Vector2 Test = new Vector2 (player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y)*2;
            float dist = Vector2.Distance(npc.Center, Main.player[npc.target].Center);
            npc.TargetClosest(true);

            #region Flip sprite
            Main.NewText(Test);
            //Main.NewText(delta.SafeNormalize(Vector2.One) * 2);
            #endregion

            #region Movement counter
            if (npc.Distance(player.Center) > 0f)
            {
                MovementCount -= 1;
                StopFuckinMovin = false;
            }

            if (npc.Distance(player.Center) < 300f)
            {
                StopFuckinMovin = true;
                npc.velocity *= 0.95f;
                if (Math.Abs(npc.velocity.X) < 0.1f)
                {
                    npc.spriteDirection = player.Center.X > npc.Center.X ? 1 : -1;
                    npc.velocity.X = 0;
                }
            }



            if (MovementCount <= 0)
            {
                if (!StopFuckinMovin)
                {
                    MovementCount = Main.rand.Next(150, 250); // this is to give the movements a bit more randomness of how long he walks.
                    Walktheotherway = (byte)Main.rand.Next(0, 2); // tells him to turn around and walk the other direction.
                }
                if (StopFuckinMovin)
                {
                    MovementCount = 50;
                    if (MovementCount < 50)
                        MovementCount = 51;
                }
                
            }
            #endregion

            #region Movement 
            if (MovementCount >100 && Walktheotherway == 0) //Walk to the Right
            {
                FrameSelection = 1;
                npc.velocity.X = 0.7f;
                npc.spriteDirection = -1;
            }
            if (MovementCount < 100) //Stop walking
            {
                FrameSelection = 0;
                npc.velocity.X = 0f;
                Jump = false;
            }
            if (MovementCount > 100 && Walktheotherway == 1) //Walk to the Left
            {
                FrameSelection = 1;
                npc.velocity.X = -0.7f;
                Jump = false;
                npc.spriteDirection = 1;
            }
            if (npc.velocity.Y != 0f) // if in Air, Play this anim
            {
                FrameSelection = 0;
                Jump = true;
            }

           

            #endregion

        }
        #endregion
    }
}
