using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace DeepClient.Client.Coroutines
{
    internal class CustomLoadingScreen
    {
        private static Texture2D _skyCubeTexture;
        private static Color _primaryColor = new Color(0f, 0f, 0.25f, 1f);
        private static Color _highlightColor = new Color(0.547f, 0f, 0.933f, 1f);
        private static Color _pressedColor = new Color(0.247f, 0f, 0.333f, 1f);
        public static IEnumerator Init()
        {
            while (GameObject.Find("LoadingBackground_TealGradient_Music") == null)
            {
                yield return null;
            }
            GameObject.Find("PlayerDisplay/BlackFade/inverted_sphere").SetActive(false);
            GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetTexture("_Tex", _skyCubeTexture);
            GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetColor("_Tint", _primaryColor);
            SetupParticleSystem(GameObject.Find("LoadingBackground_TealGradient_Music/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>());
            while (GameObject.Find("MenuContent/Popups/LoadingPopup") == null)
            {
                yield return null;
            }
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetTexture("_Tex", _skyCubeTexture);
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<MeshRenderer>().material.SetColor("_Tint", _primaryColor);
            SetupParticleSystem(GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles/FX_snow").GetComponent<ParticleSystem>());
            SetupButton(GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Button>());
            SetupButton(GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Button>());
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = _primaryColor;
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = _primaryColor;
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = _primaryColor;
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Point light").GetComponent<Light>().color = _primaryColor;
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