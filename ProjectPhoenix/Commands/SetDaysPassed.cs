using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class SetDaysPassed : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "SetDaysPassed";

		public override string Usage
			=> "/SetDaysPassed index";

		public override string Description
			=> "Set days passed";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			ProjectPhoenix.Instance.playboy.daysPassed = int.Parse(args[0]);


		   Main.NewText("Days passed is now " + args[0] + ".");
			
		}
	}
}