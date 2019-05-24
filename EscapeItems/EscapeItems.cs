using Smod2.Attributes;
using scp4aiur;

namespace EscapeItems
{
	[PluginDetails(
	author = "Cyanox",
	name = "Escape Items",
	description = "Allows you to keep your items when you escape.",
	id = "cyan.escapeitems",
	version = "1.1.0",
	SmodMajor = 3,
	SmodMinor = 0,
	SmodRevision = 0
	)]
	public class EscapeItems : Smod2.Plugin
	{
		public override void OnDisable() { }

		public override void OnEnable() { }

		public override void Register()
		{
			Timing.Init(this);
			AddEventHandlers(new EventHandler());
		}
	}
}
