using UnityEngine;
using System;

namespace SlimeRPG.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float distanceToTarget = 0.1f;
        [SerializeField] private float speed = 1f;

        private GameObject target;
        private bool isMove;
        private Vector3 direction;

        public event Action OnCameToTarget;

        private void Start() => 
            StartMoving();

        private void Update() => 
            Move();

        private void Move()
        {
            if (isMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

                direction = target.transform.position - transform.position;

                if (direction.sqrMagnitude < distanceToTarget)
                {
                    isMove = false;

                    OnCameToTarget?.Invoke();
                }               
            }
        }

        public void StartMoving()
        {
            if (target != null)
            {
                isMove = true;
            }
        }

        public void GetTarget(GameObject _target) => 
            target = _target;
    }
}
