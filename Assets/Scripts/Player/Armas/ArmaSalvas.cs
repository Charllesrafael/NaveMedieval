using UnityEngine;

namespace Nephenthesys
{
    public class ArmaSalvas : ArmaBase
    {
        [SerializeField] internal bool revertOrder = false;
        [SerializeField] internal float delayEntreTiro = 0;
        [SerializeField] internal Tiro tiro;
        [SerializeField] internal Pontos[] pontosLevel;


        internal float delayAtualEntreTiro;
        private bool direcaoCrescente = true;
        private int idTiroAtual = 0;

        private void Start() {
            
        }

        public override void Atirar()
        {
            delayAtualEntreTiro += Time.deltaTime;

            if (delayAtualEntreTiro > delayEntreTiro)
            {
                delayAtualEntreTiro = 0;
                Transform ponto = pontosLevel[LevelArma != null ? LevelArma.value - 1 : 0].pontos[idTiroAtual];
                ControllerPool.Create(tiro, ponto.position, ponto.rotation);

                if (!revertOrder)
                {
                    idTiroAtual++;
                    idTiroAtual = idTiroAtual < pontosLevel[LevelArma != null ? LevelArma.value - 1 : 0].pontos.Length ? idTiroAtual : 0;
                }
                else
                {
                    if (direcaoCrescente)
                    {
                        idTiroAtual++;
                        if (idTiroAtual == pontosLevel[LevelArma != null ? LevelArma.value - 1 : 0].pontos.Length - 1)
                            direcaoCrescente = false;
                    }
                    else
                    {
                        idTiroAtual--;
                        if (idTiroAtual == 0)
                            direcaoCrescente = true;
                    }
                }
            }
        }
    }

}
