using UnityEngine;
using YG;

namespace MythicalBattles
{
    public class SaveDataResseter : MonoBehaviour
    {
        public void ResetSaveData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();

            Debug.Log("Save data has been reset.");
        }
    }
}
