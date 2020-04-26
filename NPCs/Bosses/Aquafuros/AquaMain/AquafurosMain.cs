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


namespace Nocturn.NPCs.Bosses.Aquafuros.AquaMain
{
    [AutoloadBossHead]
    public class Aquafuros : ModNPC
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

        public int loop = 0;// Loop thing
        int Timer = 0;
        public int Framepos = 0;
        int AnimPhase = 0;
        int AnimFrames = 0;
        public int attack = 0;
        bool _Attacking = false;
        public static int attackphase = 0;
        public int attackRel = -1;
        public int attackdelayMAX = 0;
        public int attackdelay = 0;
        int _attackDur = 0;
        int _despawn = 0;
        public int shOOt = 0;
        public int shOOt2 = 0;
        //Thank you Laugic for that old YT vid you made for this!
        public float targetx = 0;
        public float targety = 0;
        public int vMAX = 0;
        public int vAccel = 0;
        public float tvel = 0f; //target Velocity
        public float vmag = 0f;

        //testing this out
        bool Goddamnit = false;
        public int TimerMAX = 0;
        public int AnimTime = 0;
        public float Tic = 0f;
        public float Phasetimer = 0f;


       


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquafuros");
            Main.npcFrameCount[npc.type] = 17;
        }
        public override void SetDefaults()
        {
            
            Goddamnit = false;
            loop = 0;
            TimerMAX = 10;
            Timer = TimerMAX;
            AnimTime = 0;
            Tic = 1f;
            Phasetimer = 0f;

            AnimFrames = 1;
            _attackDur = 0;
            Framepos = 0;
            AnimPhase = 0;
            attackphase = 0;
            shOOt2 = 0;
            shOOt = 0;
            _despawn = 0;
            attackdelayMAX = 30;
            attackdelay = attackdelayMAX;
            _Attacking = false;
            attack = 0;
            attackRel = -1;
            vMAX = 100;
            vAccel = 10;
            npc.aiStyle = 0;
            npc.lifeMax = 3400;
            npc.damage = 20;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            //npc.dontTakeDamage = false;
            npc.width = 120;
            npc.height = 118;
            npc.value = Item.buyPrice(0, 1, 10, 0);
            npc.npcSlots = 50f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = null;
            npc.alpha = 0;
            npc.dontTakeDamage = false;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AF");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            musicPriority = MusicPriority.BossHigh;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override void AI()
        {
            npc.dontTakeDamage = false;
            Main.raining = true;
            //Main.rainTime = 100;

            //Despawn
            #region DESPAWN
            //despawn
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

            //Movement code
            #region Movement
            //THANK YOU DUAL FOR THIS CODE
            float speed = tvel; // pixels per second
            npc.velocity = (Main.player[npc.target].Center - npc.Center).SafeNormalize(Vector2.Zero) * speed;

            Player player = Main.player[npc.target];
            if (npc.Distance(player.Center) < 50)
            {
                tvel -= 1;

            }
            if (npc.Distance(player.Center) < 40 && attackphase ==0)
            {

                tvel += 10;
            }
            float dist = Vector2.Distance(npc.Center, Main.player[npc.target].Center);
            tvel = dist / 70;
            if (vmag < vMAX && vmag < tvel)
            {

                
            }
            if (vmag > tvel)
            {
               

            }
            #endregion

            //Attack Cool
            #region Attack Cool
            attackCool -= 1f;


            if (Main.netMode != 1 && attackCool <= 0f)
            {
                attackCool = 600f + Main.rand.Next(200);

                npc.netUpdate = true;

            }
            #endregion

            //Frootloops
            #region Loops for phases
            if (loop == 0)
            {
                
            }
            if (loop == 10 && Goddamnit == false)
            {
                loop += 10;
                
            }
            if (loop == 20 && Goddamnit == false)
            {
                attackphase = 1;
                //loop++;
            }
            if (loop == 30)
            {
                if (attackCool <= 400f && attackCool >= 200f && Tic >= 20f)
                {
                    attackphase = 2;
                    //Tic = 0f;
                }
            }
            if (loop == 40)
            {
                if (attackCool <= 400f && attackCool >= 300f && Tic >= 300f)
                {
                    attackphase = 3;
                }

            }
            if (loop == 50)
            {
                if (attackCool <= 400f && attackCool >= 300f && Tic >= 400f && Phasetimer == 0)
                {
                    attackphase = 4;
                }
                if (attackCool <= 400f && attackCool >= 300f && Tic >= 100f && Phasetimer != 0)
                {
                    attackphase = 4;
                }
            }
            if (loop >= 80)
            {
                loop = 30;
            }
            if (Tic >= 600f)
            {
                Tic = 1f;
            }
            #region Timer for something or other
            //timer for shit
            /*if (!Goddamnit)
                Timer = -1;
            if (Timer <= 0)
            {
                AnimTime++;
                 Goddamnit = true;
                if (AnimTime > 5 * 30)
                    AnimTime = 1;
                //_attackDur = 15;
            }

            if (Goddamnit)
            {
                Tic -= 10;
                Goddamnit = false;
            }*/
            #endregion            
            #endregion

            //Animation loops
            #region Anim loops and whatnot

            if (_attackDur <= 10)// Idle anim
            {
                AnimPhase = 0;
            }
            else if (_attackDur <= 20 && _attackDur > 10)// Swing full anim
            {
                AnimPhase = 2;
                if (_attackDur == 15)// Swing loop anim
                {
                    AnimPhase = 1;
                }
            }
            else if (_attackDur >= 21)// Sets it back to Idle
            {
                _attackDur = 0;
            }
            #endregion

            //Aquaball delay
            #region Attack delay

            if (!_Attacking)
                attackdelay = -1;            
            if (attackdelay <= 0)
            {              
                attack++;                
                _Attacking = true;
                if (attack > 5*60)
                    attack = 1;
                shOOt = 0;
                shOOt2 = 1;                
            }
            #endregion

            //Phase 0
            #region Phase 0

            if (attackphase == 0)
            {
                
                _attackDur = 2;
                
            }

            if (shOOt == 0 && attack == 1 && _Attacking  && attackphase == 0) //attack #1
            {
                shOOt = 1;
                shOOt2 = 0;
                for (int i = 0; i < 8; i++)
                {
                    int v = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Aquaball2"));
                    Main.npc[v].ai[0] = npc.whoAmI;
                    int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Aquaball"));
                    Main.npc[n].ai[0] = npc.whoAmI;
                    Main.npc[n].ai[1] = i;
                    

                }
                Goddamnit = false;
                
                 if (attackphase == 0 && !Goddamnit)
                 {
                     loop += 10;
                 }
                
                attack = 1;
                _Attacking = false;
                
                
            }
            #endregion

            //Phase 1
            #region Phase 1

            if (attackphase == 1 )
            {
                
                if (loop == 20 && !Goddamnit)
                {
                    for (int k = 0; k < 10; k++) //this if for crating and centering the AFL's
                    {

                        int AF = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<AFL>());
                        Main.npc[AF].ai[0] = npc.whoAmI;
                        Main.npc[AF].ai[1] = k;
                        AFL.SetPosition(Main.npc[AF]);

                        npc.netUpdate = true;


                    }
                    loop += 10;
                }
                attackdelay = attackdelayMAX;
                _Attacking = false;
                attack = 1;
                Goddamnit = true;
                if (loop == 30 )
                {
                    if (Main.netMode != 1 && attackCool >= 500f && Phasetimer == 0f)
                    {
                        _attackDur = 15;
                        tvel += 2;
                    }
                    if (attackCool <=500f && attackCool >= 2f && Phasetimer ==0f)
                    {
                        _attackDur = 1;
                        Tic += 1f;
                    }
                   if (Main.netMode != 1 && attackCool >= 590f && Phasetimer != 0f)
                    {
                        _attackDur = 16;
                        tvel += 2;
                    }
                    if (attackCool < 590f && attackCool >= 2f && Phasetimer != 0f)
                    {
                        loop = 50;

                    }
                    if (attackCool <= 589f && attackCool >= 550f && Phasetimer != 0f)
                    {
                        _attackDur = 1;
                    }
                    if (attackCool <= 540f && attackCool >= 440f && Phasetimer != 0f)
                    {
                        _attackDur = 15;
                        tvel += 2;
                    }
                    if (attackCool <= 399f && attackCool >= 350f && Phasetimer != 0f)
                    {
                        Tic += 1f;
                        _attackDur = 15;
                    }
                    npc.netUpdate = true;
                }
                Goddamnit = false;
                npc.netUpdate = true;
            }
            #endregion

            //Phase 2
            #region Phase 2

            if (attackphase == 2)
            {
               // NPC aquafuros = Main.npc[center];
                Vector2 delta = Main.player[npc.target].Center - npc.Center;
                int damage = 15;

                if (Phasetimer == 0f)
                {
                    loop = 40;
                }
                
                if (Main.netMode != 1 && attackCool <= 590f && attackCool >= 500f)
                {
                    _attackDur = 15;
                }
                if (Main.netMode != 1 && attackCool <= 570f && attackCool >= 565f)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X, delta.Y, ProjectileType<WaterA>(), damage, 3f, Main.myPlayer);
                }
                if (Main.netMode != 1 && attackCool <= 540f && attackCool >= 535f)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X, delta.Y, ProjectileType<WaterA>(), damage, 3f, Main.myPlayer);
                }

                if (attackCool <= 500f && attackCool >= 450f )
                {
                    _attackDur = 2;
                }
                if (attackCool <= 449f && attackCool >= 350f)
                {
                    _attackDur = 15;
                    tvel += 2.5f;
                }
                if (attackCool <= 349f && attackCool >=2f)
                {
                    _attackDur = 5;
                }
                if (attackCool <= 300f && attackCool >= 100f && Phasetimer > 0f)
                {
                    //Tic += 1f;
                    Phasetimer = -1f;
                }
                if (Phasetimer == -1f)
                {
                    loop = 50;
                    Tic += 1f;
                    //attackphase = 4;
                }
                if (attackCool <= 300f && attackCool >= 100f && Phasetimer == 0f)
                {
                    Tic += 1f;
                }
                    npc.netUpdate = true;
            }
            #endregion

            //Phase 3
            #region Phase 3

            if (attackphase >= 3)
            {
                _attackDur = 16;

                if (loop == 40 && shOOt == 0 && _Attacking && attack == 1 && shOOt2 == 1)
                {

                    shOOt = 1;
                    shOOt2 = 0;
                    tvel += 5;
                    for (int k = 0; k < 10; k++)
                    {


                        int f = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WaterBall"));
                        Main.npc[f].ai[0] = npc.whoAmI;
                        Main.npc[f].ai[1] = k;
                        WaterBall.SetPosition(Main.npc[f]);
                    }
                    attackdelay = attackdelayMAX;
                    attack = 1;
                    _Attacking = false;
                    loop = 50;
                    npc.netUpdate = true;
                }
                if (loop == 50)
                {
                    if (Main.netMode != 1 && attackCool >= 580f)
                    {
                        _attackDur = 15;
                        tvel += 2.5f;
                    }
                    if (attackCool <= 500f && attackCool >= 2f)
                    {
                        _attackDur = 1;
                        Tic += 1f;
                        Phasetimer = -1f;
                    }
                    AnimTime = 1;
                    npc.netUpdate = true;

                }
            }
            #endregion

            //Phase 4 (Main Hub for the attacks)
            #region Phase 4
            if (attackphase == 4)
            {
                if (Phasetimer <= 0f)
                {
                    
                    Phasetimer = 1f;
                }
                if (Phasetimer == 1f)
                {
                    Phasetimer =  Main.rand.Next(8);
                }
                if (Phasetimer >= 2f && Phasetimer <= 3f)
                {
                    //Tic = 0;
                    //loop = 30;
                    attackphase = 5;
                    npc.netUpdate = true;
                }
                if (Phasetimer >=4f && Phasetimer <= 5f)
                {
                    loop = 50;
                    attackphase = 3;
                       
                }
                if (Phasetimer >= 6f && Phasetimer <= 7f)
                {
                    loop = 50;
                    attackphase = 2;
                }
                if (Phasetimer >= 11f)
                {
                    loop = 50;
                    Phasetimer = -1f;
                }
            }
            #endregion

            //Phase 5
            #region Phase 5

            if (attackphase == 5)
            {
                Vector2 delta = Main.player[npc.target].Center - npc.Center;
                int damage = 15;
                if (Main.netMode != 1 && attackCool >= 450f)
                    {
                        _attackDur = 15;
                        tvel += 3f;
                    
                    }
                    if (attackCool <= 400f && attackCool >= 397)
                    {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X, delta.Y, ProjectileType<WaterA>(), damage, 3f, Main.myPlayer);
                }
                    if (attackCool <= 199f && attackCool >= 2f)
                    {
                        _attackDur = 3;
                    
                        //tvel += 2;
                    }
                    
                if (attackCool <= 2f)
                {
                    npc.netUpdate = true;
                    Phasetimer = -1;
                    attackphase = 4;
                }
                
            }

            #endregion

            //making it so ifthe AFL's are alive he can't be attacked
            #region Inv
            bool Inv = false;
            for (int i = 0; i < Main.npc.Length -1; i++)
            {
                
                    if (Main.npc[i].active&&Main.npc[i].type == ModContent.NPCType<AFL>())
                    {
                    Inv = true;
                        break;
                    }                               
            }
            Main.raining = Inv;
            npc.dontTakeDamage = Inv;
            #endregion
        }


        //Actual NPC Frame count and whatnot 
        #region Frame shit
        public override void FindFrame(int frameHight)
        {

            if (AnimPhase == 0)
            {
                npc.frameCounter++;
                if (npc.frame.Y == frameHight * 6)
                {
                    
                        npc.frame.Y = 0;
                }

                if (npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHight;
                    if (npc.frame.Y >= frameHight * Main.npcFrameCount[npc.type]) npc.frame.Y = 0;
                }
            }

            if (AnimPhase == 1)
            {
                npc.frameCounter++;
                if (npc.frame.Y > frameHight * 13)
                {

                    npc.frame.Y = frameHight *11;
                }

                if (npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHight;
                    if (npc.frame.Y >= frameHight * Main.npcFrameCount[npc.type]) npc.frame.Y = 0;
                }
            }

            if (AnimPhase == 2)
            {
                npc.frameCounter++;
                if (npc.frame.Y > frameHight * 16)
                {

                    npc.frame.Y = 0;
                }

                if (npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHight;
                    if (npc.frame.Y >= frameHight * Main.npcFrameCount[npc.type]) npc.frame.Y = 0;
                }
            }

        }
        #endregion

        //Boss loot
        #region Loot
        public override void NPCLoot()
        {
            NocturnWorld.YeetedAquafuros = true;
             if (Main.expertMode)
             {
                 Item.NewItem(npc.getRect(), mod.ItemType("Aquabag"));
             }
             else
             {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Aquamace"));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AquaSpitter"));                
             }
        }
        #endregion
    }
}
    
 