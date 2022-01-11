using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria;


namespace extraUtility
{
	public class extraUtility : Mod
	{
		public static RecipeGroup AnyEvilMaterial,
			AnyLivingFire, AnyPoorTomb, AnyRichTomb, AnyMirror, AnyCopperOre;
		public override void AddRecipeGroups()
		{
			AnyEvilMaterial = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Crafting Material", new int[] { ItemID.Ichor, ItemID.CursedFlame });
			RecipeGroup.RegisterGroup("ExUtil:EvilMaterial", AnyEvilMaterial);

			AnyLivingFire = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Living Fire", new int[] {
				ItemID.LivingFireBlock, ItemID.LivingCursedFireBlock, ItemID.LivingDemonFireBlock,
				ItemID.LivingFrostFireBlock, ItemID.LivingIchorBlock, ItemID.LivingUltrabrightFireBlock });
			RecipeGroup.RegisterGroup("ExUtil:AnyLivingFire", AnyLivingFire);

			int[] graves = new int[]
			{
				ItemID.Tombstone,
				ItemID.GraveMarker,
				ItemID.CrossGraveMarker,
				ItemID.Headstone,
				ItemID.Gravestone,
				ItemID.Obelisk
			};
			AnyPoorTomb = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Non-Gilded Gravestone", graves);
			RecipeGroup.RegisterGroup("ExUtil:AnyPoorTomb", AnyPoorTomb);

			int[] richGraves = new int[]
			{
				ItemID.RichGravestone1,
				ItemID.RichGravestone2,
				ItemID.RichGravestone3,
				ItemID.RichGravestone4,
				ItemID.RichGravestone5
			};
			AnyRichTomb = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gilded Gravestone", richGraves);
			RecipeGroup.RegisterGroup("ExUtil:AnyRichTomb", AnyRichTomb);

			AnyMirror = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Magic Mirror", new int[] { ItemID.MagicMirror, ItemID.IceMirror });
			RecipeGroup.RegisterGroup("ExUtil:AnyMirror", AnyMirror);

			AnyCopperOre = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Copper Ore", new int[] { ItemID.CopperOre, ItemID.TinOre });
			RecipeGroup.RegisterGroup("ExUtil:AnyCopperOre", AnyCopperOre);
		}

	}
}