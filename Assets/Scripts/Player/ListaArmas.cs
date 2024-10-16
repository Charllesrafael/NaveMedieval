using UnityEngine;

namespace Nephenthesys
{

    [CreateAssetMenu(fileName = "ListaArmas", menuName = "Nephenthesys/ListaArmas", order = 0)]
    public class ListaArmas : ScriptableObject
    {
        public ArmaBase[] armas;

        public Drone GetDrone(int id)
        {
            ArmaBase _armaBase = GetArma(id);
            if (_armaBase != null)
                return _armaBase.GetDrone();

            return null;
        }

        public ArmaBase GetArma(int id)
        {
            if (id >= 0 && id < armas.Length)
                return armas[id];

            return null;
        }
    }
}
