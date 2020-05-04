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
        public static bool YeetedAquafuros;
        public static bool YeetedMechs;

        public override void Initialize()
        {
            YeetedMechs = NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
            YeetedAquafuros = false;
            WormInvasionUp = false;


        }

        public override TagCompound Save()
        {
            var Yeeted = new List<string>();
            if (YeetedAquafuros) Yeeted.Add("H2OBoi");
            if (YeetedMechs) Yeeted.Add("mECH");

            return new TagCompound
            {
                {"yeeted", Yeeted}
            };


        }

        public override void Load(TagCompound tag)
        {
            var Yeeted = tag.GetList<string>("yeeted");
            YeetedAquafuros = Yeeted.Contains("H2OBoi");
            YeetedMechs = Yeeted.Contains("mECH");

            YeetedMechs = NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;

        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = YeetedAquafuros;
                writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            YeetedAquafuros = flags[0];
        }

        public override void PostUpdate()
        {
              if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                if(!YeetedMechs)
                {
                    YeetedMechs = true;
                }
            }
        }
   }
}

