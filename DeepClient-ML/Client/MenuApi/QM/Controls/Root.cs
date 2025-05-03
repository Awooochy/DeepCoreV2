namespace AstroClient.MenuApi.QM.Controls
{
    using TMPro;
    using UnityEngine;
    using VRC.Localization;
    using AstroClient.MenuApi.Tools;
    using UnityEngine.UI;

    public class Root
    {
        public string _text;
        public string Text
        {
            get => _text; set
            {
                _text = value;
                if (TMProCompnt != null)
                    TMProCompnt.text = _text;
            }
        }

        public GameObject gameObject { get; internal set; }
        public Transform transform { get; internal set; }
        public TextMeshProUGUI TMProCompnt { get; internal set; }

        public virtual void SetActive(bool Active) => gameObject.SetActive(Active);
        public void SetTextColor(Color color) => TMProCompnt.color = color;
        public void SetTextColor(string Hex) => TMProCompnt.text = $"<color={Hex}>{Text}</color>";

        public void SetTextColorHTML(string hex) => SetTextColor(hex);

        public void SetInteractable(bool Interactable)
        {
            if (gameObject.GetComponent<Button>() != null)
                gameObject.GetComponent<Button>().interactable = Interactable;
            else if (gameObject.GetComponent<Toggle>() != null)
                gameObject.GetComponent<Toggle>().interactable = Interactable;
        }
        public void SetText(string text) => Text = text;

        public void SetRotation(Vector3 Poz) => gameObject.transform.localRotation = Quaternion.Euler(Poz);
        public void SetPostion(Vector3 Poz) => gameObject.transform.localPosition = Poz;
        public GameObject GetGameObject() => gameObject;
        public Transform GetTransform() => gameObject.transform;
        public Transform ChangeParent(GameObject newParent) => gameObject.transform.parent = newParent.transform;

        public virtual void Destroy() => UnityEngine.Object.DestroyImmediate(gameObject);

        public virtual string SetToolTip(string tip)
        {
            bool Fi = false;
            foreach (var s in gameObject.GetComponentsInChildren<VRC.UI.Elements.Controls.ToolTip>())
            {
                if (!Fi)
                {
                    Fi = true;
                    s._localizableString = tip.Localize();
                    s.enabled = !string.IsNullOrEmpty(tip);
                }
                else s.enabled = false;
            }
            return tip;
        }
    }

}

