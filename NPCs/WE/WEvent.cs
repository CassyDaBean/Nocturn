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



namespace Nocturn.NPCs.WE
{
    public class WEvent : ModNPC
    {


        int _despawn = 0;
        public static int aiballcount = 0;
        float _blip = -1;
        float _dist = 0;
        float _distRate = .1f;
        Vector2 _origin;
       
        public override void SetStaticDefaults()
        {
                      DisplayName.SetDefault("Worm Regime");
                      Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            _despawn = 0;
            aiballcount = 0;
            _distRate = 3;
            _dist = 50;
            _blip = -1;
            npc.width = 2;
            npc.height = 2;
            npc.damage = 0;
            npc.defense = 12;
            npc.lifeMax = 1;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 0;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.buffImmune[24] = true;
            npc.dontTakeDamage = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/WR");
            musicPriority = MusicPriority.BossHigh;
        }


        public override void AI()
        {
            //Emerald Ball movement 1 (and hopefully it will work) 
            if (_blip == -1 && aiballcount == 0)
            {
                _blip = npc.ai[1] * 6.28f / 8;
                _origin = npc.position;
            }

            const float speed = 10; // pixels per second
            npc.velocity = (Main.player[npc.target].Center - npc.Center).SafeNormalize(Vector2.Zero) * speed;




            //Should despawn after the player dies (hopefully)
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
            if (_dist > 100)
            {
                npc.active = false;
                npc.life = 0;
            }


            // Worm event spawn shananagiens
            if (npc.active == true)
            {
                NocturnWorld.WormInvasionUp = true;


            }

            if (npc.active == false)
            {
                NocturnWorld.WormInvasionUp = false;


            }                          
            
        }                              
    }
}
