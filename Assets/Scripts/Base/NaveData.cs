using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "NaveData", menuName = "Nephenthesys/NaveData", order = 0)]
    public class NaveData : Achievement
    {
        public int armaId;
        public GameObject navePrefab;
        public ElementDados infoData;

        internal GameObject GetNavePrefab() => navePrefab;
        internal int GetArmaId() => armaId;

        internal string Descricao()
        {
            return infoData.Descricao(Unlocked);
        }
    }
}
