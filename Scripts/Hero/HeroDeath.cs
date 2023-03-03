
using UnityEngine;

namespace SlimeRPG.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth health;
        [SerializeField] private GameObject deathFx;

        private bool _isDead;

        private void Start() => 
            health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;

            Instantiate(deathFx, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);

            gameObject.SetActive(false);           
        }
    }
}