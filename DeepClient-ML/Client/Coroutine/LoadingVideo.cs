using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using ReMod.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace DeepCore.Client.Coroutine
{
    internal class LoadingVideo
    {
        public static bool Videoplayer = true;
        public static bool BigVideoLoadingscreen = false;
        public static bool isvideo = true;
        public static RenderTexture rendert;
        public static AudioSource getauds;
        public static float bpm = 105f;
        public static float timePerBeat;
        public static float nextBeatTime;
        public static AudioSource audio;
        public static float threshold = 0.04f;
        private Vector3 initialScale;
        public static float maxIntensity = 10f;
        public static float minIntensity = 0.1f;
        public static float intensityMultiplier = 0.3f;
        public static float[] _spectrum;
        public static float _lastBeatTime;
        public static bool _isBeat;
        public static float comp1;
        public static float comp2;

        public static IEnumerator LoadVideo()
        {
            {
                Image component = GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle").transform.GetComponent<Image>();
                Image image = UnityEngine.Object.Instantiate<Image>(component, component.transform.parent);
                image.gameObject.name = "Video";
                Transform transform = GameObject.Find("MenuContent/Popups/LoadingPopup/Video/Text").transform;
                transform.GetComponent<TextMeshProUGUI>().text = "";
                UnityEngine.Object.DestroyImmediate(transform);
                if (false)
                {
                }
                else
                {
                    image.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f, 0f);
                    image.transform.localPosition = new Vector3(-1f, 220.4001f, 500f);
                    image.GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 400f);
                    Image image3 = GameObject.Instantiate<Image>(image, image.transform);
                    image3.name = "Backround";
                    image3.transform.localPosition = new Vector3(0f, 0f, 0f);
                    image3.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                    GameObject.Find("MenuContent/Popups/LoadingPopup/Video/Backround").SetActive(false);
                    GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.position = new Vector3(-0.0061f, 2.0535f, 1.5441f); ;
                    GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.localPosition = new Vector3(-0.9868f, 314.19f, 500.0472f);
                    GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.localScale = new Vector3(14.5228f, 2.825f, 2f);

                }
                if (isvideo)
                {
                    GameObject gameObject = GameObject.Find("MenuContent/Popups/LoadingPopup/Video").transform.gameObject;
                    GameObject.Destroy(gameObject.GetComponent<Button>());
                    GameObject.Destroy(gameObject.transform.Find("Backround").gameObject.GetComponent<Button>());
                    gameObject.GetComponent<Image>().sprite = null;
                    gameObject.AddComponent<VideoPlayer>();
                    VideoPlayer vidcomp = gameObject.GetComponent<VideoPlayer>();
                    vidcomp.isLooping = true;
                    rendert = new RenderTexture(512, 512, 16);
                    rendert.Create();
                    Material material = new Material(Shader.Find("Standard"));
                    material.color = default(Color);
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", Color.white);
                    material.SetTexture("_EmissionMap", rendert);
                    gameObject.GetComponent<Image>().material = material;
                    vidcomp.targetTexture = rendert;
                    vidcomp.url = MelonUtils.GameDirectory + "\\DeepClient\\LoadingVid.mp4";
                    while (GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>() == null)
                    {
                        yield return null;
                    }
                    GameObject.Find("MenuContent/Popups/LoadingPopup/Video").AddComponent<AudioSource>();
                    AudioSource component2 = GameObject.Find("MenuContent/Popups/LoadingPopup/Video").GetComponent<AudioSource>();
                    component2.clip = null;
                    vidcomp.audioOutputMode = (VideoAudioOutputMode)1;
                    vidcomp.EnableAudioTrack(0, true);
                    vidcomp.SetTargetAudioSource(0, component2);
                    vidcomp.Stop();
                    component2.Stop();
                    vidcomp.Play();
                    component2.Play();
                    GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound").SetActive(false);
                    vidcomp = null;
                }
            }
            yield return null;
            yield break;
        }
    }
}
