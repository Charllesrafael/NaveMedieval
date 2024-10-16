using System;
using UnityEngine;

namespace Nephenthesys
{
    [CreateAssetMenu(fileName = "GameDialogList", menuName = "Nephenthesys/GameDialogList", order = 0)]
    public class GameDialogList : ScriptableObject
    {
        public GameDialog[] textHistorias;

        internal string GetTextHistorias(int idFaseAtual, int idCurrentText)
        {
            return textHistorias[idFaseAtual].GetTexto(idCurrentText);
        }
    }
}
