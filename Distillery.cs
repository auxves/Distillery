using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ID;
using System.ComponentModel;

namespace Distillery;

using Content.NPCs;

public class Distillery : Mod
{
	public override void PostSetupContent()
	{
		if (ModLoader.TryGetMod("Census", out var census))
		{
			census.Call("TownNPCCondition", ModContent.NPCType<Distiller>(), $"Have a bottle [i:{ItemID.Bottle}] in your inventory");
		}
	}

	public static Mod GetMod(string name) => ModLoader.TryGetMod(name, out var mod) ? mod : null;
}

public class Config : ModConfig
{
	public static Config Instance;

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("NPCs")]
	[DefaultValue(true)]
	public bool EnableDistiller;

	[Header("Mod Integration")]
	[DefaultValue(true)]
	public bool EnableCalamity;
}
