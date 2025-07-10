using UnityEngine;
// YG Old/using YG;

namespace MythicalBattles
{
    public class SaveDataResseter : MonoBehaviour
    {
        public void ResetSaveData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            // YG Old/YandexGame.ResetSaveProgress();
            // YG Old/YandexGame.SaveProgress();

            Debug.Log("Save data has been reset.");
        }
    }
}
