using System.Collections;
using System.IO;
using MelonLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
    internal class CustomLoadingScreen
    {
        private static Texture2D _skyCubeTexture;
        private static Color _primaryColor = new Color(0.25f, 0f, 0f, 1f);
        private static Color _primaryColorS = new Color(0.25f, 0f, 0.25f, 1f);
        private static Color _highlightColor = new Color(0.547f, 0f, 0f, 1f);
        private static Color _pressedColor = new Color(0.247f, 0f, 0f, 1f);
        public static IEnumerator Init()
        {
            while (GameObject.Find("LoadingBackground_TealGradient_Music") == null)
            {
                yield return null;
            }
            GameObject.Find("PlayerDisplay/BlackFade/inverted_sphere").SetActive(false);
            GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetTexture("_Tex",_skyCubeTexture);
            GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetColor("_Tint", _primaryColorS);
            SetupParticleSystem(GameObject.Find("LoadingBackground_TealGradient_Music/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>());
            while (GameObject.Find("MenuContent/Popups/LoadingPopup") == null)
            {
                yield return null;
            }
            while (GameObject.Find("MenuContent/Popups/LoadingPopup") == null)
            {
                yield return null;
            }
            if (File.Exists("DeepClient\\LoadingVid.mp4"))
            {
                DeepConsole.Log("LoadingVid", "Video found !");
                MelonCoroutines.Start(LoadingVideo.LoadVideo());
            }
            else
            {
                DeepConsole.Log("LoadingVid", "Video was not found, Loading CustomAudio !");
                MelonCoroutines.Start(CustomLoadingAudios.Init());
                MelonCoroutines.Start(LoadingParticules.loadparticles());
            }
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetTexture("_Tex", _skyCubeTexture);
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetColor("_Tint", _primaryColor);
            SetupParticleSystem(GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>());
            SetupButton(GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Button>());
            SetupButton(GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Button>());
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_Percent").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_LOADING_Size").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = Color.green;
            GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = _primaryColor;
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = _primaryColor;
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = _primaryColor;
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Point light").GetComponent<Light>().color = _primaryColor;
            foreach (Button button in GameObject.Find("UserInterface").transform.Find("MenuContent").GetComponentsInChildren<Button>(true))
            {
                ColorBlock colors = button.colors;
                colors.normalColor = _primaryColor;
                colors.highlightedColor = _highlightColor;
                colors.pressedColor = _pressedColor;
                colors.selectedColor = _primaryColor;
                button.colors = colors;
            }
            yield break;
        }
        private static void SetupParticleSystem(ParticleSystem particleSystem)
        {
            if (particleSystem == null)
                return;

            particleSystem.startColor = Color.white;
            particleSystem.startSize = 0.3f;
            particleSystem.gravityModifier = 1.0f;
        }
        private static void SetupButton(Button button)
        {
            if (button == null)
                return;
            ColorBlock colors = button.colors;
            colors.pressedColor = _pressedColor;
            colors.highlightedColor = _highlightColor;
            colors.normalColor = _primaryColor;
            button.colors = colors;
        }
    }
}
