namespace AstroClient.MenuApi.QM.Controls
{
    using System;
    using UnityEngine;
    using VRC.UI.Elements;

    public class ClientPage : Root
    {
        public string MenuName { get; internal set; }
        public Action OnMenuOpen { get; set; }
        public Action OnMenuClose { get; set; }

        public UIPage Page { get; internal set; }
        public Transform MenuContents { get; internal set; }
    }


}

