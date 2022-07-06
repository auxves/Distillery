using Terraria.ModLoader;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using System.Linq;

namespace Distillery.Content.NPCs;

using Common.Integrations;

[AutoloadHead]
public class Distiller : Base
{
	public override object DefaultShop => ShopType.Picker;

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();

		NPC.Happiness
			.SetBiomeAffection<JungleBiome>(AffectionLevel.Love)
			.SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
			.SetBiomeAffection<DesertBiome>(AffectionLevel.Hate);
	}

	public override bool CanTownNPCSpawn(int numTownNPCs, int money) => Config.Instance.EnableDistiller &&
		Main.player.Any(player => player.active && player.inventory.Any(item => item.type == ItemID.Bottle));

	public override List<ShopItem> GetShopItems(object type) => (ShopType) type switch
	{
		ShopType.Picker => PickerItems(),
		ShopType.VanillaBuffs => GetVanillaBuffs(),
		ShopType.CalamityBuffs => GetCalamityBuffs(),
		ShopType.UtilityPotions => GetUtilityPotions(),
	};

	static List<ShopItem> PickerItems() => new()
	{
		new ShopItem(ModContent.ItemType<VanillaBuffsPicker>(), 0),
		Calamity.Present ? new ShopItem(ModContent.ItemType<CalamityBuffsPicker>(), 0) : null,
		new ShopItem(ModContent.ItemType<UtilityPotionsPicker>(), 0),
	};

	static List<ShopItem> GetVanillaBuffs() => new()
	{
		new ShopItem(ItemID.SwiftnessPotion, 5000),
		new ShopItem(ItemID.IronskinPotion, 10000),
		new ShopItem(ItemID.RegenerationPotion, 10000),
		new ShopItem(ItemID.MiningPotion, 7500),
		new ShopItem(ItemID.BuilderPotion, 5000),
		new ShopItem(ItemID.ArcheryPotion, 15000),
		new ShopItem(ItemID.SummoningPotion, 7500),
		new ShopItem(ItemID.EndurancePotion, 10000),
		new ShopItem(ItemID.HeartreachPotion, 10000),
		new ShopItem(ItemID.AmmoReservationPotion, 7500),
		new ShopItem(ItemID.ThornsPotion, 5000),
		new ShopItem(ItemID.WrathPotion, 25000),
		new ShopItem(ItemID.RagePotion, 25000),
		new ShopItem(ItemID.LifeforcePotion, 25000),
		new ShopItem(ItemID.InfernoPotion, 15000),
		new ShopItem(ItemID.ShinePotion, 5000),
		new ShopItem(ItemID.NightOwlPotion, 5000),
		new ShopItem(ItemID.WarmthPotion, 20000),
		new ShopItem(ItemID.SpelunkerPotion, 20000),
		new ShopItem(ItemID.HunterPotion, 10000),
		new ShopItem(ItemID.TrapsightPotion, 10000),
		new ShopItem(ItemID.FlipperPotion, 5000),
		new ShopItem(ItemID.GillsPotion, 5000),
		new ShopItem(ItemID.InvisibilityPotion, 5000),
		new ShopItem(ItemID.WaterWalkingPotion, 5000),
		new ShopItem(ItemID.ObsidianSkinPotion, 10000),
		new ShopItem(ItemID.FeatherfallPotion, 7500),
		new ShopItem(ItemID.GravitationPotion, 20000),
		new ShopItem(ItemID.MagicPowerPotion, 15000),
		new ShopItem(ItemID.ManaRegenerationPotion, 5000),
		new ShopItem(ItemID.TitanPotion, 7500),
		new ShopItem(ItemID.BattlePotion, 10000),
		new ShopItem(ItemID.CalmingPotion, 10000),
		new ShopItem(ItemID.FishingPotion, 10000),
		new ShopItem(ItemID.SonarPotion, 10000),
		new ShopItem(ItemID.CratePotion, 10000),
		new ShopItem(ItemID.GenderChangePotion, 15000),
	};

	static List<ShopItem> GetCalamityBuffs() => new()
	{
		new ShopItem(Calamity.ItemType("SupremeHealingPotion"), 15000)
	};

	static List<ShopItem> GetUtilityPotions() => new()
	{
		new ShopItem(ItemID.Bottle, 1_00),
		new ShopItem(ItemID.BottledWater, 2_00),

		// Healing Potions
		new ShopItem(ItemID.LesserHealingPotion, 5_00),
		NPC.downedBoss1 ? new ShopItem(ItemID.HealingPotion, 10_00) : null,
		Main.hardMode ? new ShopItem(ItemID.GreaterHealingPotion, 1_00_00) : null,
		NPC.downedMoonlord ? new ShopItem(ItemID.SuperHealingPotion, 2_50_00) : null,
		Calamity.Downed("providence") ? new ShopItem(Calamity.ItemType("SupremeHealingPotion"), 25_00_00) : null,
		Calamity.Downed("providence") ? new ShopItem(Calamity.ItemType("OmegaHealingPotion"), 50_00_00) : null,

		// Mana Potions
		new ShopItem(ItemID.LesserManaPotion, 2_50),
		NPC.downedBoss1 ? new ShopItem(ItemID.ManaPotion, 5_00) : null,
		Main.hardMode ? new ShopItem(ItemID.GreaterManaPotion, 50_00) : null,
		NPC.downedMechBossAny ? new ShopItem(ItemID.SuperManaPotion, 1_00_00) : null,
		Calamity.Present && NPC.downedMoonlord ? new ShopItem(Calamity.ItemType("SupremeManaPotion"), 15_00_00) : null,

		new ShopItem(ItemID.RecallPotion, 25_00),
		new ShopItem(ItemID.WormholePotion, 50_00),
		Main.hardMode ? new ShopItem(ItemID.TeleportationPotion, 50_00) : null,
	};

	public override List<string> SetNPCNameList() => new()
	{
		"Hermes",
		"Thomas",
		"Edward",
		"Dmitri",
		"Vince",
		"Noel",
	};

	enum ShopType
	{
		Picker,
		VanillaBuffs,
		CalamityBuffs,
		UtilityPotions
	}

	sealed class VanillaBuffsPicker : PickerItem
	{
		public override int TextureSource => ItemID.SwiftnessPotion;
		public override object Shop => ShopType.VanillaBuffs;
	}

	sealed class CalamityBuffsPicker : PickerItem
	{
		public override int TextureSource => Calamity.Present ? Calamity.ItemType("SupremeHealingPotion") : ItemID.SwiftnessPotion;
		public override object Shop => ShopType.CalamityBuffs;
	}

	sealed class UtilityPotionsPicker : PickerItem
	{
		public override int TextureSource => ItemID.WormholePotion;
		public override object Shop => ShopType.UtilityPotions;
	}
}
