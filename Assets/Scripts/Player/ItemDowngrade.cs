namespace Nephenthesys
{
    public class ItemDowngrade : ItemUpgrade
    {
        public override void AcaoItem(PlayerVida playerVida)
        {
            playerVida.RecebeDano(1);
        }
    }
}
