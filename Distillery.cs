using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace Distillery;

using Content.NPCs;

public class Distillery : Mod
{
	public override void PostSetupContent()
	{
		if (ModLoader.TryGetMod("Census", out var census))
		{
			census.Call("TownNPCCondition", ModContent.NPCType<Distiller>(), Language.GetTextValue("Mods.Distillery.Census.Distiller"));
		}
	}

	public static Mod? GetMod(string name) => ModLoader.TryGetMod(name, out var mod) ? mod : null;
}

public class Config : ModConfig
{
#nullable disable
	public static Config Instance;

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("$Mods.Distillery.Config.NPCs")]
	[Label("$Mods.Distillery.Config.EnableDistiller.Label")]
	[Tooltip("$Mods.Distillery.Config.EnableDistiller.Tooltip")]
	[DefaultValue(true)]
	public bool EnableDistiller;

	[Header("$Mods.Distillery.Config.ModIntegration")]
	[Label("$Mods.Distillery.Config.EnableCalamity.Label")]
	[Tooltip("$Mods.Distillery.Config.EnableCalamity.Tooltip")]
	[DefaultValue(true)]
	public bool EnableCalamity;
}
