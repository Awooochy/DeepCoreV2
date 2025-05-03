
using System;
using System.Globalization;
using MelonLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DeepCore.Client.Misc;
using VRC;

namespace DeepCore.Client.Mono
{
	public class CustomNameplateAccountAge : MonoBehaviour, IDisposable
	{
		private TextMeshProUGUI _statsText;

		public Player Player;

		public string playerAge;

		public bool OverRender;

		public bool Enabled = true;

		private int skipX = 20000;

		public CustomNameplateAccountAge(IntPtr ptr)
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
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			if (!((Behaviour)this).enabled)
			{
				return;
			}

			try
			{
				PlayerNameplate field_Public_PlayerNameplate_ = Player._vrcplayer.field_Public_PlayerNameplate_0;
				Transform transform = field_Public_PlayerNameplate_.field_Public_GameObject_5.transform;
				Transform val = UnityEngine.Object.Instantiate<Transform>(transform, transform);
				val.parent = field_Public_PlayerNameplate_.field_Public_GameObject_0.transform;
				((Component)val).gameObject.SetActive(true);
				val.localPosition = new Vector3(0f, 100f, 0f);
				_statsText = ((Component)val.Find("Trust Text")).GetComponent<TextMeshProUGUI>();
				((TMP_Text)_statsText).color = Color.white;
				ImageThreeSlice component = ((Component)val).GetComponent<ImageThreeSlice>();
				((Graphic)component).color = PlayerUtil.Trusted();
				if (OverRender && Enabled)
				{
					((TMP_Text)_statsText).isOverlay = true;
				}
				else
				{
					((TMP_Text)_statsText).isOverlay = false;
				}

				((Component)val.Find("Trust Icon")).gameObject.SetActive(false);
				((Component)val.Find("Performance Icon")).gameObject.SetActive(false);
				((Component)val.Find("Performance Text")).gameObject.SetActive(false);
				((Component)val.Find("Friend Anchor Stats")).gameObject.SetActive(false);
			}
			catch (Exception ex)
			{
				MelonLogger.Msg(ex.Message);
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
				if (skipX >= 20000)
				{
					if (!string.IsNullOrEmpty(playerAge))
					{
						try
						{
							DateTime dateTime = DateTime.ParseExact(playerAge.Trim(), "yyyy-MM-dd",
								CultureInfo.InvariantCulture);
							string arg = OtherUtil.ToAgeString(dateTime);
							((TMP_Text)_statsText).text = $"{dateTime:dddd MMMM yyyy} [Account Age: {arg}]";
							skipX = 0;
							return;
						}
						catch
						{
							return;
						}
					}
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




