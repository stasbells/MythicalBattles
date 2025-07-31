using Ami.BroAudio;
using MythicalBattles.Assets.Scripts.Services.AudioPlayback;
using MythicalBattles.Assets.Scripts.Services.Data;
using Reflex.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.UI.View.ScreenGameComplete
{
    public class ScreenGameCompleteBinder : ScreenBinder<ScreenGameCompleteViewModel>
    {
        private const float DelayToShowButton = 5f;

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private TMP_Text _score;

        private IPersistentData _persistentData;
        private IAudioPlayback _audioPlayback;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();

            _persistentData = container.Resolve<IPersistentData>();
            _audioPlayback = container.Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();
        }

        private void OnEnable()
        {
            _mainMenuButton.gameObject.SetActive(false);

            StartCoroutine(ShowMainMenuButton(DelayToShowButton));

            int totalScore = (int)_persistentData.GameProgressData.GetAllPoints();

            _score.text = totalScore.ToString();

            SoundID soundID = _audioPlayback.AudioContainer.FinalTittlesTheme;

            _audioPlayback.PlayMusic(soundID);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick?.RemoveListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }

        private IEnumerator ShowMainMenuButton(float duration)
        {
            yield return new WaitForSeconds(duration);

            _mainMenuButton.gameObject.SetActive(true);

            _mainMenuButton.onClick?.AddListener(OnMainMenuButtonClicked);
        }
    }
}
