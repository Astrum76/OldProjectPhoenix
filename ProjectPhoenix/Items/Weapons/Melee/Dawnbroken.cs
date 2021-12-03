using Terraria.ID;
using Terraria.ModLoader;
using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ProjectPhoenix.Dusts;
using ProjectPhoenix.Projectiles.Melee;

namespace ProjectPhoenix.Items.Weapons.Melee
{
	public class Dawnbroken : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dawnbroken"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("...");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.melee = true;
			item.width = 60;
			item.height = 60;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 8;
			item.value = Item.buyPrice(0, 0, 50, 0);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<Firebirths>();
			item.shootSpeed = 8f;

		}

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
				//now drops naturally so cut

		}*/
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			//target.AddBuff(mod.BuffType("Stunned"), 1200, false);
			//cut this dust its placeholder
			for (int i = 0; i <20; i++)
			{
				Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<Dusts.Blood>(), 1.0f, 1.0f);
			}
		}
	}
}