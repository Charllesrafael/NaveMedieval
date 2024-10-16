using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class WhitePrism : MonoBehaviour
    {
        private ControlerTiro controlerTiro;
        [SerializeField] private int max= 3;
        private PlayerVida playerVida;
        private void Start()
        {
            playerVida = FindObjectOfType<PlayerVida>();
            if (playerVida != null)
            {
                playerVida.bypassDrone = true;
                playerVida.OnRecebeVida = OnRecebeVida;
                controlerTiro = GetComponentInParent<ControlerTiro>();
            }
        }

        public void OnRecebeVida(int _vida)
        {
            int armaId = controlerTiro.GetIdArma();
            List<int> lista = new List<int>(){0,1,2,3};
            lista.Remove(armaId);
            armaId = lista[Random.Range(0,lista.Count)];
            controlerTiro.SetArma(armaId);
            EventRecebeVida(_vida);
        }
        

        private void EventRecebeVida(int _vida)
        {
            AudioSystem.instance?.AudioConfigSetTrigger("Upgrade", false);
            if (playerVida._vidaAtual.value + _vida < playerVida.vidaMax)
            {
                playerVida._vidaAtual.value += _vida;
            }
            else
            {
                if (playerVida._vidaAtual.value == playerVida.vidaMax)
                    playerVida.ObterDrone();
                else
                   playerVida. _vidaAtual.value = playerVida.vidaMax;
            }
            playerVida.feedbackRecebeVida?.PlayFeedbacks();
            ArmasUI.ChangeLive(playerVida._vidaAtual.value);
        }
    }
}
