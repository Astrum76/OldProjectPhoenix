using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPhoenix.Items;
using Terraria;
using Terraria.ModLoader;

namespace ProjectPhoenix.Prefixes
{
    class Terrifying : ModPrefix
    {
		private readonly byte _power;

		// see documentation for vanilla weights and more information
		// note: a weight of 0f can still be rolled. see CanRoll to exclude prefixes.
		// note: if you use PrefixCategory.Custom, actually use ChoosePrefix instead, see ExampleInstancedGlobalItem
		public override float RollChance(Item item)
			=> 5f;

		// determines if it can roll at all.
		// use this to control if a prefixes can be rolled or not
		public override bool CanRoll(Item item)
			=> true;

		// change your category this way, defaults to Custom
		public override PrefixCategory Category
			=> PrefixCategory.AnyWeapon;

		public Terrifying()
		{
		}

		public Terrifying(byte power)
		{
			_power = power;
		}

		// Allow multiple prefix autoloading this way (permutations of the same prefix)
		public override bool Autoload(ref string name)
		{
			if (!base.Autoload(ref name))
			{
				return false;
			}

			mod.AddPrefix("Terrifying", new Terrifying(1));
			mod.AddPrefix("TrulyTerrifying", new Terrifying(2));
			return false;
		}

		public override void Apply(Item item)
			=> item.GetGlobalItem<InstancedGlobalItem>().terrifying = _power;

		public override void ModifyValue(ref float valueMult)
		{
			float multiplier = 1f + 0.05f * _power;
			valueMult *= multiplier;
		}
	}

}
