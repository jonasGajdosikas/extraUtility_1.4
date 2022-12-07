using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace ExtraUtility.Items
{
    class Herbicide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault($"{{$Mods.ExUtil.Herbicide.Name}}"); 
            Tooltip.SetDefault($"{{$Mods.ExUtil.Herbicide.Tooltip}}");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 10);
            Item.buffTime = 5 * 60 * 60;
            Item.buffType = ModContent.BuffType<StatusEffects.Withering>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Deathweed)
                .AddRecipeGroup(ExUtilSystem.AnyCopperOre)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
