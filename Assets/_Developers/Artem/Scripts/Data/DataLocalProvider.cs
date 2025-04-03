using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace MythicalBattles
{
    public class DataLocalProvider : IDataProvider
    {
        private const string FileName = "PlayerSave";
        private const string SaveFileExtension = ".json";

        private IPersistentData _persistentData;

        public event Action DataReseted;

        public DataLocalProvider(IPersistentData persistentData) => _persistentData = persistentData;

        private string SavePath => "C:/Users/Artem/Documents/MythicalBattles/Data";
        private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");
        
        public void Save()
        {
            File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistentData.PlayerData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }

        public bool TryLoad()
        {
            if (IsDataAlreadyExist() == false)
                return false;

            PlayerData savedData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(FullPath));

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

        public void ResetData()
        {
            _persistentData.PlayerData.Reset();
            
            Save();
            
            DataReseted?.Invoke();
        }

        public bool IsDataAlreadyExist() => File.Exists(FullPath);
    }
}
