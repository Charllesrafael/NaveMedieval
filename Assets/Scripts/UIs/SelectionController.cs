using UnityEngine;
using UnityEngine.EventSystems;

namespace Nephenthesys
{
    public class SelectionController : MonoBehaviour
    {
        private static SelectionController instance;
        private GameObject lastSelectedGameObject;

        public GameObject LastSelectedGameObject { get => lastSelectedGameObject; private set => lastSelectedGameObject = value; }
        public static SelectionController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("SelectionController").AddComponent<SelectionController>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }


        public static void SetSelectedGameObject(GameObject selected)
        {
            Instance.LastSelectedGameObject = selected;
            EventSystem.current.SetSelectedGameObject(selected);
        }
    }
}
