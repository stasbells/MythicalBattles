using UnityEngine;

namespace MythicalBattles
{
    public class CompanionSpot : MonoBehaviour
    {
        private CompanionMover _companion;

        public bool IsFilled => _companion != null;

        public void Fill(CompanionMover companion)
        {
            _companion = companion;
        }

        public void Release()
        {
            if (_companion == null)
                return;

            Destroy(_companion?.gameObject);
            
            _companion = null;
        }
    }
}
