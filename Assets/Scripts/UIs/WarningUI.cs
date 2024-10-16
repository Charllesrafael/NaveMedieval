using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class WarningUI : MonoBehaviour
    {
        public void FimAnimation()
        {
            ManagerWarningUI.instance.OnFimWarning();
            this.gameObject.SetActive(false);
        }
    }

}
