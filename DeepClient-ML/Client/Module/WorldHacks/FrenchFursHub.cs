using UnityEngine;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class FrenchFursHub
    {
        public static void SendMsg(string msg)
        {
            GameObject.Find("Menu/Container/Panels/Chat/Input").GetComponent<UnityEngine.UI.InputField>().text = msg;
            GameObject.Find("Menu/Container/Panels/Chat/Keyboard/Row_2/Key_Enter").GetComponent<UnityEngine.UI.Button>().Press();
        }
    }
}
