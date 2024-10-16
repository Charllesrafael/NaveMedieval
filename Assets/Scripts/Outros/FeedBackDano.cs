using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class FeedBackDano : MonoBehaviour
    {
        [SerializeField] private bool Ativo = true;
        [SerializeField] private MaterialRef materialRef;
        [SerializeField] private FloatRef floatRef;
        private bool efeitoTocando;
        [SerializeField] private Renderer[] renderes;
        private List<Material[]> materials;
        private Material[] materiaisDano;

        private void Start()
        {
            materiaisDano = new Material[] { new Material(materialRef.value), new Material(materialRef.value), new Material(materialRef.value) };
            ConfigurarRenderers();
        }

        public void ConfigurarRenderers()
        {
            if (renderes.Length == 0)
                renderes = GetComponentsInChildren<Renderer>();

            materials = new List<Material[]>();
            foreach (var render in renderes)
            {
                materials.Add(render.materials);
            }
        }

        public void ConfigurarRenderers(Renderer[] _renderes)
        {
            renderes = _renderes;
            materials = new List<Material[]>();
            foreach (var render in renderes)
            {
                materials.Add(render.materials);
            }
        }

        public void Efeito()
        {
            if (efeitoTocando || !Ativo)
                return;

            efeitoTocando = true;
            FeedBackDanoManager.instance?.Efeito(renderes, materials, () => { 
                efeitoTocando = false; 
            });
        }
    }
}
