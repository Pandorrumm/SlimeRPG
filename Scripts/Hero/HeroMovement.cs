using UnityEngine;
using DG.Tweening;
using SlimeRPG.Bullet;

namespace SlimeRPG.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float forwardDuration = 2f;
        [SerializeField] private float backDuration = 2f;

        [SerializeField] private Ease easeType;

        [Space]
        [SerializeField] private BulletMovement bullet;

        [Space]
        [SerializeField] private AttackZone attackZone;

        private Tweener tweener;
        private Vector3 startPosition;

        private void OnEnable() => 
            attackZone.OnEnemyCrossedZone += Stop;

        private void OnDisable() => 
            attackZone.OnEnemyCrossedZone -= Stop;

        private void Start()
        {
            startPosition = transform.position;

            Move();
        }

        public void Move()
        {
            tweener = transform.DOMove(target.transform.position, forwardDuration)
                        .SetEase(easeType);
        }

        private void Stop()
        {
            tweener = transform.DOMove(startPosition, backDuration)
                        .SetEase(easeType)
                        .OnComplete(() => bullet.StartMoving());
        }
    }
}
