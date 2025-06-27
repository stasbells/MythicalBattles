using Newtonsoft.Json;
using System;
using UnityEngine;
using YG;

namespace MythicalBattles
{
    public class DataLocalProvider : IDataProvider
    {
        private IPersistentData _persistentData;

        public event Action PlayerDataReseted;

        public DataLocalProvider(IPersistentData persistentData) => _persistentData = persistentData;

        public void SavePlayerData()
        {
            string jsonPlayerData = JsonConvert.SerializeObject(_persistentData.PlayerData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            YandexGame.savesData.JsonPlayerData = jsonPlayerData;

            YandexGame.SaveProgress();
        }

        public void SaveGameProgressData()
        {
            string jsonGameProgressData = JsonConvert.SerializeObject(_persistentData.GameProgressData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            YandexGame.savesData.JsonGameProgressData = jsonGameProgressData;

            YandexGame.SaveProgress();
        }

        public void SaveSettingsData()
        {
            string jsonSettingsData = JsonConvert.SerializeObject(_persistentData.SettingsData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            YandexGame.savesData.JsonGameSettingsData = jsonSettingsData;

            YandexGame.SaveProgress();
        }

        public bool TryLoadPlayerData()
        {
            if (YandexGame.savesData.JsonPlayerData == null)
                return false;

            string json = YandexGame.savesData.JsonPlayerData ??= string.Empty;

            if (json != string.Empty && json[1] == 'n')
            {
                Debug.Log(json);
                Debug.LogWarning("Replacing chars");
                json = ConvertJsonString(json);
            }

            Debug.Log(json);

            PlayerData savedData = JsonConvert.DeserializeObject<PlayerData>(json);

            if (savedData == null)
                throw new InvalidOperationException();

            _persistentData.PlayerData = new PlayerData(

                money: savedData.Money,
                selectedWeaponID: savedData.SelectedWeaponID,
                selectedArmorID: savedData.SelectedArmorID,
                selectedHelmetID: savedData.SelectedHelmetID,
                selectedBootsID: savedData.SelectedBootsID,
                selectedNecklaceID: savedData.SelectedNecklaceID,
                selectedRingID: savedData.SelectedRingID);

            return true;
        }

        public bool TryLoadGameProgressData()
        {
            if (YandexGame.savesData.JsonGameProgressData == null)
                return false;

            string json = YandexGame.savesData.JsonGameProgressData ??= string.Empty;

            if (json != string.Empty && json[1] == 'n')
            {
                Debug.Log(json);
                Debug.LogWarning("Replacing chars");
                json = ConvertJsonString(json);
            }

            Debug.Log(json);

            GameProgressData savedData = JsonConvert.DeserializeObject<GameProgressData>(json);

            if (savedData == null)
                throw new InvalidOperationException();

            _persistentData.GameProgressData = new GameProgressData(

                levelsResults: savedData.LevelsResults);

            return true;
        }

        public bool TryLoadSettingsData()
        {
            if (YandexGame.savesData.JsonGameSettingsData == null)
                return false;

            string json = YandexGame.savesData.JsonGameSettingsData ??= string.Empty;

            if (json != string.Empty && json[1] == 'n')
            {
                Debug.Log(json);
                Debug.LogWarning("Replacing chars");
                json = ConvertJsonString(json);
            }

            Debug.Log(json);

            SettingsData savedData = JsonConvert.DeserializeObject<SettingsData>(json);

            if (savedData == null)
                throw new InvalidOperationException();

            _persistentData.SettingsData = new SettingsData(

                musicVolume: savedData.MusicVolume,
                soundsVolume: savedData.SoundsVolume);

            return true;
        }

        public void ResetPlayerData()
        {
            _persistentData.PlayerData.Reset();

            SavePlayerData();

            PlayerDataReseted?.Invoke();
        }

        public void ResetProgressData()
        {
            _persistentData.GameProgressData.Reset();

            SaveGameProgressData();
        }

        private string ConvertJsonString(string json)
        {
            char previous;
            char next;

            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == 'n')
                {
                    previous = json[i - 1];
                    next = json[i + 1];

                    if (previous == '{' || previous == '}' || previous == ',' || next == ' ' || next == '}')
                    {
                        json = json.Remove(i, 1);
                        json = json.Insert(i, "\n");
                    }
                }
            }

            return json;
        }
    }
}