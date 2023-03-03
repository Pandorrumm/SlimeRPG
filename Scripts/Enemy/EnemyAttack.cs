using DG.Tweening;
using SlimeRPG.Logic;
using UnityEngine;

namespace SlimeRPG.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float damage = 1f;

        public Sequence attackSequence;

        private GameObject hero;

        public void Construct(GameObject _hero) => 
            hero = _hero;

        private void OnEnable() => 
            enemyMovement.OnCameToTarget += StartAttack;

        private void OnDisable() => 
            enemyMovement.OnCameToTarget -= StartAttack;


        private void StartAttack() => 
            Attack();

        private void Attack()
        {
            hero.transform.GetComponent<IHealth>().TakeDamage(damage);

            attackSequence = DOTween.Sequence()
                               .AppendInterval(attackCooldown)
                               .AppendCallback(StartAttack);
        }
    }
}