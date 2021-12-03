using Terraria.ID;
using Terraria.ModLoader;
using ProjectPhoenix;
using Terraria;
using Terraria.DataStructures;

namespace ProjectPhoenix.Items.Crafting
{
	public class ArcBar : ModItem
	{
		//todo:placeable
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Cool to the touch.");
			// ticksperframe, frameCount
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(3, 20));
			ItemID.Sets.ItemIconPulse[item.type] = false;
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 99;
			item.value = 150 *2;
			item.rare = 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 5);
			recipe.AddIngredient(ItemID.GoldBar, 1);
			recipe.SetResult(this, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ModContent.ItemType<CopperWire>(), 5);
			recipe2.AddIngredient(ItemID.PlatinumBar, 1);
			recipe2.SetResult(this, 1);
			recipe2.AddTile(TileID.Anvils);
			recipe2.AddRecipe();

			ModRecipe recipe3 = new ModRecipe(mod);
			recipe3.AddIngredient(ModContent.ItemType<TinWire>(), 5);
			recipe3.AddIngredient(ItemID.GoldBar, 1);
			recipe3.SetResult(this, 1);
			recipe3.AddTile(TileID.Anvils);
			recipe3.AddRecipe();

			ModRecipe recipe4 = new ModRecipe(mod);
			recipe4.AddIngredient(ModContent.ItemType<TinWire>(), 5);
			recipe4.AddIngredient(ItemID.PlatinumBar, 1);
			recipe4.SetResult(this, 1);
			recipe4.AddTile(TileID.Anvils);
			recipe4.AddRecipe();

		}
	}
}