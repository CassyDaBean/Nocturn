using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria.DataStructures;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.Linq;
using Terraria.Localization;


namespace Nocturn
{
   public class NocturnWorld : ModWorld
    {


        public static bool WormInvasionUp = false;


        public override void Initialize()
        {

            WormInvasionUp = false;


        }
        public override void PostUpdate()
        {
           /* if (WormInvasionUp)
            {

                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (NPCs.WEveGlobal.Wave1worms(other.type))
                    {
                        other.alpha += 5;
                        if (other.alpha >= 255)
                        {
                            other.active = false;
                        }
                    }
                }
            } */           
        }
   }
}

