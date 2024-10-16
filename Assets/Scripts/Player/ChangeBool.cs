using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class ChangeBool : MonoBehaviour
    {
        public bool byPass;
        private GameManager gameManager;

        public GameManager GameManager
        {
            get
            {
                if (gameManager == null)
                    gameManager = GameManager.FindObjectOfType<GameManager>();
                return gameManager;
            }
        }

        public void FimEntradaPlayer()
        {
            if (ManagerScene.HasStory())
                ManagerScene.instance.OnIniciarHistoria();
            else
                GameManager.GameOn = true;

            this.transform.DetachChildren();

            this.gameObject.SetActive(false);
        }
    }
}
