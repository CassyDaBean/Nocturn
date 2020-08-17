
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Nocturn.Support;
namespace Nocturn
{
    public class Nocturn : Mod
    {
        internal static Nocturn instance;
        internal UserInterface Smith;
        public Nocturn()
        {

            instance = this;
        }
        public override void PostSetupContent()
        {
            WeakReferences.GoModSupport();
        }
        public override void Load()
        {
            if (!Main.dedServ)
            {
                Smith = new UserInterface();

            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "Nocturn: Smith UI",
                    delegate
                    {
                        Smith.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

        }
        public override void UpdateUI(GameTime gameTime)
        {
            Smith?.Update(gameTime);
        }
    }
}

