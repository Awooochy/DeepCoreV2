namespace AstroClient.MenuApi.QM.Extras
{
    using System;
    using UnityEngine;

    public class QMCellInfo
    {
        public QMCellInfo(Transform location)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search had FAILED!");

            //gameObject = Object.Instantiate(APIBase.QMCellInfoTemplate, APIBase.GetQuickMenuInstance.transform.Find("ShortcutMenu/WorldsButton"));
        }
    }
}