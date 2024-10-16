namespace Nephenthesys
{
    public class ArmasMultiplas : ArmaBase
    {
        public ArmaBase[] armas;

        public override void Atirar()
        {
            foreach (var arma in armas)
            {
                arma.Atirar();
            }
        }
    }
}
