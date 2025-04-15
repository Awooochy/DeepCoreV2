using System.Collections;

namespace DeepClient.Client.Misc
{
    namespace DeepClient.Client.Misc
    {
        public static class Extensions
        {
            public static UnityEngine.Coroutine Start(this IEnumerator enumerator) => Coroutine.Start(enumerator);
            public static void Stop(this UnityEngine.Coroutine coroutine) => Coroutine.Stop(coroutine);
        }
    }
}