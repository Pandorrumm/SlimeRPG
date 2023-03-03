using SlimeRPG.Enemy;
using System;
using UnityEngine;

namespace SlimeRPG.Hero
{
    public class AttackZone : MonoBehaviour
    {
        public event Action OnEnemyCrossedZone;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<EnemyMovement>())
            {
                OnEnemyCrossedZone?.Invoke();
            }
        }
    }
}
