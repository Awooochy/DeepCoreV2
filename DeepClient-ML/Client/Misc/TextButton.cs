using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;

namespace DeepCore.Client.Misc
{
    internal class TextButton
    {
        private GameObject _ButtonGameobject { get; set; }
        private UnityEngine.UI.Button _ButtonComponent { get; set; }
        private GameObject _ButtonIcon { get; set; }
        private GameObject _Text { get; set; }
        private TextMeshProUGUI _TextComponent { get; set; }
        ~TextButton()
        {
            this._ButtonGameobject = null;
            this._ButtonComponent = null;
            this._ButtonIcon = null;
            this._Text = null;
            this._TextComponent = null;
        }
        public TextButton(Transform path, string text, string id, Player user)
        {
            this._ButtonGameobject = new GameObject("BTN_" + id);
            this._ButtonGameobject.transform.parent = path;
            this._ButtonGameobject.transform.localScale = new Vector3(8.7f, 0.7f, 1f);
            this._ButtonGameobject.transform.localPosition = Vector3.zero;
            this._ButtonGameobject.transform.localEulerAngles = Vector3.zero;
            this._ButtonIcon = UnityEngine.Object.Instantiate<GameObject>(this._ButtonGameobject, this._ButtonGameobject.transform).gameObject;
            this._Text = UnityEngine.Object.Instantiate<GameObject>(this._ButtonIcon, this._ButtonIcon.transform).gameObject;
            this._ButtonIcon.transform.localPosition = new Vector3(-1.6f, 0f, 0f);
            this._ButtonIcon.transform.localScale = new Vector3(0.93f, 0.7f, 1f);
            this._ButtonIcon.gameObject.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
            this._ButtonComponent = this._ButtonGameobject.AddComponent<Button>();
            this._ButtonGameobject.AddComponent<Button>();
            this._ButtonGameobject.AddComponent<LayoutElement>();
            this._TextComponent = this._Text.AddComponent<TextMeshProUGUI>();
            this._TextComponent.richText = true;
            this._TextComponent.enableWordWrapping = false;
            this._TextComponent.text = text;
            this._TextComponent.alignment = TextAlignmentOptions.Left;
            this._TextComponent.transform.localScale = new Vector3(0.1f, 1.2f, 1f);
            this._TextComponent.fontSize = 49f;
            this._TextComponent.transform.localPosition = new Vector3(-40f, 0f, 0f);
        }
    }
}
