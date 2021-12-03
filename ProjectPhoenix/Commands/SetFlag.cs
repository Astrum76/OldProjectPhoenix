using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class SetFlag : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "SetFlag";

		public override string Usage
			=> "/SetFlag flag, value";

		public override string Description
			=> "Set a flag";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			
			ProjectPhoenix.Instance.playboy.flags[(args[0])] = int.Parse(args[1]);

			Main.NewText("Flag " + args[0] + " set to " + args[1] + " successfully.");
			
		}
	}
}