using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using SlimeRPG.Currency;
using SlimeRPG.Enemy;

namespace SlimeRPG.UI
{
    public class CurrencyAnimation : MonoBehaviour
    {
        [SerializeField] private CurrencySystem currencySystem;

        [Space]
        [SerializeField] private TMP_Text currencyText = null;

        [Space]
        [SerializeField] private GameObject currencyAnimatedPrefab = null;
        [SerializeField] private GameObject placeCreation = null;
        [SerializeField] private Transform target = null;

        [Header("Available Money")]
        [SerializeField] private int maxMoney;

        Queue<GameObject> moneyQueue = new Queue<GameObject>();

        [Header("Animation Settings")]
        [SerializeField] [Range(0.5f, 0.9f)] private float minAnimDuration;
        [SerializeField] [Range(0.9f, 2f)] private float maxAnimDuration;
        [SerializeField] private Ease easeType;
        [SerializeField] private float decreaseDuration;

        [Space]
        [SerializeField] private Improvements improvements;

        private Vector3 targetPosition;

        private int _m = 0;
        public int Money
        {
            get { return _m; }
            set
            {
                _m = value;
                currencyText.text = Money.ToString();
            }
        }

        private void OnEnable()
        {
            EnemyDeath.OnEnemyDeathPosition += StartAnimationCurrency;
            improvements.OnPurchase += DecreaseCurrency;
        }

        private void OnDisable()
        {
            EnemyDeath.OnEnemyDeathPosition -= StartAnimationCurrency;
            improvements.OnPurchase -= DecreaseCurrency;
        }

        private void Awake()
        {
            targetPosition = target.position;

            UpdateCuyrrencyText();
        }

        private void StartAnimationCurrency(Vector3 _placeCreation)
        {
            PrepareMoney();
            AnimateCurrency(Camera.main.WorldToScreenPoint(_placeCreation), 10);
        }

        private void PrepareMoney()
        {
            GameObject money;

            for (int i = 0; i < maxMoney; i++)
            {
                money = Instantiate(currencyAnimatedPrefab, placeCreation.transform);
                money.SetActive(false);
                moneyQueue.Enqueue(money);
            }
        }

        private void AnimateCurrency(Vector3 _collectedMoneyPosition, int _amount)
        {
            Money = currencySystem.GetCurrency();

            currencySystem.Add(_amount);

            DOTween.To(() => Money, x => Money = x, Money + _amount, 1.5f);

            for (int i = 0; i < maxMoney; i++)
            {
                if (moneyQueue.Count > 0)
                {
                    GameObject money = moneyQueue.Dequeue();
                    money.SetActive(true);

                    money.transform.position = _collectedMoneyPosition;

                    float duration = UnityEngine.Random.Range(minAnimDuration, maxAnimDuration);

                    money.transform.DOMove(targetPosition, duration)
                        .SetEase(easeType)
                        .OnComplete(() =>
                        {
                            money.SetActive(false);
                            moneyQueue.Enqueue(money);
                        });
                }
            }
        }

        private void DecreaseCurrency(int _amount)
        {
            Money = currencySystem.GetCurrency();

            currencySystem.Take(_amount);

            DOTween.To(() => Money, x => Money = x, Money - _amount, decreaseDuration);
        }

        private void UpdateCuyrrencyText() => 
            currencyText.text = currencySystem.GetCurrency().ToString();
    }
}
