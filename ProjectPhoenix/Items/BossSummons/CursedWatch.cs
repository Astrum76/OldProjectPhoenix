using ProjectPhoenix;
using ProjectPhoenix.NPCs.Bosses.Watcher;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Items.BossSummons
{
	//imported from my tAPI mod because I'm lazy
	public class CursedWatch : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Watch");

			Tooltip.SetDefault("Summons the Watcher");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 20;
			item.rare = ItemRarityID.Cyan;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.Item31;
			item.consumable = true;
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<Watcher>());
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Watcher>());
			//Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			ModRecipe recipeAlt = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldWatch, 1);
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddIngredient(ItemID.SoulofFright, 6);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipeAlt.AddIngredient(ItemID.PlatinumWatch, 1);
			recipeAlt.AddIngredient(ItemID.HallowedBar, 20);
			recipeAlt.AddIngredient(ItemID.SoulofFright, 6);
			recipeAlt.AddTile(TileID.MythrilAnvil);
			recipeAlt.SetResult(this);
			recipeAlt.AddRecipe();
		}
	}
}