namespace Nephenthesys
{
    public class TiroMiraPlayer : Tiro
    {
        public override void Start()
        {
            PlayerVida player = GameManager.GetPlayer();
            if (player != null)
                direcaoMove = (player.transform.position - this.transform.position).normalized;
            else
                base.Start();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.transform.LookAt(this.transform.position + direcaoMove);
        }
    }
}