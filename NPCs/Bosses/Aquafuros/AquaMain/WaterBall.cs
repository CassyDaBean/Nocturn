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
using Nocturn.Projectiles.Bosses.Aqua;

namespace Nocturn.NPCs.Bosses.Aquafuros.AquaMain
{
    class WaterBall : ModNPC
    {
        int _despawn = 0;
        public static int aiballcount = 0;
        float _blip = -1;
        float _dist = 0;
        float _distRate = .1f;
        Vector2 _origin;
        int ree = 0;

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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Waterball");
        }

       
       

        public override void SetDefaults()
        {
            aiballcount = 0;
            _distRate = 3;
            _dist = 100;
            _blip = -1;
            _despawn = 0;
            npc.aiStyle = 0;
            npc.lifeMax = 1;
            npc.damage = 30;
            npc.defense = 55;
            npc.knockBackResist = 0f;
            npc.dontTakeDamage = true;
            npc.width = 28;
            npc.height = 36;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 10f;
           // npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            //npc.HitSound = SoundID.NPCHit1;
            //npc.DeathSound = SoundID.NPCDeath1;
            //music = MusicID.Boss2;
        }

        public override void AI()
        {
            #region DESPAWN

                NPC aquafuros = Main.npc[center];
            if (!aquafuros.active || aquafuros.type != NPCType<Aquafuros>())
            {

                npc.life = -1;
                npc.active = false;
                return;

            }

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

            #endregion

            #region Attack

            attackCool -= 1f;
            if (Main.netMode != 1 && attackCool <= 0f)
            {
                attackCool = 600f + Main.rand.Next(200);
                Vector2 delta = Main.player[aquafuros.target].Center - npc.Center;
                
                int damage = 15;
                if (Main.expertMode)
                {
                    damage = 9;
                }
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X, delta.Y, ProjectileType<WaterA>(), damage, 3f, Main.myPlayer);
                npc.netUpdate = true;
            }

            #endregion

            #region Position prt 1
            
            if (_blip == -1 && aiballcount == 0)
            {
                _blip = npc.ai[1] * 6.28f / 3;
                _origin = npc.position;

            }
            _blip += 3.14f / 50;
            WaterBall modNPC = npc.modNPC as WaterBall;
            if (modNPC != null)
            {
                Vector2 center = Main.npc[modNPC.center].Center;

                Vector2 _Dist;
                SetPosition(npc);
                
                _Dist = npc.position;
                center.X = _Dist.X - 60 * (float)Math.Sin(_blip) - npc.width / 2 ;
                center.Y = _Dist.Y - 60 * (float)Math.Cos(_blip);
                npc.position = center;
            }

            #endregion
        }

        #region Position prt 2
        public static void SetPosition(NPC npc)
        {
            float Rotation = 0;
            WaterBall modNPC = npc.modNPC as WaterBall;
            if (modNPC != null)
            {
                Vector2 center = Main.npc[modNPC.center].Center;

                float test = -1;
                if (test == -1)
                {
                    Rotation += 0.1f;
                    test = npc.ai[1] * 6.28f / 8;
                }


                npc.position = center;
            }
        }
        #endregion
    }
}
