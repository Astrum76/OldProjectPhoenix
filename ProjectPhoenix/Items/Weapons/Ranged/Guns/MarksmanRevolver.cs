using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectPhoenix.Items.Crafting;
using ProjectPhoenix.NPCs;
using ProjectPhoenix.NPCs.Unique;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ProjectPhoenix.Items.Weapons.Ranged.Guns
{
    public class MarksmanRevolver : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heaven's Piercer");
            Tooltip.SetDefault("<right> to toss a coin");
        }
        public override void SetDefaults()
        {
            item.damage = 70;  //gun damage
            item.expert = true;
            item.ranged = true;   //its a gun so set this to true
            item.width = 52;     //gun image width
            item.height = 20;   //gun image  height
            item.useTime = 30;  //how fast 
            item.useAnimation = 30;
            item.useAmmo = AmmoID.Bullet;
            item.useStyle = 5;    //
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7f;
            item.value = 10000;
            item.rare = 7;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 10;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7F, 0f);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()  //How to craft this gun
        {
            ModRecipe craft = new ModRecipe(mod);
            ModRecipe craft2 = new ModRecipe(mod);

            //alt recs
            if (Main.expertMode)
            {
                craft.AddIngredient(ItemID.Revolver);
                craft.AddIngredient(ItemID.SoulofSight, 1);
                craft.AddIngredient(ModContent.ItemType<ArcBar>(), 5);
                craft.AddTile(TileID.MythrilAnvil);
                craft.SetResult(this);
                craft.AddRecipe();
            }





        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 20;
                item.useAnimation = 20;
            }
            else
            {

                item.useTime = 30;
                item.useAnimation = 30;
            }
            if (!(NPC.CountNPCS(ModContent.NPCType<MarksmanCoin>()) < 2)&&player.altFunctionUse == 2)return false;
            return base.CanUseItem(player);
        }
        public override bool ConsumeAmmo(Player player)
        {
            return true;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (Main.LocalPlayer.altFunctionUse == 2)
            {
                return false;
            }
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                if (NPC.CountNPCS(ModContent.NPCType<MarksmanCoin>()) < 2)
                {

                    Main.PlaySound(SoundID.CoinPickup, player.position);

                    NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<MarksmanCoin>());
                }

                return false;
            }

            Main.PlaySound(SoundID.Item41, player.position);
            return true;
        }
    }
}