using System.Collections;
using UnityEngine;

namespace Nephenthesys
{
    public class Tiro : Dangerous
    {
        [SerializeField] internal TiroModel tiroModel;
        internal Vector3 direcaoMove;
        [SerializeField] internal bool destruirAoColidir = true;
        [SerializeField] private bool bypassCamera = false;

        private void Awake()
        {
            ///
            StartCoroutine(IMorrer());
            if (tiroModel.EfeitoInicial)
                ControllerPool.Create(tiroModel.EfeitoInicial, transform.position, Quaternion.identity);
        }

        public virtual void Start()
        {
            direcaoMove = transform.forward;
        }

        public virtual void FixedUpdate()
        {
            if (LimitadorTela.instance.ForaCamera(this.transform.position) && !bypassCamera)
            {
                ControllerPool.Discard(this.gameObject);
            }

            transform.position += direcaoMove * tiroModel.Velocidade * Time.deltaTime;
        }

        private IEnumerator IMorrer()
        {
            yield return new WaitForSeconds(tiroModel.Tempovida);
            Morrer(true);
        }

        public void Morrer(bool _destruirAoColidir)
        {
            if (tiroModel.EfeitoMorte)
                ControllerPool.Create(tiroModel.EfeitoMorte, transform.position, Quaternion.identity);

            if (_destruirAoColidir)
                ControllerPool.Discard(this.gameObject);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            Morrer(destruirAoColidir);
        }
    }
}