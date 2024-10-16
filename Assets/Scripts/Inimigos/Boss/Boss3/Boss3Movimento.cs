using UnityEngine;

namespace Nephenthesys
{
    public class Boss3Movimento : BossMovimento
    {
        [SerializeField] private float delyaAnimacao = 2;
        [SerializeField] internal Animator _animator;
        [SerializeField] internal Transform target;

        private Vector3 direcao;
        private float horizontal;
        private float vertical;

        private void Awake()
        {
            direcao = Vector3.zero;
            _animator.SetFloat("Vertical", 1);
        }

        public override void Start()
        {
            indoParaDireita = true;
        }

        public override void Update()
        {
            base.Update();
            ControlAnimation();
        }

        private void ControlAnimation()
        {
            horizontal = Mathf.MoveTowards(horizontal, GetDirection(target.position.x, pontosMovimento[idPonto].position.x), delyaAnimacao * Time.deltaTime);
            vertical = Mathf.MoveTowards(vertical, GetDirection(target.position.z, pontosMovimento[idPonto].position.z), delyaAnimacao * Time.deltaTime);

            _animator.SetFloat("Horizontal", horizontal);
            _animator.SetFloat("Vertical", vertical);
        }

        private float GetDirection(float a, float b)
        {
            if (a > b)
                return 1f;

            if (a < b)
                return -1f;

            return 0f;
        }
    }
}
