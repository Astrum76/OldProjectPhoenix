using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class debugCommand : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "debugCommand";

		public override string Usage
			=> "/debugCommand";

		public override string Description
			=> "Resets EH bonuses for testing";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			
			var modPlayer = Main.player[0].GetModPlayer<PhoenixModPlayer>();
            Main.player[0].GetModPlayer<PhoenixModPlayer>().etheralDamage = 0;
            Main.player[0].GetModPlayer<PhoenixModPlayer>().totalHearts = 0;
            Main.player[0].GetModPlayer<PhoenixModPlayer>().etherHeart = false;
            Main.player[0].GetModPlayer<PhoenixModPlayer>().bonusDamage = 0;
            Main.player[0].GetModPlayer<PhoenixModPlayer>().bonusHearts = 0;
			Main.NewText("Should have overriten all EH values!");
			
		}
	}
}