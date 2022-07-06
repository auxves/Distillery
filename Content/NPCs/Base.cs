using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;

namespace Distillery.Content.NPCs;

public record ShopItem(int Type, int Price);

public abstract class Base : ModNPC
{
	public abstract object DefaultShop { get; }

	public object CurrentShop { get; set; }

	public override string Texture => $"Distillery/Assets/Textures/NPCs/{GetType().Name}";

	public override void SetDefaults()
	{
		NPC.townNPC = true;
		NPC.friendly = true;
		NPC.width = 18;
		NPC.height = 40;
		NPC.aiStyle = NPCAIStyleID.Passive;
		NPC.damage = 10;
		NPC.defense = 15;
		NPC.lifeMax = 250;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.knockBackResist = 0.5f;

		AnimationType = NPCID.Merchant;
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[Type] = 21;
	}

	public override void SetupShop(Chest shop, ref int nextSlot)
	{
		nextSlot = 0;

		var items = GetShopItems(CurrentShop);

		foreach (var item in items)
		{
			if (item == null) continue;

			var slot = shop.item[nextSlot++];

			slot.SetDefaults(item.Type);
			slot.shopCustomPrice = item.Price;
		}
	}

	public override void SetChatButtons(ref string button, ref string button2) => button = Language.GetTextValue("Mods.Distillery.Button.Shop");

	public override void OnChatButtonClicked(bool firstButton, ref bool shop)
	{
		if (firstButton)
		{
			var npc = NPCLoader.GetNPC(Type) as Base;
			npc.CurrentShop = npc.DefaultShop;

			shop = true;
		}
	}

	public abstract List<ShopItem> GetShopItems(object type);
}

public abstract class PickerItem : ModItem
{
	public abstract int TextureSource { get; }
	public abstract object Shop { get; }

	public override string Texture => ItemLoader.GetItem(TextureSource)?.Texture ?? $"Terraria/Images/Item_{TextureSource}";

	public override void SetStaticDefaults() => Tooltip.SetDefault("{$Mods.Distillery.ItemTooltip.PickerItem}");

	public override void SetDefaults()
	{
		Item.rare = ItemRarityID.Orange;
	}
}

public class PickerHandler : ModPlayer
{
	public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
	{
		var shop = Main.instance.shop[Main.npcShop];

		if (vendor.ModNPC is not Base) return;
		if (item.ModItem is not PickerItem picker) return;

		var npc = NPCLoader.GetNPC(vendor.type) as Base;
		npc.CurrentShop = picker.Shop;

		shop.SetupShop(vendor.type);

		Main.mouseItem.TurnToAir();
	}
}
