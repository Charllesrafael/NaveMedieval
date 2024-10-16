
namespace Nephenthesys
{
    public class BossVida : Vida
    {
        public BossExplosao bossExplosao;
        public BossBase boss;
        internal bool ignorethisboold;

        public virtual void Start()
        {
            BarraVidaBoss.instance?.Aparecer(vidaInicial);
            bossExplosao.FimExplosoes += () =>
            {
                GameManager.GanhouOJogo();
                base.Morrer(ignorethisboold);
            };
        }

        public override void RecebeDano(int _dano, bool ehAreaLimite = false, bool byPass = false)
        {
            base.RecebeDano(_dano, ehAreaLimite, byPass);
            BarraVidaBoss.instance?.SetProgress(vidaAtual);
        }

        public override void Morrer(bool ignorethisboold)
        {
            if (!boss.morto)
            {
                BarraVidaBoss.instance?.Desaparecer();
                boss.Morreu();
                this.ignorethisboold = ignorethisboold;
                bossExplosao.Explodir();
            }
        }
    }
}
