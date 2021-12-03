using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Items.Mining
{
	//ported from my tAPI mod because I don't want to make more artwork
	public class LightDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pure Drill");

		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.melee = true;
			item.width = 20;
			item.height = 12;
			item.useTime = 9;
			item.useAnimation = 19;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.pick = 99;
			item.tileBoost--;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3;
			item.value = Item.buyPrice(0, 1, 20, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<Projectiles.Mining.LightDrill>();
			item.shootSpeed = 20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			ModRecipe recipeAlt = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 25);
			recipe.AddIngredient(ItemID.PurificationPowder, 20);
			recipe.AddIngredient(ItemID.ShadowScale, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipeAlt.AddIngredient(ItemID.CrimtaneBar, 25);
			recipeAlt.AddIngredient(ItemID.PurificationPowder, 20);
			recipeAlt.AddIngredient(ItemID.TissueSample, 6);
			recipeAlt.AddTile(TileID.Anvils);
			recipeAlt.SetResult(this);
			recipeAlt.AddRecipe();




		}
	}

}