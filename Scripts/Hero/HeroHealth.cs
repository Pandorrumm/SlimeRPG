using SlimeRPG.Logic;
using System;
using UnityEngine;

namespace SlimeRPG.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float currentHP;
        [SerializeField] private float maxHP;

        public event Action HealthChanged;  

        public float Current
        {
            get => currentHP;
            set
            {
                if (currentHP != value)
                {
                    currentHP = value;

                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => maxHP;
            set => maxHP = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
            {
                return;
            }
            
            Current -= damage;

            HealthChanged?.Invoke();
        }
    }
}