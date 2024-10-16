using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Nephenthesys
{
    public class ControllerPool : MonoBehaviour
    {
        private static ControllerPool instance;
        private List<Tiro> listArmazenamento;

        private void Awake()
        {
            instance = this;
            listArmazenamento = new List<Tiro>();
        }

        public static T Create<T>(T target, Vector3 _position) where T : Object
        {
            return Create(target, _position, Quaternion.identity);
        }

        public static T Create<T>(T target, Vector3 _position, Quaternion _rotation) where T : Object
        {
            return Instantiate(target, _position, _rotation);
        }

        public static T Create<T>(T target, Transform _transform) where T : Object
        {
            return Instantiate(target, _transform);
        }

        public static void Discard<T>(T target, float delay = 0) where T : Object
        {
            Destroy(target, delay);
        }

        public static Tiro CreateTiro(Tiro target, Vector3 _position, Quaternion _rotation)
        {
            if (instance == null)
                instance = new GameObject("ControllerPool").AddComponent<ControllerPool>();

            instance.listArmazenamento.Add(Instantiate(target, _position, _rotation));
            return instance.listArmazenamento[instance.listArmazenamento.Count - 1];
        }

        public static void DiscardTiro(Tiro target, float delay = 0)
        {
            instance.listArmazenamento.Remove(target);
            Destroy(target.gameObject, delay);
        }

        public static void DestruirTiros()
        {
            if (instance.listArmazenamento != null && instance.listArmazenamento.Count > 0)
            {
                for (int i = instance.listArmazenamento.Count - 1; i >= 0; i--)
                {
                    if (instance.listArmazenamento[i] != null)
                        Destroy(instance.listArmazenamento[i].gameObject);
                }
                instance.listArmazenamento.Clear();
            }
        }
    }
}