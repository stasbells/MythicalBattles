using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenTutorial
{
    public class ScreenTutorialBinder : ScreenBinder<ScreenTutorialViewModel>
    {
        [SerializeField] private TMP_Text _messageMobile;
        [SerializeField] private TMP_Text _messageDesktop;
        [SerializeField] private Button _gameButton;

        private void OnEnable()
        {
            if (YG2.envir.isMobile)
            {
                _messageMobile.gameObject.SetActive(true);
                _messageDesktop.gameObject.SetActive(false);
            }
            else if (YG2.envir.isDesktop)
            {
                _messageMobile.gameObject.SetActive(false);
                _messageDesktop.gameObject.SetActive(true);
            }
            else
            {
                throw new InvalidOperationException();
            }

            _gameButton.onClick?.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            _gameButton.onClick?.RemoveListener(OnPlayButtonClicked);
            YG2.saves.IsFirstSession = false;
            YG2.SaveProgress();
        }

        private void OnPlayButtonClicked()
        {
            ViewModel.RequestGoToSceneGameplay();
        }
    }
}
