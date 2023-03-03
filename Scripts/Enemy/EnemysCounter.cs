using DG.Tweening;
using SlimeRPG.EnemyCreator;
using SlimeRPG.Hero;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeRPG.Enemy
{
    public class EnemysCounter : MonoBehaviour
    {
        [SerializeField] private EnemiesCreator enemysCreator;

        [Space]
        [SerializeField] private HeroMovement heroMovement; 

        [Space]
        public List<GameObject> enemies = new List<GameObject>();

        private int enemyCounter;

        private void OnEnable()
        {
            enemysCreator.OnCreateEnemy += GetEnemy;
            EnemyDeath.OnEnemyDeathObject += DeleteEnemy;
        }

        private void OnDisable()
        {
            enemysCreator.OnCreateEnemy -= GetEnemy;
            EnemyDeath.OnEnemyDeathObject -= DeleteEnemy;
        }

        private void GetEnemy(GameObject _enemy)
        {
            enemies.Add(_enemy);
            enemyCounter++;
        }

        private void DeleteEnemy(GameObject _enemy)
        {
            enemies.Remove(_enemy);
            enemyCounter--;

            if (enemyCounter <= 0)
            {
                heroMovement.Move();

                DOTween.Sequence()
                     .AppendInterval(2f)
                     .AppendCallback(() => enemysCreator.CreateEnemyObjects());               
            }
        }
    }
}
