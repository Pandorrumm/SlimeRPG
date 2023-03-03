using UnityEngine;
using DG.Tweening;

namespace SlimeRPG.UI
{
    public class NoMoney : MonoBehaviour
    {
        [SerializeField] private GameObject targetMovement = null;
        [SerializeField] private float durationMovement = 1f;

        [Space]
        [SerializeField] private GameObject noADSPanel = null;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = noADSPanel.transform.position;
        }

        public void StatusNoMoneyPanel(bool _status)
        {
            noADSPanel.SetActive(_status);

            if (_status)
            {             
                noADSPanel.transform.DOMove(targetMovement.transform.position, durationMovement).OnComplete(() => ReturnPosition());
            }             
        }

        private void ReturnPosition()
        {
            StatusNoMoneyPanel(false);
            noADSPanel.transform.DOMove(startPosition, 0);
        }
    }
}
