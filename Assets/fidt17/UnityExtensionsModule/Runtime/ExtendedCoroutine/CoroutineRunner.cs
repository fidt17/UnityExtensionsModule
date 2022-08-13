using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = CreateInstance();
                }
                return _instance;
            }
        }

        private static CoroutineRunner _instance;
        
        public static void Destroy()
        {
            if (_instance == null) return;
            if (_instance.gameObject == null) return;
#if UNITY_EDITOR
            DestroyImmediate(Instance.gameObject);
#else
            Destroy(Instance.gameObject);
#endif
        }

        private static CoroutineRunner CreateInstance()
        {
            var coroutineRunner = new GameObject("Coroutine Runner");
            return coroutineRunner.AddComponent<CoroutineRunner>();
        }
    }
}