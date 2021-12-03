using Microsoft.Xna.Framework;
using ProjectPhoenix.Items.Crafting;
using ProjectPhoenix.NPCs;
using ProjectPhoenix.NPCs.Unique;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ProjectPhoenix.Items.Weapons.Melee

    //todo: make this a goddamn boomerang 
{
    public class FryingPan : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comically Large Pan");
            Tooltip.SetDefault("<right> to toss a pan");
        }
        public override void SetDefaults()
        {
            item.useTurn = true;    
            item.damage = 65;  //gun damage
            item.knockBack = 15f;
            item.expert = false;
            item.melee = true;   //its a gun so set this to true
            item.width = 52;     //gun image width
            item.height = 20;   //gun image  height
            item.useTime = 60;  //how fast 
            item.useAnimation = 60;
            //item.useAmmo = AmmoID.Bullet;
            item.useStyle = ItemUseStyleID.SwingThrow;    //
            item.noMelee = false; //so the item's animation doesn't do damage
            item.knockBack = 12;
            item.value = 10000;
            item.rare = 4;
            item.autoReuse = false;
            item.shootSpeed = 10;
            item.shoot = 1;
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
        public override bool ConsumeAmmo(Player player)
        {
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
               // Main.PlaySound(SoundID.CoinPickup, player.position);

                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<FryingPanEntity>());
            }
            Main.PlaySound(SoundID.Item37, player.position);
            return false;
        }
    }
}