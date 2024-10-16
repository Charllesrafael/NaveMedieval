using UnityEngine;

namespace Nephenthesys
{
    public class VisualNaveControler : MonoBehaviour
    {
        [SerializeField] private Transform paiNave;
        [SerializeField] private FeedBackDano feedBackDano;
        [SerializeField] private Player player;
        [SerializeField] private GameObject refNave;
        internal int _personagemSelecionado;

        void Start()
        {
            _personagemSelecionado = ManagerScene.instance != null ? ManagerScene.instance.naveSelecionada : 0;
            CriaNave(_personagemSelecionado);
        }

        public void CriaNave(int personagemSelecionado)
        {
            if (ManagerScene.instance == null)
                return;

            if (refNave != null && ManagerScene.instance != null)
            {
                Destroy(refNave.gameObject);
                refNave = ControllerPool.Create(ManagerScene.instance.GetNavePrefab(personagemSelecionado), paiNave);
            }

            feedBackDano.ConfigurarRenderers(refNave.GetComponent<ListMeshNave>().MeshsNave);
            player.target = refNave;
        }
    }
}
