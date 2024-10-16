using System;
using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class BlackSpear : MonoBehaviour
    {
        PlayerVida playerVida;

        private IEnumerator Start()
        {
            playerVida = FindObjectOfType<PlayerVida>(true);
            if (playerVida != null)
            {
                playerVida.OnRecebeVida = OnRecebeVida;
                playerVida.OnRecebeDano = OnRecebeDano;
                while(GameManager.instance == null)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                playerVida._vidaAtual.value = playerVida.vidaMax;
                ArmasUI.ChangeLive(playerVida._vidaAtual.value);
                playerVida.ObterDrone();
            }
            yield return new WaitForSeconds(0.1f);
            
            ArmasUI.instance.gameObject.AddComponent<BlackSpearUI>();
        }

        private void OnRecebeDano(int arg1, bool arg2)
        {
            if(playerVida.drone != null)
                playerVida.drone.Morrer();
            playerVida?.Morrer(false);
        }

        public void OnRecebeVida(int _vida)
        {
            AudioSystem.instance?.AudioConfigSetTrigger("Upgrade", false);
            playerVida.feedbackRecebeVida?.PlayFeedbacks();
            playerVida.ObterDrone();
        }
    }
}
