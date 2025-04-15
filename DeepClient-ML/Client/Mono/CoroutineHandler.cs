using UnityEngine;

namespace DeepCore.Client.Mono
{
    internal class CoroutineHandler : MonoBehaviour
    {
        private static CoroutineHandler _instance;

        public static CoroutineHandler Instance
        {
            get
            {
                bool flag = _instance == null;
                if (flag)
                {
                    GameObject gameObject = new GameObject("CoroutineHandler");
                    _instance = gameObject.AddComponent<CoroutineHandler>();
                    GameObject.DontDestroyOnLoad(gameObject);
                    Debug.Log("[CoroutineHandler] Instance created.");
                }
                return _instance;
            }
        }
        private void Awake()
        {
            bool flag = _instance == null;
            if (flag)
            {
                _instance = this;
                GameObject.DontDestroyOnLoad(base.gameObject);
                Debug.Log("[CoroutineHandler] Instance set in Awake.");
            }
            else
            {
                bool flag2 = _instance != this;
                if (flag2)
                {
                    GameObject.Destroy(base.gameObject);
                    Debug.Log("[CoroutineHandler] Duplicate instance destroyed.");
                }
            }
        }
    }
}
