using Smod2.API;
using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using System.Linq;
using scp4aiur;

namespace EscapeItems
{
	class EventHandler : IEventHandlerCheckEscape, IEventHandlerSpawn, IEventHandlerRoundRestart
	{
		private Dictionary<string, List<ItemType>> pItems = new Dictionary<string, List<ItemType>>();

		private List<ItemType> getPlayerItems(Player player)
		{
			foreach (KeyValuePair<string, List<ItemType>> entry in pItems.Where(x => x.Key == player.SteamId))
			{
				return entry.Value;
			}
			return null;
		}

		public void OnSpawn(PlayerSpawnEvent ev)
		{
			if (ev.Player.TeamRole.Team == Team.CLASSD || ev.Player.TeamRole.Team == Team.SCIENTIST ||
				ev.Player.TeamRole.Team == Team.NINETAILFOX || ev.Player.TeamRole.Team == Team.CHAOS_INSURGENCY)
			Timing.InTicks(() =>
			{
				List<ItemType> pList = getPlayerItems(ev.Player);
				if (pList != null)
				{
					int c = 0;
					while (ev.Player.GetInventory().Count < 8)
					{
						ev.Player.GiveItem(pList[c]);
						c++;
					}
					for (int i = c; i < pList.Count; i++)
					{
						PluginManager.Manager.Server.Map.SpawnItem(pList[i], ev.Player.GetPosition(), Vector.Zero);
					}
					pItems.Remove(ev.Player.SteamId);
				}
			}, 8);
		}

		public void OnRoundRestart(RoundRestartEvent ev)
		{
			pItems.Clear();
		}

		public void OnCheckEscape(PlayerCheckEscapeEvent ev)
		{
			pItems.Add(ev.Player.SteamId, ev.Player.GetInventory().Select(x => x.ItemType).ToList());
		}
	}
}
