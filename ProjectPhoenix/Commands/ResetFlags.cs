using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
	//note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
	public class ResetFlags : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "ResetFlags";

		public override string Usage
			=> "/ResetFlags";

		public override string Description
			=> "ResetFlags";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			string[] sus = new string[100];
			ProjectPhoenix.Instance.playboy.flags.Keys.CopyTo(sus, 0);
			for(int i = 0; i < ProjectPhoenix.Instance.playboy.flags.Count; i++)
			{
				ProjectPhoenix.Instance.playboy.flags[sus[i]] = 0;
			}
			Main.NewText("Cleared " + sus.Length + " values");

		}
	}
}