using Terraria.ID;
using Terraria.ModLoader;
//cut weapon. literally no use. kept around bc im sentimental

namespace ProjectPhoenix.Items.Weapons.Melee
{
	public class unused : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Firebirth");
			Tooltip.SetDefault("This is a modded sword.");
			//This is a cut weapon
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Firebirths");
			item.shootSpeed = 8f;
		}

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}
