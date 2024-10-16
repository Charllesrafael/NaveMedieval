using System.Collections.Generic;
using UnityEngine;
namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "TotemData", menuName = "Custom/TotemData", order = 0)]
    public class TotemData : ScriptableObject
    {
        public List<NaveData> listaNaves;

        internal int GetMaxNaves()
        {
            return listaNaves.Count;
        }

        internal GameObject GetNavePrefab(int naveSelecionada)
        {
            if (naveSelecionada < listaNaves.Count)
                return listaNaves[naveSelecionada].GetNavePrefab();

            return null;
        }

        internal NaveData GetNave(int naveSelecionada)
        {
            if (naveSelecionada < listaNaves.Count)
                return listaNaves[naveSelecionada];

            return null;
        }

        internal int GetArmaIdNave(int naveSelecionada)
        {
            if (naveSelecionada < listaNaves.Count)
                return listaNaves[naveSelecionada].GetArmaId();

            return 0;
        }
        internal List<NaveData> GetAllNaves()
        {
            return listaNaves;
        }
    }
}