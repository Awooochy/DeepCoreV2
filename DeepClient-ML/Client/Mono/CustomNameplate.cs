
using System;
using MelonLoader;
using TMPro;
using UnityEngine;
using VRC;
using DeepCore.Client.Misc;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DeepCore.Client.Mono
{
	public class CustomNameplate : MonoBehaviour, IDisposable
{
	private TextMeshProUGUI _statsText;
	public Player Player;
	public bool OverRender = true;
	public bool Enabled = true;
	private byte frames;
	private byte ping;
	private int noUpdateCount = 0;
	private int skipX = 50;
	public CustomNameplate(IntPtr ptr)
		: base(ptr)
	{
	}

	public void Dispose()
	{
		((TMP_Text)_statsText).text = null;
		_statsText.OnDisable();
		((Behaviour)this).enabled = false;
	}

	private void Start()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (!((Behaviour)this).enabled)
		{
			return;
		}
		try
		{
			PlayerNameplate field_Public_PlayerNameplate_ = Player._vrcplayer.field_Public_PlayerNameplate_0;
			Transform transform = field_Public_PlayerNameplate_.field_Public_GameObject_5.transform;
			TextMeshProUGUI component = ((Component)transform.Find("Trust Text")).GetComponent<TextMeshProUGUI>();
			Transform val = UnityEngine.Object.Instantiate<Transform>(transform, transform);
			val.parent = field_Public_PlayerNameplate_.field_Public_GameObject_0.transform;
			((Component)val).gameObject.SetActive(true);
			val.localPosition = new Vector3(0f, 50f, 0f);
			_statsText = ((Component)val.Find("Trust Text")).GetComponent<TextMeshProUGUI>();
			ImageThreeSlice component2 = ((Component)val).GetComponent<ImageThreeSlice>();
			((Graphic)component2).color = PlayerUtil.Trusted();
			((TMP_Text)_statsText).color = Color.white;
			if (OverRender && Enabled)
			{
				((TMP_Text)_statsText).isOverlay = true;
				((TMP_Text)component).isOverlay = true;
			}
			else
			{
				((TMP_Text)_statsText).isOverlay = false;
				((TMP_Text)component).isOverlay = false;
			}
			((Component)val.Find("Trust Icon")).gameObject.SetActive(false);
			((Component)val.Find("Performance Icon")).gameObject.SetActive(false);
			((Component)val.Find("Performance Text")).gameObject.SetActive(false);
			((Component)val.Find("Friend Anchor Stats")).gameObject.SetActive(false);
		}
		catch (Exception ex)
		{
			MelonLogger.Msg(ex.Message);
			DeepConsole.LogConsole("NAMEPLATE SYSTEM", "NAMEPLATES FUCKED");
		}
	}

	private void Update()
	{
		if (!Enabled)
		{
			return;
		}
		try
		{
			if (frames == Player._playerNet.field_Private_Byte_0 && ping == Player._playerNet.field_Private_Byte_1)
			{
				noUpdateCount++;
			}
			else
			{
				noUpdateCount = 0;
			}
			frames = Player._playerNet.field_Private_Byte_0;
			ping = Player._playerNet.field_Private_Byte_1;
			if (skipX >= 50)
			{
				string text = "<color=green>Stable</color>";
				if (noUpdateCount > 30)
				{
					text = "<color=yellow>Lagging</color>";
				}
				if (noUpdateCount > 150)
				{
					text = "<color=red>Crashed</color>";
				}
				if (PlayerUtil.knownBlocks.Contains(Player.field_Private_APIUser_0.displayName))
				{
					((TMP_Text)_statsText).text = "[<color=red>BLOCKED YOU</color>] | [" + Player.GetPlatform() + "] | [" + Player.GetAvatarStatus() + "] |" + (Player.GetIsMaster() ? " | [<color=#0352ff>HOST</color>] |" : "") + " [" + text + "] | [FPS: " + Player.GetFramesColord() + "] | [Ping: " + Player.GetPingColord() + "]  " + (Player.ClientDetect() ? " | [<color=red>ClientUser</color>]" : "");
				}
				else
				{
					((TMP_Text)_statsText).text = "[" + Player.GetPlatform() + "] | [" + Player.GetAvatarStatus() + "] |" + (Player.GetIsMaster() ? " | [<color=#0352ff>HOST</color>] |" : "") + " [" + text + "] | [FPS: " + Player.GetFramesColord() + "] | [Ping: " + Player.GetPingColord() + "]  " + (Player.ClientDetect() ? " | [<color=red>ClientUser</color>]" : "");
				}
				skipX = 0;
			}
			else
			{
				skipX++;
			}
		}
		catch (Exception ex)
		{
			MelonLogger.Msg(ex.Message);
		}
	}
}
}






