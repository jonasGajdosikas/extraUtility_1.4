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
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ExUtilPlayer>().WithersPlants = true;
        }
    }
}
