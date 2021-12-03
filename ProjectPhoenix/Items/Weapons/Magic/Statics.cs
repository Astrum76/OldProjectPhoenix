using ProjectPhoenix.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ProjectPhoenix.Items.Weapons.Magic
{
	public class Statics : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Static Blaster"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Totally stuns enemies\nIgnores defense\nFor Arzon");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 50;
			item.magic = true;
			item.mana = 200;
			item.width = 32;
			item.height = 32;
			item.useTime = 70;
			item.useAnimation = 70;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 3;
			item.value = Item.buyPrice(0, 50, 0, 0);
			item.rare = 8;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/StaticBlast"); item.autoReuse = true; //auto sound load
			item.shoot = ModContent.ProjectileType<Projectiles.Magic.StaticBlast>();
			item.shootSpeed = 1f; //vel of proj
		}
		public override void HoldItem(Player player)
		{
			base.HoldItem(player);
			player.armorPenetration += 9999;
			//allows for armor piercing
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ElectrosphereLauncher, 1);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.SoulofSight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

