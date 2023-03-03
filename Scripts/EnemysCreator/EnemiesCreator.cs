using SlimeRPG.Bullet;
using SlimeRPG.Enemy;
using SlimeRPG.Factory;
using System;
using UnityEngine;

namespace SlimeRPG.EnemyCreator
{
    public class EnemiesCreator : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private Transform spawnPosition;

        [Space]
        [SerializeField] private int maxNumberOfEnemies;

        [Space]
        [SerializeField] private float minPositionX;
        [SerializeField] private float maxPositionX;
        [SerializeField] private float minPositionZ;
        [SerializeField] private float maxPositionZ;

        [Space]
        [SerializeField] private BulletMovement bullet;
        [SerializeField] private GameObject hero;

        private AbstractFactory abstractFactory;

        public event Action OnCreateAllEnemy;
        public event Action<GameObject> OnCreateEnemy;

        private void Start()
        {
            abstractFactory = new EnemyFactory(enemyPrefab, parent);

            CreateEnemyObjects();
        }

        public void CreateEnemyObjects() => 
            CreateEnemy(spawnPosition.position);

        private void CreateEnemy(Vector3 _spawnPosition)
        {
            for (int i = 0; i < GetRandomNumber(); i++)
            {
                var obj = abstractFactory.CreateObject();

                if (obj.TryGetComponent<EnemyMovement>(out var EnemyMovement))
                {
                    EnemyMovement.GetTarget(hero);
                }

                if (obj.TryGetComponent<EnemyAttack>(out var EnemyAttack))
                {
                    EnemyAttack.Construct(hero);
                }

                OnCreateEnemy?.Invoke(obj);

                obj.transform.position = new Vector3(_spawnPosition.x, _spawnPosition.y, GetRandomPositionZ());
            }

            OnCreateAllEnemy?.Invoke();
        }

        private float GetRandomPositionZ() =>
            UnityEngine.Random.Range(minPositionZ, maxPositionZ);

        private float GetRandomPositionX() =>
            UnityEngine.Random.Range(minPositionX, maxPositionX);

        private int GetRandomNumber() =>
            UnityEngine.Random.Range(1, maxNumberOfEnemies + 1);
    }
}
