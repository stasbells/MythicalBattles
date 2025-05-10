using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using YG;

namespace MythicalBattles
{
    public class DataLocalProvider : IDataProvider
    {
        private const string FileNamePlayerData = "PlayerSave";
        private const string FileNameGameProgress = "GameProgress";
        private const string SaveFileExtension = ".json";

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

        public bool TryLoadPlayerData()
        {
            if (YandexGame.savesData.JsonPlayerData == null)
                return false;

            PlayerData savedData = JsonConvert.DeserializeObject<PlayerData>(YandexGame.savesData.JsonPlayerData);

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

            GameProgressData savedData = JsonConvert.DeserializeObject<GameProgressData>(YandexGame.savesData.JsonGameProgressData);

            _persistentData.GameProgressData = new GameProgressData(

                levelsResults: savedData.LevelsResults);

            return true;
        }

        public void ResetPlayerData()
        {
            _persistentData.PlayerData.Reset();

            SavePlayerData();

            PlayerDataReseted?.Invoke();
        }
    }
}