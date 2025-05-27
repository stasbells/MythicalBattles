using System.Collections;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameComplete
{
    public class ScreenGameCompleteBinder : ScreenBinder<ScreenGameCompleteViewModel>
    {
        private const float DelayToShowButton = 5f;
        
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private TMP_Text _score;
        
        private IPersistentData _persistentData;

        private void Construct()
        {
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
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
