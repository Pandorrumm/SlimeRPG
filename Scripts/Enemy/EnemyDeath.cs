using System;
using UnityEngine;
using DG.Tweening;

namespace SlimeRPG.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyMovement movement;

        [Space]
        [SerializeField] private GameObject deathFx;

        public static Action<Vector3> OnEnemyDeathPosition;
        public static Action OnEnemyDeath;
        public static Action<GameObject> OnEnemyDeathObject;

        private void OnDestroy() =>
             health.HealthChanged -= HealthChanged;

        private void Start() =>
             health.HealthChanged += HealthChanged;

        private void HealthChanged()
        {
            if (health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            health.HealthChanged -= HealthChanged;

            enemyAttack.attackSequence.Kill();
            DOTween.Kill(movement.transform);

            OnEnemyDeathPosition?.Invoke(transform.position);
            OnEnemyDeathObject?.Invoke(gameObject);
            OnEnemyDeath?.Invoke();

            Instantiate(deathFx, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);

            Destroy(gameObject);           
        }
    }
}
