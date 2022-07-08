using Terraria.ModLoader;

namespace Distillery.Common.Integrations;

public class Calamity
{
	public static readonly Mod? Mod = Config.Instance.EnableCalamity ? Distillery.GetMod("CalamityMod") : null;

	public static bool Present => Mod is not null;

	public static bool Downed(string boss) => (bool?) Mod?.Call("GetBossDowned", boss) ?? false;

	public static int ItemType(string name) => Mod!.Find<ModItem>(name).Type;
}
