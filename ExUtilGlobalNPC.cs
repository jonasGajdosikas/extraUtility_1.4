using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using extraUtility.Items;

namespace extraUtility
{
    public class ExUtilGlobalNPC : GlobalNPC
    {
        public ExUtilGlobalNPC()
        {
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            
            if (type == NPCID.WitchDoctor)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DeathRecallPot>());
                shop.item[nextSlot].shopCustomPrice = 2500;
                ++nextSlot;
            }
            base.SetupShop(type, shop, ref nextSlot);
        }
        
    }

    
}
