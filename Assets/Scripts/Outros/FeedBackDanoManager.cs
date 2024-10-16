using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class FeedBackDanoManager : MonoBehaviour
    {
        public static FeedBackDanoManager instance;
        [SerializeField] private MaterialRef materialRef;
        [SerializeField] private FloatRef floatRef;
        private Material[] materiaisDano;
        private void Awake()
        {
            instance = this;
            materiaisDano = new Material[] { new Material(materialRef.value), new Material(materialRef.value), new Material(materialRef.value) };
        }

        public void Efeito(Renderer[] renderes, List<Material[]> materials, Action callBack)
        {
            Material[] materiaisDano = new Material[] { new Material(materialRef.value), new Material(materialRef.value), new Material(materialRef.value) };
            StartCoroutine(IEfeito(renderes, materials, materiaisDano, callBack));
        }

        public IEnumerator IEfeito(Renderer[] renderes, List<Material[]> materials, Material[] _materiaisDano, Action callBack)
        {
            if(_materiaisDano != null){
                foreach (var render in renderes)
                {
                    if (render != null)
                    {
                        render.materials = _materiaisDano;
                    }
                }

                if(floatRef != null)
                    yield return new WaitForSeconds(floatRef.value);
                else
                    yield return new WaitForSeconds(0.05f);

                for (int i = 0; i < renderes.Length; i++)
                {
                    if (renderes[i] != null && materials[i] != null)
                        renderes[i].materials = materials[i];
                }
            }
            callBack?.Invoke();
        }
    }
}
