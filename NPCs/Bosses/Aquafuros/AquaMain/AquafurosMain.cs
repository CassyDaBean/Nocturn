
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
    //[AutoloadBossHead]
    public class Aquafuros : ModNPC
    {

        public int Framepos = 0;
        int AnimPhase = 0;

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


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquafuros");
            Main.npcFrameCount[npc.type] = 17;
        }
        public override void SetDefaults()
        {
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
            npc.lifeMax = 1000;
            npc.damage = 20;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.dontTakeDamage = false;
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
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }



        }
        public override void AI()
        {

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
                if (_despawn >= 300)
                    npc.active = false;
            }

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


            //should help stop the projectile SPAM that happened in the past


            //lets try this (still expirementing with this one)

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

          

            if (shOOt == 0 && attack == 1 && _Attacking && attackphase == 0) //attack #1
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
                attackdelay = attackdelayMAX;
                
                attack = 1;
                _Attacking = false;
            }
            if (npc.life <= npc.lifeMax * .75 && attackphase == 0) //attack Trasfer
            {
                attackphase += 1;
                attackdelayMAX -= 30;
                //AnimPhase = 1;
            }
            if (attackphase == 1)
            {
                AnimPhase = 2;
                attackphase += 1;
                tvel -= 1;
                /*for (int k = 0; k < 10; k++) //this was gonna be used for centering the AFL's but it didn't work out too well
                {
                    
                    int f = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AFLbase"));
                    Main.npc[f].ai[0] = npc.whoAmI;
                    Main.npc[f].ai[1] = k;
                    npc.netUpdate = true;
                }*/
               
            }
            if (attackphase >= 2) //attack #2
            {
                AnimPhase = 3;
                if (attackphase == 2 && shOOt == 0 && _Attacking && attack == 1 && shOOt2 == 1)
                {
                    AnimPhase = 3;
                    shOOt = 1;
                    shOOt2 = 0;
                    for (int k = 0; k < 10; k++)
                    {


                        int f = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AFL"));
                        Main.npc[f].ai[0] = npc.whoAmI;
                        Main.npc[f].ai[1] = k;
                    }
                    attack = 1;
                    _Attacking = false;
                }
                

            }
            
        }

        
     
        //Frame shit
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
                if (npc.frame.Y > frameHight * 10)
                {

                    npc.frame.Y = 6;
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
                if (npc.frame.Y > frameHight * 14)
                {

                    npc.frame.Y = 10;
                }

                if (npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHight;
                    if (npc.frame.Y >= frameHight * Main.npcFrameCount[npc.type]) npc.frame.Y = 0;
                }
            }

        }
        
        

        /* public override void NPCLoot() {



             if (Main.expertMode)
             {
                 Item.NewItem(npc.getRect(), mod.ItemType("Aquabag"));
             }
           }*/

    }
}
    
 