using System;
using System.Collections.Generic;
using VRC.Core;


internal static class DiscordManager
{
	private static DiscordRpc.RichPresence presence;

	private static DiscordRpc.EventHandlers eventHandlers;

	private static bool Enabled = false;

	private static List<string> strings = new List<string>();

	private static int lastMessage = 0;

	public static void Init()
	{
		eventHandlers = default(DiscordRpc.EventHandlers);
		eventHandlers.errorCallback = delegate
		{
		};
		presence.state = "World: Loading...";
		presence.details = "Private VRChat Client";
		presence.largeImageKey = "logo";
		presence.partySize = 0;
		presence.partyMax = 0;
		presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		presence.partyId = "";
		presence.smallImageKey = "";
		presence.largeImageText = "DeepCoreV2";
		try
		{
			DiscordRpc.Initialize("1356002323620434241", ref eventHandlers, autoRegister: true, "438100");
			DiscordRpc.UpdatePresence(ref presence);
			Enabled = true;
		}
		catch
		{
		}
		strings.Add("Modding doesn't have to be toxic");
		strings.Add("QoL Menu by Awooochy");
		strings.Add("Wholesome Modding Enviroment!");
	}

	public static void Update()
	{
		try
		{
			if (Enabled && APIUser.CurrentUser != null)
			{
				if (lastMessage == 4)
				{
					lastMessage = 0;
				}
				presence.details = strings[lastMessage];
				presence.state = "World: " + RoomManager.field_Internal_Static_ApiWorld_0.name;
				string statusDefaultDescriptionDisplayString = APIUser.CurrentUser.statusDefaultDescriptionDisplayString;
				if (statusDefaultDescriptionDisplayString == "Online")
				{
					presence.smallImageKey = "join";
				}
				else if (statusDefaultDescriptionDisplayString == "Ask Me")
				{
					presence.smallImageKey = "ask";
				}
				else
				{
					presence.smallImageKey = "erp";
				}
				DiscordRpc.UpdatePresence(ref presence);
				lastMessage++;
			}
		}
		catch
		{
		}
	}

	public static void OnApplicationQuit()
	{
		DiscordRpc.Shutdown();
	}
}
