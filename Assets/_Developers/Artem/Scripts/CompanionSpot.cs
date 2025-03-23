using UnityEngine;

namespace MythicalBattles
{
    public class CompanionSpot : MonoBehaviour
    {
        private Companion _companion;

        public bool IsFilled => _companion != null;

        public void Fill(Companion companion)
        {
            _companion = companion;
        }

        public void Release()
        {
            Destroy(_companion.gameObject);
            
            _companion = null;
        }
    }
}
