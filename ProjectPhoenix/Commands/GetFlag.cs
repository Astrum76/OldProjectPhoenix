using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class GetFlag : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "GetFlag";

		public override string Usage
			=> "/GetFlag index";

		public override string Description
			=> "Get a flag";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			

			Main.NewText("Flag " + args[0] + " is " + ProjectPhoenix.Instance.playboy.flags[(args[0])] + ".");
			
		}
	}
}