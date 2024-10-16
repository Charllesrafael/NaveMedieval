using UnityEngine;
using UnityEngine.EventSystems;

namespace Nephenthesys
{
    public class ReverseSelection : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SelectionController.SetSelectedGameObject(SelectionController.Instance.LastSelectedGameObject);
        }
    }
}
