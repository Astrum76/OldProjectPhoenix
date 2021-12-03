using ProjectPhoenix.NPCs.Bosses.Watcher;
using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{	public class WatcherPhase : ModCommand
	{
		public override CommandType Type
			=> CommandType.Chat;

		public override string Command
			=> "watcherPhase";

		public override string Usage
			=> "/watcherPhase";

		public override string Description
			=> "watcher phase";

		public override void Action(CommandCaller caller, string input, string[] args)
		{

          
            NPC targetNPC;
			for (int j = 0; j < Main.npc.Length - 1; j++)
			{
				targetNPC = Main.npc[j];
				// Main.NewText("lol");
				if (targetNPC.type == ModContent.NPCType<Watcher>())
				{
					(Main.npc[(j)].modNPC as Watcher).setBossMode3(int.Parse(args[0]));


				}

			}

        }
	}
}