using Terraria.ID;
using Terraria.ModLoader;
using Terraria;


namespace ProjectPhoenix.Items.Crafting
{
	public class CopperWire : ModItem
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("This is a modded item.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 99;
			item.value = 150 / 8;
			item.rare = ItemRarityID.Blue;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperBar);
			recipe.SetResult(this, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.AddRecipe();
		
		}
	}
}