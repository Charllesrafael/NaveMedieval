using UnityEngine;

namespace Nephenthesys
{
    public class Vida : MonoBehaviour
    {
        [SerializeField] internal float delayDano = 0.3f;
        internal float delayDanoCurrent;

        [SerializeField] internal int vidaInicial;
        [SerializeField] private GameObject target;
        [SerializeField] private ParticleSystem EfeitoMorte;

        [Range(0f, 1f)]
        [SerializeField] private float chanceDrop = 0;
        [SerializeField] private int IdScore;

        internal FeedBackDano feedBackDano;

        [SerializeField] internal int vidaAtual;
        internal bool morreu = false;

        public virtual void Awake()
        {
            vidaAtual = vidaInicial;
            feedBackDano = GetComponent<FeedBackDano>();
        }

        public virtual void OnCollisionEnter(Collision other)
        {
            Colidiu(other.collider);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            Colidiu(other);
        }

        private void Colidiu(Collider other)
        {
            if (other.gameObject.tag != "TiroPlayer" && other.gameObject.tag != "AreaLimite")
                return;

            Dangerous dangerous = other.GetComponent<Dangerous>();

            bool ehAreaLimite = false;
            if (other.gameObject.tag == "AreaLimite") { ehAreaLimite = true; }

            if (dangerous)
            {
                RecebeDano(dangerous.DanoRef ? dangerous.DanoRef.value : 1, ehAreaLimite);
            }
        }

        public virtual void RecebeDano(int _dano, bool ehAreaLimite = false, bool byPass = false)
        {
            if (LimitadorTela.instance.ForaCamera(this.transform.position) || morreu)
                return;


            if (Time.time > delayDanoCurrent + delayDano || byPass)
            {
                delayDanoCurrent = Time.time;
                feedBackDano?.Efeito();

                if (vidaInicial == -1)
                    return;

                if (vidaAtual - _dano > 0)
                {
                    vidaAtual -= _dano;
                }
                else
                {
                    vidaAtual = 0;
                    morreu = true;
                    Morrer(ehAreaLimite);
                }
            }
        }

        public virtual void Morrer(bool ehMortePorLimite)
        {
            if (EfeitoMorte)
                if (!ehMortePorLimite) ControllerPool.Create(EfeitoMorte, transform.position, Quaternion.identity);

            DropItem();

            ScoreUI.CriarTextUIScore(IdScore, this.transform.position);
            ControllerPool.Discard(target);
        }

        private void DropItem()
        {
            if (chanceDrop > 0)
            {
                float randoNum = Random.Range(0f, 1f);
                if (randoNum <= (chanceDrop + (GameManager.instance.chanceDrop/100)) && randoNum > 0)
                {
                    float chanceDropUpgrade = Random.Range(0, 100);
                    if (chanceDropUpgrade <= GameManager.instance.chanceDropUpgrade)
                    {
                        ControllerPool.Create(GameManager.instance.upgrade, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        ControllerPool.Create(GameManager.instance.downgrade, transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
