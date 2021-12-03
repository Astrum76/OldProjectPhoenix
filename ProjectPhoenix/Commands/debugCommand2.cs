using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class DebugCommand2 : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "debugCommand2";

		public override string Usage
			=> "/debugCommand2";

		public override string Description
			=> "Gets EH";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			
			var modPlayer = Main.player[0].GetModPlayer<PhoenixModPlayer>();
            Main.NewText("EthDam: "+ Main.player[0].GetModPlayer<PhoenixModPlayer>().etheralDamage);
            Main.NewText("totalHearts: " + Main.player[0].GetModPlayer<PhoenixModPlayer>().totalHearts);
            Main.NewText("etherHeartAtive " + Main.player[0].GetModPlayer<PhoenixModPlayer>().etherHeart );
            Main.NewText("bonusEthDam " + Main.player[0].GetModPlayer<PhoenixModPlayer>().bonusDamage);
            Main.NewText("bonusHP " + Main.player[0].GetModPlayer<PhoenixModPlayer>().bonusHearts);
			Main.NewText("Current EH values!");
			
		}
	}
}