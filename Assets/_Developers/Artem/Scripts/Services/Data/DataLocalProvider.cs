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
            string jsonPlayerData = JsonConvert.SerializeObject(_persistentData.PlayerData);

            YG2.saves.JsonPlayerData = jsonPlayerData;

            YG2.SaveProgress();
        }

        public void SaveGameProgressData()
        {
            string jsonGameProgressData = JsonConvert.SerializeObject(_persistentData.GameProgressData);

            YG2.saves.JsonGameProgressData = jsonGameProgressData;

            YG2.SaveProgress();
        }

        public void SaveSettingsData()
        {
            string jsonSettingsData = JsonConvert.SerializeObject(_persistentData.SettingsData);

            YG2.saves.JsonGameSettingsData = jsonSettingsData;

            YG2.SaveProgress();
        }

        public void LoadPlayerData()
        {
            if (string.IsNullOrEmpty(YG2.saves.JsonPlayerData))
            {
                _persistentData.PlayerData = new PlayerData();
                return;
            }

            _persistentData.PlayerData = JsonConvert.DeserializeObject<PlayerData>(YG2.saves.JsonPlayerData);
        }

        public void LoadGameProgressData()
        {
            if (string.IsNullOrEmpty(YG2.saves.JsonGameProgressData))
            {
                _persistentData.GameProgressData = new GameProgressData();
                return;
            }

            // string json = YandexGame.savesData.JsonGameProgressData ??= string.Empty;
            //
            // if (json != string.Empty && json[1] == 'n')
            // {
            //     Debug.Log(json);
            //     Debug.LogWarning("Replacing chars");
            //     json = ConvertJsonString(json);
            // }

            _persistentData.GameProgressData = JsonConvert.DeserializeObject<GameProgressData>(YG2.saves.JsonGameProgressData);
        }

        public void LoadSettingsData()
        {
            if (string.IsNullOrEmpty(YG2.saves.JsonGameSettingsData))
            {
                _persistentData.SettingsData = new SettingsData();
                return;
            }

            // string json = YandexGame.savesData.JsonGameSettingsData ??= string.Empty;
            //
            // if (json != string.Empty && json[1] == 'n')
            // {
            //     Debug.Log(json);
            //     Debug.LogWarning("Replacing chars");
            //     json = ConvertJsonString(json);
            // }

            _persistentData.SettingsData = JsonConvert.DeserializeObject<SettingsData>(YG2.saves.JsonGameSettingsData);
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