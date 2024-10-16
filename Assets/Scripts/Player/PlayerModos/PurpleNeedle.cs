using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class PurpleNeedle : MonoBehaviour
    {
        [Range(0, 100)]
        public int chanceDrop;
        [Range(0, 100)]
        public int chanceDropUpgrade;
        PlayerVida playerVida;


        private IEnumerator Start()
        {
            playerVida = FindObjectOfType<PlayerVida>(true);
            if (playerVida != null)
            {
                // playerVida.OnRecebeVida = OnRecebeVida;
                while(GameManager.instance == null)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                GameManager.instance.chanceDrop = chanceDrop;
                GameManager.instance.chanceDropUpgrade = chanceDropUpgrade;
                playerVida._vidaAtual.value = playerVida.vidaMax;
                ArmasUI.ChangeLive(playerVida._vidaAtual.value);
                playerVida.ObterDrone();
            }
        }

        // public void OnRecebeVida(int _vida)
        // {
        //     playerVida?.RecebeDano(_vida, false);
        // }
    }
}
