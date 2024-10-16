using UnityEngine;

namespace Nephenthesys
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T instance;
        public string id;
        public bool _dontDestroyOnLoad = true;
        public GameObject painel;

        private void OnEnable()
        {

            if (instance != null && instance.id == id)
                Destroy(this.gameObject);

            if (instance == null)
            {
                instance = GetComponent<T>();

                if (_dontDestroyOnLoad)
                    DontDestroyOnLoad(this.gameObject);

                if (painel != null)
                    painel.SetActive(true);
            }
        }
    }
}
