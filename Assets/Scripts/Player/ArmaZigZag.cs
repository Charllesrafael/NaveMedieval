using UnityEngine;

namespace Nephenthesys
{
    public class ArmaZigZag : ArmaBase
    {
        [SerializeField] private TiroZigZag tiro;
        [SerializeField] private float delayTiro;
        [SerializeField] private bool lookat;
        [SerializeField] private Pontos[] pontosRef;
        private float delayAtualTiro;

        [System.Serializable]
        struct Pontos
        {
            public PontosTransform[] pontos;
        }

        [System.Serializable]
        struct PontosTransform
        {
            public float RangeZigZag;
            public float SpeedZigZag;
            public bool lookat;
            public Transform ponto;
        }

        public void Awake()
        {
            delayAtualTiro = Time.time;
        }

        public override void Atirar()
        {
            if (Time.time > delayAtualTiro + delayTiro)
            {
                delayAtualTiro = Time.time;

                foreach (var item in pontosRef[LevelArma != null ? LevelArma.value - 1 : 0].pontos)
                {
                    TiroZigZag _tiro = ControllerPool.Create(tiro, item.ponto.position, item.ponto.rotation);
                    _tiro.Config(item.RangeZigZag, item.SpeedZigZag, item.lookat);
                }
            }
        }
    }
}
