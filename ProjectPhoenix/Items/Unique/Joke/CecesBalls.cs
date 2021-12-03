using Microsoft.Xna.Framework;
using ProjectPhoenix.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix.Items.Unique.Joke
{
    class CecesBalls : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cece's Balls");
			Tooltip.SetDefault("Supposedly provide musical gifts if consumed.");
			//Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(12, 5));

		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 1;
			item.value = Item.buyPrice(0,0,0,0);
			item.rare = -1;
			item.useAnimation = 60;
			item.useTime = 60;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.NPCHit9;
			item.consumable = true;
		}
        public override bool CanUseItem(Terraria.Player player)
        {
			return true;
        }

        public override bool UseItem(Terraria.Player player)
        {

			//ProjectPhoenix.Instance.playboy.etherHeart = true;
			return false;
		}
    }

}
