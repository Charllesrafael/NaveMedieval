using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nephenthesys
{
    public class ActiveSelection : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private void Awake()
        {
            EventSystem.current.SetSelectedGameObject(target);
        }
    }
}
