using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace extraUtility.StatusEffects
{
    class Withering : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Withering");
            Description.SetDefault("All plants around you wither");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
        }

    }
}
