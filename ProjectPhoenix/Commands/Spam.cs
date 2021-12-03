using Microsoft.Xna.Framework;
using ProjectPhoenix.Text;
using ProjectPhoenix.UI;
using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Commands
{
    //note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
    public class LordHelpUsAll : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "Spam";

        public override string Usage
            => "/Spam";

        public override string Description
            => "Spam lol (no arg)";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            string[] sus = new string[1000];
            SmokeyDialogue1.Sus.Keys.CopyTo(sus, 0);
            for (int i = 0; i < SmokeyDialogue1.Sus.Count; i++)
            {
                NewTextBox dialogue = new NewTextBox(Text.SmokeyDialogue1.GetText(sus[i] ),
Text.SmokeyDialogue1.GetTexture("Thinking"),
Text.SmokeyDialogue1.GetVoice(), false, Color.White, true, true);
                dialogue.AddBox();
            }


        }
    }
}