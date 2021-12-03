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

namespace ProjectPhoenix.Items.Unique.Player
{
    class CorruptedCore : ModItem
    {
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
			Tooltip.SetDefault("Locks health to current amount\nAll Life Crystals permanantly increase damage by 1\nGrants an immediate damage bonus");
			//Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(12, 5));

		}

		public override void SetDefaults()
		{
			item.width = 64;
			item.height = 64;
			item.maxStack = 1;
			item.value = Item.buyPrice(0,0,0,0);
			item.rare = 11;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.NPCHit9;
			item.consumable = true;
		}
        public override bool CanUseItem(Terraria.Player player)
        {
			return true;
        }

        public override bool UseItem(Terraria.Player player)
        {
            if (!ProjectPhoenix.Instance.playboy.etherHeart)
            {
				ProjectPhoenix.Instance.playboy.etherHeart = true;
				return true;
			}
			return false;
		}
    }

}
