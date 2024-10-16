using System;
using UnityEngine;

namespace Nephenthesys
{
    public class ArmaLookAtDirectionPlayer : MonoBehaviour
    {
        private Transform targetDirtiros;
        private Player player;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
            CreateTargetDir();
        }

        private void CreateTargetDir()
        {
            targetDirtiros = new GameObject("TargetDir").transform;
            targetDirtiros.position = this.transform.position;
            for (int i = transform.childCount - 1; i >= 0; i--)
                transform.GetChild(i).parent = targetDirtiros;

            targetDirtiros.parent = this.transform;
        }

        private void Update()
        {
            if (player.direcao.x != 0 || player.direcao.z != 0)
            {
                targetDirtiros.LookAt(targetDirtiros.position + player.direcao);
            }
        }
    }
}
