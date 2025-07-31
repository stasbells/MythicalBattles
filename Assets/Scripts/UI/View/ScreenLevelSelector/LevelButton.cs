using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenLevelSelector
{
    [System.Serializable]
    public class LevelButton
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _lockedIcon;
        [SerializeField] private TMP_Text _levelLabel;
        [SerializeField] private TMP_Text _levelNumber;

        public RectTransform RectTransform => _rectTransform;

        public void SetLocked(bool isLocked)
        {
            _lockedIcon.gameObject.SetActive(isLocked);
            _levelLabel.gameObject.SetActive(!isLocked);
            _levelNumber.gameObject.SetActive(!isLocked);
        }
    }
}