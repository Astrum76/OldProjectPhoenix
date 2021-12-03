using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ProjectPhoenix.NPCs.Unique;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectPhoenix
{
    public class PlayerHelper
    {

        ///<summary>Method takes a player and an item to find. Returns index of item, or -1 if none found.</summary>
        public static int CheckForItem(int itemID, Player player)
        {
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if (player.inventory[i].type == itemID)
                {
                    return i;
                }

            }
            return -1;
        }
    }
}
