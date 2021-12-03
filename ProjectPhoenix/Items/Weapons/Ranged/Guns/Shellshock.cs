using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectPhoenix.Items.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Items.Weapons.Ranged.Guns

{
    class Shellshock : ModItem

    {
       /*   this code is a fucking mess
        *   it works but like. this gun is a meme and is too weak, and shoots SO MUCH SAND
        *   probs shouldn't make the sand stick around
        *   it works fine for now tho. TODO: fix this
        *   todo: fucking fix the ammo consv
        *   IT SHREDS THE DESTROYER?????????????
        *   
        *   11.1 fixed this! now consumes ammo and code is clean and fresh
        */
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supershotsandgun");
            Tooltip.SetDefault("Sometimes, my genius is almost... frightening.");
            
        }
        public override void SetDefaults()
        {
            item.damage = 60;  //gun damage
            item.ranged = true;   //its a gun so set this to true
            item.width = 80;     //gun image width
            item.height = 24;   //gun image  height
            item.useTime = 20;  //how fast 
            item.useAnimation = 20;
            item.useAmmo = AmmoID.Sand; //for display purposes
            item.useStyle = 5;    //
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = Item.buyPrice(0,10,0,0);
            item.UseSound = SoundID.Item38;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this <- shut up <- you shut up < thats why you shoes raggedy <- thats why yo momma
            item.shootSpeed = 12f;
        }

        public override void AddRecipes()  //How to craft this gun
        {
            ModRecipe craft = new ModRecipe(mod);
            ModRecipe craft2 = new ModRecipe(mod);

            //alt recs
            craft.AddIngredient(ItemID.Sandgun);
            craft.AddIngredient(ItemID.Shotgun, 1);
            craft.AddIngredient(ModContent.ItemType<ArcBar>(), 5);
            craft.AddTile(TileID.MythrilAnvil);
            craft.SetResult(this);
            craft.AddRecipe();




        }
        //possibly redundant
        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            base.PickAmmo(weapon, player, ref type, ref speed, ref damage, ref knockback);
        }
        //useful
        public override bool ConsumeAmmo(Player player)
        {
                 return false;
        }
        //took some guessing
        public override Vector2? HoldoutOffset()
        {
        return new Vector2(-25f, 0f);
        }
        //redundant?
        public override void OnConsumeAmmo(Player player)
        {
          
        }
        //mess
        private int FindAmmo()
        {
            Player player = Main.player[0];
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if(player.inventory[i].stack >= 4)
                {
                    if (player.inventory[i].type == ItemID.SandBlock)
                    {
                        ConsumeSand(player, i);
                        return 0;
                    }

                    if (player.inventory[i].type == ItemID.PearlsandBlock)
                    {
                        ConsumeSand(player, i);
                        return 1;
                    }

                    if (player.inventory[i].type == ItemID.EbonsandBlock)
                    {
                        ConsumeSand(player, i);
                        return 2;
                    }

                    if (player.inventory[i].type == ItemID.CrimsandBlock)
                    {
                        ConsumeSand(player, i);
                        return 3;
                    }
                }
                
            }
            CombatText.NewText(player.getRect(), Color.Red, "Click!");

            return -1;
        }
        //hey megs you can probs use this method that i make on my mod

       /* public static int GetItemStack(this Player player, int id)
        {
            int a = 0;
            foreach (var item in player.inventory) { if (item.type == id) { a += item.stack; } }
            return a;
        }
        public static void DeleteItem(this Player player, int id, int amount = 1)
        {
            int deleted = 0;
            foreach (var aitem in player.inventory)
            {
                if (aitem.type == id && deleted <= amount)
                {
                    for (int i = 0; i < amount + 1; i++)
                    {
                        if (aitem.stack > 1 && deleted <= amount) { aitem.stack--; deleted++; }
                        else if (aitem.stack == 1 && deleted <= amount) { aitem.TurnToAir(); deleted++; }
                        CombatText.NewText(player.getRect(), Color.Pink, deleted);
                    }
                }
            }
            CombatText.NewText(player.getRect(), Color.Red, deleted);
        }*/
        public void ConsumeSand(Player player, int i)
        {

            if (PhoenixModPlayer.GetCurrentAmmoRed(player.inventory[i].type) && Main.rand.Next(11) > 0)
                player.inventory[i].stack -= 4;

                if (player.inventory[i].stack <= 0)
                    player.inventory[i].TurnToAir();
            
           
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
         {
            //Main.NewText(3 + player.GetModPlayer<PlayerMod>().getCurrentAmmoRed());
            int selected = FindAmmo();




            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
            
               type = ProjectileID.SandBallGun;
             int numberProjectiles = 3 + Main.rand.Next(3); // 4 or 5 shots
             for (int i = 0; i < numberProjectiles; i++)
             {
                 Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20)); // 20 degree spread.
                 // If you want to randomize the speed to stagger the projectiles
                 float scale = 1f - (Main.rand.NextFloat() * .4f);
                 perturbedSpeed = perturbedSpeed * scale; 
                
                    switch (selected)
                    {
                        case 0:
                            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.SandBallGun, damage, knockBack, player.whoAmI);

                            break;

                        case 1:
                            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.PearlSandBallGun, damage+5, knockBack, player.whoAmI);

                            break;

                        case 2:
                            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.EbonsandBallGun, damage+5, knockBack, player.whoAmI);

                            break;
                        case 3:
                            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.CrimsandBallGun, damage + 5, knockBack, player.whoAmI);

                            break;

                        default:
                            //no sand fired
                            break;

                    }

                
                    
                    
                    
             }
             return false; // return false because we don't want tmodloader to shoot projectile
         }

    }
}
