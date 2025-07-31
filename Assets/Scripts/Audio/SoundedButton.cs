using Ami.BroAudio;
using MythicalBattles.Assets.Scripts.Services.AudioPlayback;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicalBattles.Assets.Scripts.Audio
{
    [RequireComponent(typeof(Button))]
    public class SoundedButton : MonoBehaviour, IPointerClickHandler
    {
        private IAudioPlayback _audioPlayback;
        private Button _button;

        private void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();
            
            _button = GetComponent<Button>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_button.interactable == false)
                return;

            SoundID buttonClick = _audioPlayback.AudioContainer.ButtonClick;

            _audioPlayback.PlaySound(buttonClick);
        }
    }
}
