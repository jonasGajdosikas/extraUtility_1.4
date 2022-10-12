using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraUtility.Items
{
    public class PocketPylon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.Tooltip.SetDefault("Teleport to any pylon, no matter where you are");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TeleportationPylonPurity)
                .AddIngredient(ItemID.TeleportationPylonDesert)
                .AddIngredient(ItemID.TeleportationPylonSnow)
                .AddIngredient(ItemID.TeleportationPylonUnderground)
                .AddIngredient(ItemID.TeleportationPylonJungle)
                .AddIngredient(ItemID.TeleportationPylonMushroom)
                .AddIngredient(ItemID.TeleportationPylonOcean)
                .AddIngredient(ItemID.TeleportationPylonHallow)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.maxStack = 1;
            Item.consumable = false;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<ExUtilPlayer>().hasPocketPylon = true;
        }
        public override void Load()
        {
            On.Terraria.Player.IsTileTypeInInteractionRange += Player_IsPylonInInteractionRangeOrInv;
            On.Terraria.Player.InInteractionRange += Player_PylonInInteractionRangeOrPocket;
        }
        public override void Unload()
        {
            On.Terraria.Player.IsTileTypeInInteractionRange -= Player_IsPylonInInteractionRangeOrInv;
            On.Terraria.Player.InInteractionRange -= Player_PylonInInteractionRangeOrPocket;
        }
        private bool Player_PylonInInteractionRangeOrPocket(On.Terraria.Player.orig_InInteractionRange orig, Player self, int interactX, int interactY)
        {
            return (Main.tile[interactX, interactY].TileType == 597 && self.GetModPlayer<ExUtilPlayer>().hasPocketPylon) || orig.Invoke(self, interactX, interactY);
        }
        private bool Player_IsPylonInInteractionRangeOrInv(On.Terraria.Player.orig_IsTileTypeInInteractionRange orig, Player self, int targetTileType)
        {
            return (targetTileType == 597 && self.GetModPlayer<ExUtilPlayer>().hasPocketPylon) || orig.Invoke(self, targetTileType);
        }
    }
}
