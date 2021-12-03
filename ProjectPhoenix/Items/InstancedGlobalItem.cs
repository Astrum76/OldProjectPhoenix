using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.ID;
namespace ProjectPhoenix.Items
{
    public class InstancedGlobalItem : GlobalItem
    {
        public string originalOwner;
        public byte terrifying;
        public bool examplePersonFreeGift;

        public InstancedGlobalItem()
        {
            originalOwner = "";
            terrifying = 0;
        }

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            //interesting code, and honestly whatever
            InstancedGlobalItem myClone = (InstancedGlobalItem)base.Clone(item, itemClone);
            //myClone.originalOwner = originalOwner;
            myClone.terrifying = terrifying;
            myClone.examplePersonFreeGift = examplePersonFreeGift;
            return myClone;
        }

        public override int ChoosePrefix(Item item, UnifiedRandom rand)
        {
            if ((item.accessory || item.damage > 0) && item.maxStack == 1 && rand.NextBool(30))
            {
                //return mod.PrefixType(rand.Next(2) == 0 ? "Terrifying" : "Truly Terrifying");
            }
            return -1;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            //checks if life crystal or fruit is used with Etheral Heart active and adds 1 to a counter 
            //if not, it's counted but normal behavior continues
            //this does not account for HP bases that are _not_ 100
            //too bad
            if (item.type == ItemID.LifeCrystal)
            {

                if (player.GetModPlayer<PhoenixModPlayer>().totalHearts + player.GetModPlayer<PhoenixModPlayer>().etheralDamage == 15)
                {
                    return false;
                }

                if (player.GetModPlayer<PhoenixModPlayer>().etherHeart)
                {
                    if (player.HeldItem.type == ItemID.LifeCrystal)
                    {
                        player.HeldItem.stack--;
                        if (player.HeldItem.stack <= 0)
                        {
                            player.HeldItem.TurnToAir();

                        }
                        player.GetModPlayer<PhoenixModPlayer>().etheralDamage += 1;
                        Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, player.position);
                        //player.itemAnimation = 45;
                        //player.itemAnimationMax = 45;
                        CombatText.NewText(player.getRect(), Color.DarkRed, "2");

                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < player.inventory.Length; i++)
                        {
                            if (player.inventory[i].type == ItemID.LifeCrystal)
                            {
                                player.inventory[i].stack -= 1;
                                if (player.inventory[i].stack <= 0)
                                {
                                    player.inventory[i].TurnToAir();

                                }
                                player.GetModPlayer<PhoenixModPlayer>().etheralDamage += 1;
                                Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, player.position);
                                //player.itemAnimation = 45;
                                //player.itemAnimationMax = 45;
                                CombatText.NewText(player.getRect(), Color.DarkRed, "2");

                                return false;

                                //break;
                            }


                        }
                    }



                }

                player.GetModPlayer<PhoenixModPlayer>().totalHearts += 1;
                //return base.CanUseItem(item, player);

            }
            if (item.type == ItemID.LifeFruit)
            {

                if (player.GetModPlayer<PhoenixModPlayer>().bonusDamage + player.GetModPlayer<PhoenixModPlayer>().bonusHearts == 20)
                {
                    return false;
                }

                if (player.GetModPlayer<PhoenixModPlayer>().etherHeart)
                {
                    if (item.type == ItemID.LifeCrystal)
                    {
                        item.stack --;
                        if (item.stack <= 0)
                        {
                            item.TurnToAir();

                        }
                        player.GetModPlayer<PhoenixModPlayer>().bonusDamage += 1;
                        Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, player.position);
                        //player.itemAnimation = 45;
                        //player.itemAnimationMax = 45;
                        //Main.NewText("returning");
                        CombatText.NewText(player.getRect(), Color.DarkRed, "1");

                        return false;
                    }
                    else
                    {
                        for (int j = 0; j < player.inventory.Length; j++)
                        {
                            if (player.inventory[j].type == ItemID.LifeFruit)
                            {
                                //Main.NewText("found LF");

                                player.inventory[j].stack -= 1;
                                if (player.inventory[j].stack <= 0)
                                {
                                    player.inventory[j].TurnToAir();
                                    //Main.NewText("air'd");


                                }
                                player.GetModPlayer<PhoenixModPlayer>().bonusDamage += 1;
                                Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, player.position);
                                //player.itemAnimation = 45;
                                //player.itemAnimationMax = 45;
                                //Main.NewText("returning");
                                CombatText.NewText(player.getRect(), Color.DarkRed, "1");

                                return false;

                                //break;
                            }


                        }
                    }
                    //Main.NewText("eh active");



                }

                player.GetModPlayer<PhoenixModPlayer>().bonusHearts += 1;
                //base.CanUseItem(item, player);

            }

            return base.CanUseItem(item, player);
        }
        public override void OnConsumeItem(Item Item, Player player)
        {
            if (Item.type == ItemID.LifeCrystal)
            {
                ModContent.GetInstance<ProjectPhoenix>().Logger.Debug("HC CONSUMED WITH EH" + player.GetModPlayer<PhoenixModPlayer>().etherHeart + " AND TOTAL AT " + player.GetModPlayer<PhoenixModPlayer>().totalHearts + " WITH " + player.GetModPlayer<PhoenixModPlayer>().etheralDamage + " BEING DAMAGE!");

            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!item.social && item.prefix > 0)
            {
                int awesomeBonus = terrifying - Main.cpItem.GetGlobalItem<InstancedGlobalItem>().terrifying;
                if (awesomeBonus > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "PrefixTerrifying", "+" + awesomeBonus + " terror")
                    {
                        isModifier = true
                    };
                    tooltips.Add(line);
                }
            }
            if (originalOwner.Length > 0)
            {
                TooltipLine line = new TooltipLine(mod, "CraftedBy", "Crafted by: " + originalOwner)
                {
                    overrideColor = Color.LimeGreen
                };
                tooltips.Add(line);

                /*foreach (TooltipLine line2 in tooltips)
				{
					if (line2.mod == "Terraria" && line2.Name == "ItemName")
					{
						line2.text = originalOwner + "'s " + line2.text;
					}
				}*/
            }
            if (examplePersonFreeGift)
            {
                tooltips.Add(new TooltipLine(mod, "FreeGift", "This is a free gift from ExampleConfigServer")
                {
                    overrideColor = Color.Magenta
                });
            }
            /*if (ModContent.GetInstance<ExampleConfigClient>().ShowModOriginTooltip)
			{
				foreach (TooltipLine line3 in tooltips)
				{
					if (line3.mod == "Terraria" && line3.Name == "ItemName")
					{
						line3.text = line3.text + (item.modItem != null ? " [" + item.modItem.mod.DisplayName + "]" : "");
					}
				}
			}*/ //dont need rn
        }

        public override void Load(Item item, TagCompound tag)
        {
            originalOwner = tag.GetString("originalOwner");
            examplePersonFreeGift = tag.GetBool(nameof(examplePersonFreeGift));
        }

        public override bool NeedsSaving(Item item)
        {
            return originalOwner.Length > 0 || examplePersonFreeGift;
        }

        public override TagCompound Save(Item item)
        {
            return new TagCompound {
                {"originalOwner", originalOwner},
                {nameof(examplePersonFreeGift), examplePersonFreeGift},
            };
        }

        public override void OnCraft(Item item, Recipe recipe)
        {
            if (item.maxStack == 1)
            {
                originalOwner = Main.LocalPlayer.name;
            }
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(originalOwner);
            writer.Write(terrifying);
            writer.Write(examplePersonFreeGift);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            originalOwner = reader.ReadString();
            terrifying = reader.ReadByte();
            examplePersonFreeGift = reader.ReadBoolean();
        }
    }
}