using SlimeRPG.Bullet;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SlimeRPG.Currency;

namespace SlimeRPG.UI
{
    public class Improvements : MonoBehaviour
    {
        [SerializeField] private Button addSpeedButton;
        [SerializeField] private Button addDamageButton;

        [Space]
        [SerializeField] private int priceSpeed = 10;
        [SerializeField] private int priceDamage = 10;

        [Space]
        [SerializeField] private TMP_Text priceSpeedText; 
        [SerializeField] private TMP_Text priceDamageText; 

        [Space]
        [SerializeField] private float magnitudeSpeedIncrease;
        [SerializeField] private float magnitudeDamageIncrease;

        [Space]
        [SerializeField] private BulletMovement bulletMovement;
        [SerializeField] private CurrencySystem currencySystem;
        [SerializeField] private NoMoney noMoneyPanel;

        public event Action<int> OnPurchase;

        private void Start()
        {
            addSpeedButton.onClick.AddListener(AddSpeed);
            addDamageButton.onClick.AddListener(AddDamage);

            priceSpeedText.text = priceSpeed.ToString();
            priceDamageText.text = priceDamage.ToString();
        }

        private void AddSpeed()
        {
            if (currencySystem.Check(priceSpeed))
            {
                bulletMovement.speed += magnitudeSpeedIncrease;

                OnPurchase?.Invoke(priceSpeed);
            }
            else
            {
                noMoneyPanel.StatusNoMoneyPanel(true);
            }
        }

        private void AddDamage()
        {
            if (currencySystem.Check(priceDamage))
            {
                bulletMovement.damage += magnitudeDamageIncrease;

                OnPurchase?.Invoke(priceDamage);
            }
            else
            {
                noMoneyPanel.StatusNoMoneyPanel(true);
            }
        }
    }
}
