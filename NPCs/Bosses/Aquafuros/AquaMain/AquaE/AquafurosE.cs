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


namespace Nocturn.NPCs.Bosses.Aquafuros.AquaMain.AquaE
{
    [AutoloadBossHead]
    public class AquafurosE : ModNPC
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

        #region the Wall of Ints has awoken
        public int AttackPhase = 0;
        public int FramePos = 0;
        public int Attack = 0;
        public int Animation = 0;
        int _despawn = 0;
        #endregion

        #region Floaty bois
        public float AttackCool = 0f;
        public float Color = 0f;
        public float Alpha = 0f;
        public float tvel = 0f;
        public float PhaseT = 0f;
        public float point;
        public float _Switch = -1f;
        #endregion

        #region Booljob
        bool Attacking = false;
        bool Taget = false;
        #endregion

        Vector2 PartPos;
        Vector2 Movingto;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquafuros");
            Main.npcFrameCount[npc.type] = 13;
        }
        public override void SetDefaults()
        {
            Attack = 0;
            Color = 0.95f;
            Alpha = 1f;
            Animation = 0;
            PhaseT = 0f;
            _despawn = 0;
            _Switch = -1f;

            npc.aiStyle = -1;
            npc.boss = true;
            npc.damage = 70;
            npc.defense = 75;
            npc.lifeMax = 90000;
            npc.knockBackResist = 0f;
            npc.width = 120;
            npc.height = 118;
            npc.value = Item.buyPrice(0, 1, 10, 0);
            npc.npcSlots = 50f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = null;
            npc.alpha = 225;
            npc.dontTakeDamage = false;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AF");
            musicPriority = MusicPriority.BossHigh;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override void AI()
        {
            #region Testing things
            Main.NewText(npc.frame.Y);
            Main.NewText(Animation);
            #endregion

            #region The ever standing constants of the land
            Player player = Main.player[npc.target];
            AquafurosE Aquapos = npc.modNPC as AquafurosE;
            float dist = Vector2.Distance(npc.Center, Main.player[npc.target].Center);

            #region Movement
            float speed = tvel; // pixels per second
            npc.velocity = (Main.player[npc.target].Center - npc.Center).SafeNormalize(Vector2.Zero) * speed;
            Vector2 center = Main.npc[Aquapos.center].Center;
            Vector2 AttackDist;
            
            #region Flip sprite der
            if (player.Center.X > npc.Center.X)
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }
            #endregion
            #endregion

            #endregion
            #region DESPAWN

            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    if (_despawn == 0)
                        _despawn++;
                }
            }
            if (_despawn >= 1)
            {
                _despawn++;
                npc.noTileCollide = true;
                if (_despawn >= 100)
                    npc.active = false;
            }
            #endregion
            #region Particles
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            PartPos = new Vector2(npc.Center.X - 45, npc.Center.Y - 45);
            dust = Main.dust[Terraria.Dust.NewDust(PartPos, 105, 121, 20, 0f, 0f, 211, new Color(0, 255, 117), 0.9868422f)];
            dust.noLight = true;
            dust.fadeIn = 0.6315789f;
            #endregion
            #region Attack loop
            attackCool -= 1f;

            if (Main.netMode != 1 && attackCool <= 0f)
            {
                attackCool = 200f;
                AttackPhase += 1;
                npc.netUpdate = true;

            }
            if (AttackPhase >= 6)
            {
                AttackPhase = 0;
            }
            #endregion                       
            #region Attack hub
            if (Attack == 0)
            {
                Animation = 0;
                if (PhaseT <= 0f)
                {
                    PhaseT = 1f;
                }
                if(PhaseT == 1f)
                {
                    PhaseT = 2;
                }
                if (PhaseT == 2f)
                {
                    Attack = 1;
                }
                if (PhaseT == 3f)
                {
                    Attack = 2;
                }
                if(PhaseT == 4f)
                {
                    Attack = 3;
                }
                if (PhaseT == 5f)
                {
                    Attack = 4;
                }
                if (PhaseT >= 6f)
                {
                    PhaseT = -1f;
                }
            }
            #endregion
            #region attack 1
            if (Attack == 1)
            {
                
                if (AttackPhase == 0)
                {
                    if (_Switch == -1)
                    {
                        _Switch = npc.ai[1] * 6.28f / 1;
                    }
                    
                    _Switch += 3.14f / 25;
                    tvel = 13;
                    Animation = 2;
                    AttackDist = npc.position;
                    center.X = AttackDist.X + 10 * (float)Math.Cos(_Switch);
                    center.Y = AttackDist.Y + 30 * (float)Math.Sin(_Switch);
                    npc.position = center;
                    
                }
                if(AttackPhase == 1)
                {
                    if (_Switch == -1)
                    {
                        _Switch = npc.ai[1] * 6.28f / 1;
                    }

                    _Switch += 3.14f / 30;
                    AttackDist = npc.position;
                    center.X = AttackDist.X + 25 * (float)Math.Cos(_Switch);
                    center.Y = AttackDist.Y + 25 * (float)Math.Sin(_Switch);
                    npc.position = center;
                    tvel = npc.direction;
                    npc.direction = 10;
                    npc.velocity = npc.velocity *2;
                    Animation = 2;
                }
                if (AttackPhase == 2)
                {
                    tvel = 3f;
                    Animation = 0;
                }
                
                if (AttackPhase == 3)
                {
                    if (attackCool >= 100)
                    {
                        tvel = 13f;
                        _Switch += 3.14f / 30;
                        AttackDist = npc.position;
                        center.X = AttackDist.X + 25 * (float)Math.Sin(_Switch);
                        center.Y = AttackDist.Y + 25 * (float)Math.Sin(_Switch);
                        npc.position = center;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        tvel = 13f;
                        _Switch += 3.14f / 30;
                        AttackDist = npc.position;
                        center.X = AttackDist.X + 25 * (float)Math.Cos(_Switch);
                        center.Y = AttackDist.Y - 25 * (float)Math.Cos(_Switch);
                        npc.position = center;
                        npc.netUpdate = true;
                    }
                    Animation = 2;
                    

                }
                if (AttackPhase == 4)
                {
                    Animation = 1;
                    tvel = 6f;
                    npc.velocity = (Main.player[npc.target].Center - npc.Center) /70;
                    if (player.direction == 1)
                    {
                        npc.position = new Vector2(player.Center.X + 180, player.Center.Y - 45);
                    }
                    else
                    {
                        npc.position = new Vector2(player.Center.X - 280, player.Center.Y - 45);
                    }
                    //npc.position = new Vector2 (player.Center.X + 180, player.Center.Y - 45);
                    npc.netUpdate = true;
                }
                if (AttackPhase == 5)
                {
                    Animation = 1;

                }
            }
            #endregion
            #region attack 2
            if (Attack == 2)
            {

            }
            #endregion
            #region attack 3
            if (Attack == 3)
            {

            }
            #endregion
            #region attack 4
            if (Attack == 4)
            {

            }
            #endregion

        }

        #region PreDraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            drawColor = new Color(Color, Color, Color, Alpha);
            Texture2D CurrentAttack = null;
            Texture2D Idle = Main.npcTexture[npc.type];
            Texture2D Idle2 = mod.GetTexture("NPCs/Bosses/Aquafuros/AquaMain/AquaE/AquafurosE");
            Texture2D Swing = mod.GetTexture("NPCs/Bosses/Aquafuros/AquaMain/AquaE/AquafurosE_Swing");
            Texture2D SPEEN = mod.GetTexture("NPCs/Bosses/Aquafuros/AquaMain/AquaE/AquafurosE_SPEEN");

            if (Animation == 0)
            {
                CurrentAttack = Idle;
            }
            if (Animation == 1)
            {
                CurrentAttack = Swing;
            }
            if (Animation == 2)
            {
                CurrentAttack = SPEEN;
            }
            var FlipthySprite = npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(CurrentAttack, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, new Vector2(npc.width * 0.5f, npc.height * 0.5f), 1f, FlipthySprite, 0f);

            return false;
        }
        #endregion

        #region Frames
        public override void FindFrame(int frameHight)
        {

            if (npc.frameCounter > 4) //Failsafe
            {
                //npc.frame.X = 0;
                //npc.frame.Width = npc.width;
                npc.frameCounter = 0;

                npc.frame.Y += 118;
                if (npc.frame.Y >= frameHight * Main.npcFrameCount[npc.type]) npc.frame.Y = 0;
            }
            //npc.frame.Y = 0;
            frameHight = 118;
            npc.frame.Height = 118;
            npc.frameCounter++;
            if (Animation == 0)
            {
                
                if (npc.frame.Y >= 118 * 8)
                {
                    npc.frame.Y = 0;
                }                
            }
            if (Animation == 1)
            {
                if (AttackPhase == 4)
                {
                    if (npc.frame.Y < 118 * 3)
                    {
                        npc.frame.Y = frameHight * 3;
                    }
                    if (npc.frame.Y == 118* 4)
                    {
                        npc.frame.Y = frameHight* 3;
                    }
                }
                if (AttackPhase == 5)
                {
                    if (npc.frame.Y <= 118 * 4)
                    {
                        tvel = 10;

                    }
                    if (npc.frame.Y== 118* 5)
                    {
                        tvel = 8;
                    }
                    if (npc.frame.Y == 118 * 6)
                    {
                        tvel = 6;
                    }
                    if (npc.frame.Y == 118 * 7)
                    {
                        tvel = 4;
                    }
                    if (npc.frame.Y == 118 * 8)
                    {
                        tvel = 2;
                    }
                }
                if (npc.frame.Y >= frameHight * 8)
                {
                    npc.frame.Y = 0;
                }
            }
            if (Animation == 2)
            {
                
                /*if(npc.frame.Y < frameHight *4 )
                {
                    npc.frame.Y = frameHight * 4;
                }*/
                if (npc.frame.Y > frameHight * 6)
                {
                    npc.frame.Y = frameHight * 4;
                }               
            }
        }
        #endregion
    }
}
