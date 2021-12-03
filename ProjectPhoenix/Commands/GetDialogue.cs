using Microsoft.Xna.Framework;
using ProjectPhoenix.Text;
using ProjectPhoenix.UI;
using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class GetDialogue : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "GetDialogue";

		public override string Usage
			=> "/GetDialogue key texture";

		public override string Description
			=> "Opens dialogue from Smokey";

		public override void Action(CommandCaller caller, string input, string[] args)
		{

			NewTextBox dialogue = new NewTextBox(SmokeyDialogue1.GetText(args[0]), SmokeyDialogue1.GetTexture(args[1]), Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
			dialogue.AddBox();


		}
	}
}