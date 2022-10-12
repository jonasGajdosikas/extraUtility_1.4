using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.Audio;

namespace extraUtility
{
	public class extraUtility : Mod
	{
		internal static extraUtility Instance;
		public override void Load()
		{
			Instance = this;
			IL.Terraria.Main.TryInteractingWithMoneyTrough += Main_TryInteractingWithChester;
			IL.Terraria.Player.HandleBeingInChestRange += Player_HandleBeingInChesterRange;
			IL.Terraria.Player.ItemCheck_Inner += Player_CellphoneReturnHook;
		}

		private void Player_CellphoneReturnHook(ILContext il)
		{
			var c = new ILCursor(il);
			if (!c.TryGotoNext(i => (i.MatchLdcI4(3124))))
				return;
			if (!c.TryGotoNext(MoveType.After,i => (i.MatchCall<Player>(nameof(Player.RemoveAllGrapplingHooks)))))
				return;
			var normalRecallBegin = il.DefineLabel();
			var normalRecallEnd = il.DefineLabel();
			var retBegin = il.DefineLabel();
			c.Emit(OpCodes.Ldarg_0);
			c.EmitDelegate<Func<Player, bool>>((player) =>
			{
				return player.inventory[player.selectedItem].netID == ItemID.CellPhone;
			});
			c.Emit(OpCodes.Brfalse_S, normalRecallBegin);
			c.Emit(OpCodes.Ldarg_0);
			c.Emit(OpCodes.Ldarg_0);
			c.EmitDelegate<Func<Player, bool>>(player => player.altFunctionUse == 2);
            c.Emit(OpCodes.Brfalse_S, retBegin);
            c.EmitDelegate<Func<Player, ExUtilPlayer>>(player => player.GetModPlayer<ExUtilPlayer>());
			c.Emit(OpCodes.Call, typeof(ExUtilPlayer).GetMethod(nameof(ExUtilPlayer.ReturnToDeathPoint)));
			c.Emit(OpCodes.Br_S, normalRecallEnd);
			c.MarkLabel(retBegin);
            c.Emit(OpCodes.Call, typeof(Player).GetMethod(nameof(Player.DoPotionOfReturnTeleportationAndSetTheComebackPoint)));
			c.Emit(OpCodes.Br_S, normalRecallEnd);
			c.MarkLabel(normalRecallBegin);
			if (!c.TryGotoNext(MoveType.After, i => (i.MatchCall<Player>(nameof(Player.Spawn)))))
				return;
			c.MarkLabel(normalRecallEnd);
		}

		public override void Unload()
        {
            IL.Terraria.Main.TryInteractingWithMoneyTrough -= Main_TryInteractingWithChester;
			IL.Terraria.Player.HandleBeingInChestRange -= Player_HandleBeingInChesterRange;
            IL.Terraria.Player.ItemCheck_Inner -= Player_CellphoneReturnHook;
        }

		// to prevent terraria from closing chester's inventory when it's not piggy bank
		private void Player_HandleBeingInChesterRange(ILContext il) 
		{
			var c = new ILCursor(il);
			if (!c.TryGotoNext(MoveType.After, i => i.MatchLdcI4(-2)))
				return;
			c.Emit(OpCodes.Ldarg_0);
			c.EmitDelegate<Func<int, Player, int>>((returnValue, player) =>
			{
				if(Main.projectile[player.piggyBankProjTracker.ProjectileLocalIndex].type == 960)
				{
                    foreach (Item slot in player.inventory)
                    {
                        if (slot.netID == ItemID.DefendersForge && slot.favorited)
                        {
                            return -4;
                        }
                        if (slot.netID == ItemID.Safe && slot.favorited)
                        {
                            return -3;
                        }
                    }
                    return -2;
                }

				return returnValue;
			});

		}

		//set the opened chest when interacting with chester
		private void Main_TryInteractingWithChester(ILContext il)
		{
			var c = new ILCursor(il);
			while (c.TryGotoNext(MoveType.After, i => i.MatchLdcI4(-2)))
			{
				c.Emit(OpCodes.Ldarg_0);
				c.EmitDelegate<Func<int, Projectile, int>>((returnValue, projectile) =>
				{
                    if (projectile.type == 960)
                    {
                        foreach (Item slot in Main.player[projectile.owner].inventory)
                        {
                            if (slot.netID == ItemID.DefendersForge && slot.favorited)
                            {
                                return -4;
                            }
                            if (slot.netID == ItemID.Safe && slot.favorited)
                            {
                                return -3;
                            }
                        }
                        return -2;
                    }
                    return returnValue;
				});

			}
		}
	}
	public class ExUtilGlobalItem : GlobalItem
    {
		public override void SetDefaults(Item item)
        {
            switch (item.type)
            {
				case ItemID.PlatinumCoin:
					item.maxStack = 2046;
					break;

            }
        }
		TooltipLine FountainTooltip(string biome) => new TooltipLine(Mod, "TooltipFountain", $"[i:909] [c/BBBBBB:Forces surrounding biome state to {biome} if favorited in inventory]");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line;
			switch (item.type)
			{
                case ItemID.PureWaterFountain:
					tooltips.Add(FountainTooltip("Ocean"));
                    break;
                case ItemID.OasisFountain:
                case ItemID.DesertWaterFountain:
                    tooltips.Add(FountainTooltip("Desert"));
                    break;
                case ItemID.IcyWaterFountain:
                    tooltips.Add(FountainTooltip("Snow"));
                    break;
                case ItemID.JungleWaterFountain:
                    tooltips.Add(FountainTooltip("Jungle"));
                    break;
                case ItemID.CorruptWaterFountain:
                    tooltips.Add(FountainTooltip("Corruption"));
                    break;
                case ItemID.CrimsonWaterFountain:
                    tooltips.Add(FountainTooltip("Crimson"));
                    break;
                case ItemID.HallowedWaterFountain:
                    tooltips.Add(FountainTooltip("Hallowed (hardmode only)"));
                    break;
                case ItemID.ChesterPetItem:
                    line = new TooltipLine(Mod, "TooltipChester0", $"[i:{ItemID.ChesterPetItem}] [c/BBBBBB:Otto von Chesterfield, Esq will open your]");
					tooltips.Add(line);
                    line = new TooltipLine(Mod, "TooltipChester1", $"[c/BBBBBB:leftmost] [i:{ItemID.DefendersForge}] [c/BBBBBB:Defender's Forge or] [i:{ItemID.Safe}] [c/BBBBBB:Safe]");
                    tooltips.Add(line);
                    line = new TooltipLine(Mod, "TooltipChester2", $"[c/BBBBBB:if there is one favorited in your inventory]");
                    tooltips.Add(line);
                    break;
				case ItemID.CellPhone:
					line = new TooltipLine(Mod, "TooltipCellphone0", $"[i:{ItemID.PotionOfReturn}] [c/BBBBBB:Your Cell Phone is able to save your recall position]");
					tooltips.Add(line);
					line = new TooltipLine(Mod, "TooltipCellphone1", $"[c/BBBBBB:and allows you to return to your old position]");
					tooltips.Add(line);
					line = new TooltipLine(Mod, "TooltipCellphoneDeath", $"[i:{ItemID.Gravestone}] [c/BBBBBB:Right click to teleport to your death position]");
					tooltips.Add(line);
					break;
                default:
					break;
			}
		}
		public override void UpdateInventory(Item item, Player player)
		{
			if (item.favorited)
			{
				switch (item.type)
				{
					case ItemID.PureWaterFountain:
						player.ZoneBeach = true;
						break;
					case ItemID.OasisFountain:
					case ItemID.DesertWaterFountain:
						player.ZoneDesert = true;
						player.ZoneUndergroundDesert = player.Center.Y > 3200f;
						break;
					case ItemID.IcyWaterFountain:
						player.ZoneSnow = true;
						break;
					case ItemID.JungleWaterFountain:
						player.ZoneJungle = true;
						break;
					case ItemID.CorruptWaterFountain:
						player.ZoneCorrupt = true;
						break;
					case ItemID.CrimsonWaterFountain:
						player.ZoneCrimson = true;
						break;
					case ItemID.HallowedWaterFountain:
						player.ZoneHallow = Main.hardMode;
						break;
					default:
						break;
				}
			}
		}
	}
	public class CellPhoneAltGlobalItem : GlobalItem
	{
		public override bool AppliesToEntity(Item entity, bool lateInstantiation)
		{
			return entity.netID == ItemID.CellPhone;
		}
		public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.netID == ItemID.CellPhone) return player.lastDeathPostion != Vector2.Zero;
            return base.AltFunctionUse(item, player);
        }
    }
	public class exUtilSystem : ModSystem
	{
		public override void AddRecipes()
		{
			base.AddRecipes();
			
            Recipe.Create(ItemID.CellPhone).
                AddIngredient(ItemID.PDA).
                AddIngredient(ModContent.ItemType<DeathMirror>()).
                AddTile(TileID.TinkerersWorkbench).
                Register();/***/
        }
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