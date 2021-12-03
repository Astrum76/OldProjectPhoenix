using Microsoft.Xna.Framework;
using ProjectPhoenix.UI;
using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class TextCommand : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "textCommand";

		public override string Usage
			=> "/textCommand text";

		public override string Description
			=> "Opens a textbox";

		public override void Action(CommandCaller caller, string input, string[] args)
		{

			NewTextBox dialogue = new NewTextBox(input.Substring(12,input.Length-12), "ProjectPhoenix/UI/Port/Smokey/SmokeyDefault", Text.SmokeyDialogue1.GetVoice(), false, Color.White, true,true);
			dialogue.AddBox();


		}
	}
}