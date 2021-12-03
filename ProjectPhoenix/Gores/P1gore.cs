using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ReplaceThisName.Gores
{
	public class P1gore : ModGore
	{
		public override void OnSpawn(Gore gore)
		{
			gore.numFrames = 1;
			gore.behindTiles = true;
			gore.timeLeft = Gore.goreTime * 6;
		}

		public override bool Update(Gore gore)
		{
		
			return true;
		}
	}
}