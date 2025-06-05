using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenLevelSelector
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
        public Button Button => _levelButton;

        public void SetLocked(bool isLocked)
        {
            // if (_lockedIcon != null)
            // {
            //     var color = _lockedIcon.color;
            //     color.a = isLocked ? 1f : 0f;       
            //
            //     _lockedIcon.color = color;
            // }

            _lockedIcon.gameObject.SetActive(isLocked);
            _levelLabel.gameObject.SetActive(!isLocked);
            _levelNumber.gameObject.SetActive(!isLocked);
        }
    }
}