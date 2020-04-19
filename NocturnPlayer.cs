using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Nocturn.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Nocturn
{
    public partial class NocturnPlayer : ModPlayer
    {
        #region Summons
        public bool GigaBab = false;

        #endregion

        public override void ResetEffects()
        {
            resetminion();
        }

        private void resetminion()
        {
            GigaBab = false;

        }

    }
}
