using System.Collections.Generic;
using UnityEngine;

namespace Nephenthesys
{
    public class MorteDroneFire : MonoBehaviour
    {
        [SerializeField] private IntRef dano;
        [SerializeField] private Vector3 halfExtents;
        [SerializeField] private Vector3 direction;
        [SerializeField] private float maxDistance = 30;
        [SerializeField] private LayerMask layerMask;

        private List<Vida> inimigos;
        private List<Tiro> tiros;
        private List<ItemUpgrade> itemUpgrades;


        internal void AplicarEfeito()
        {
            foreach (var item in inimigos)
            {
                if (item != null)
                    item.RecebeDano(dano.value, false, true);
            }

            foreach (var item in tiros)
            {
                if (item != null)
                    item.Morrer(true);
            }

            foreach (var item in itemUpgrades)
            {
                if (item != null)
                    Destroy(item.gameObject);
            }
        }

        private void OnEnable()
        {
            inimigos = new List<Vida>();
            tiros = new List<Tiro>();
            itemUpgrades = new List<ItemUpgrade>();
            RaycastHit[] hits = Physics.BoxCastAll(this.transform.position, halfExtents, direction, Quaternion.identity, maxDistance, layerMask);

            foreach (RaycastHit item in hits)
            {
                Vida _vida = item.collider.gameObject.GetComponent<Vida>();
                if (_vida != null && !inimigos.Contains(_vida))
                    inimigos.Add(_vida);

                Tiro _tiros = item.collider.gameObject.GetComponent<Tiro>();
                if (_tiros != null && !tiros.Contains(_tiros))
                    tiros.Add(_tiros);

                ItemUpgrade itemUpgrade = item.collider.gameObject.GetComponent<ItemUpgrade>();
                if (itemUpgrade != null && !itemUpgrades.Contains(itemUpgrade))
                    itemUpgrades.Add(itemUpgrade);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(this.transform.position, halfExtents);
        }
    }
}
