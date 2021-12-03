using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class GetDaysPassed : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "GetDaysPassed";

		public override string Usage
			=> "/GetDaysPassed";

		public override string Description
			=> "Get days passed";

		public override void Action(CommandCaller caller, string input, string[] args)
		{


		   Main.NewText("Days passed is " + ProjectPhoenix.Instance.playboy.daysPassed + ".");
			
		}
	}
}