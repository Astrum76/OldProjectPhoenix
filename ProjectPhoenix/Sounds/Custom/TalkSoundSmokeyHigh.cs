using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ProjectPhoenix.Sounds.Custom 
{
    class TalkSoundSmokeyHigh : ModSound
    {
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			// By creating a new instance, this ModSound allows for overlapping sounds. Non-ModSound behavior is to restart the sound, only permitting 1 instance.
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * .4f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = 0.4f;
			return soundInstance;
		}
	}
}
