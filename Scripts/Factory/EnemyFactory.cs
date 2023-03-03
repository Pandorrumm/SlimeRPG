using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeRPG.Factory
{
    public class EnemyFactory : AbstractFactory
    {
        private GameObject enemyPrefab;
        private Transform parent;

        public EnemyFactory(GameObject _enemy, Transform _parent)
        {
            enemyPrefab = _enemy;
            parent = _parent;
        }

        public override GameObject CreateObject()
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab, parent);
            return enemy;
        }
    }
}
