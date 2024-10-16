using UnityEngine;

namespace Nephenthesys
{
    public class CreateDroneAirGirando : MonoBehaviour
    {
        public DroneAirGirandoData[] droneAirGirandoDatas;

        [System.Serializable]
        public class DroneAirGirandoData
        {
            public bool randomDirecao;
            public Vector3 direcao;
            public DroneAirGirando droneAirGirando;
        }

        private void Start()
        {
            foreach (DroneAirGirandoData item in droneAirGirandoDatas)
            {
                if (item != null)
                {
                    Vector3 _direction = item.direcao;
                    if (item.randomDirecao)
                    {
                        float valor = Random.Range(0f, 360f);
                        _direction.x = Mathf.Cos(valor);
                        _direction.z = Mathf.Sin(valor);
                        _direction.y = 0f;
                    }
                    ControllerPool.Create(item.droneAirGirando, this.transform.position).direction = _direction;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
