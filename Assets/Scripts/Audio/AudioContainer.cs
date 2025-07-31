using System;
using System.Collections.Generic;
using System.Linq;
using Ami.BroAudio;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Audio
{
    [CreateAssetMenu(fileName = "New AudioContainer", menuName = "AudioContainer/Create New AudioContainer",
        order = 51)]
    public class AudioContainer : ScriptableObject
    {
        [SerializeField] private float _volumeDecayTime = 1.5f;

        [field: SerializeField] public SoundID MenuTheme { get; private set; }
        [field: SerializeField] public SoundID BossTheme { get; private set; }
        [field: SerializeField] public SoundID GraveyardTheme { get; private set; }
        [field: SerializeField] public SoundID CastleTheme { get; private set; }
        [field: SerializeField] public SoundID DungeonTheme { get; private set; }
        [field: SerializeField] public SoundID FinalTittlesTheme { get; private set; }
        [field: SerializeField] public SoundID BaseShot { get; private set; }
        [field: SerializeField] public SoundID ElectricShot { get; private set; }
        [field: SerializeField] public SoundID FireShot { get; private set; }
        [field: SerializeField] public SoundID PoisonShot { get; private set; }
        [field: SerializeField] public SoundID PlayerDamaged { get; private set; }
        [field: SerializeField] public SoundID PlayerDeath { get; private set; }
        [field: SerializeField] public SoundID BoostUpKeep { get; private set; }
        [field: SerializeField] public SoundID BossSpell { get; private set; }
        [field: SerializeField] public SoundID PayMoney { get; private set; }
        [field: SerializeField] public SoundID ButtonClick { get; private set; }

        private SoundID _themePlayedBeforeBossThemeID;
        
        public SoundID CurrentPlayingMusicID { get; private set; }

        public void PlayMusic(SoundID soundID, float volume)
        {
            if (GetMusicIdList().Contains(soundID) == false)
                throw new InvalidOperationException();

            if (soundID == BossTheme)
                _themePlayedBeforeBossThemeID = CurrentPlayingMusicID;

            BroAudio.Stop(CurrentPlayingMusicID);

            CurrentPlayingMusicID = soundID;

            BroAudio.Play(soundID);
            
            BroAudio.SetVolume(soundID, volume);
        }
        
        public void PlayLevelThemeAfterBossTheme(float volume)
        {
            BroAudio.Stop(CurrentPlayingMusicID);

            CurrentPlayingMusicID = _themePlayedBeforeBossThemeID;
            
            BroAudio.Play(_themePlayedBeforeBossThemeID);
            
            BroAudio.SetVolume(_themePlayedBeforeBossThemeID, volume);

        }

        public void PlaySound(SoundID soundID, float volume)
        {
            if (GetSoundsIdList().Contains(soundID) == false)
                throw new InvalidOperationException();

            BroAudio.Play(soundID);
            
            BroAudio.SetVolume(soundID, volume);
        }

        public void Stop(SoundID soundID)
        {
            BroAudio.Stop(soundID, _volumeDecayTime);
        }

        public void SetMusicVolume(float volume)
        {
            BroAudio.SetVolume(CurrentPlayingMusicID, volume);
        }

        private IReadOnlyList<SoundID> GetMusicIdList()
        {
            List<SoundID> idList = new List<SoundID>
            {
                MenuTheme,
                BossTheme,
                FinalTittlesTheme,
                GraveyardTheme,
                CastleTheme,
                DungeonTheme
            };

            return idList;
        }

        private IReadOnlyList<SoundID> GetSoundsIdList()
        {
            List<SoundID> idList = new List<SoundID>
            {
                BaseShot,
                ElectricShot,
                FireShot,
                PoisonShot,
                PlayerDamaged,
                BoostUpKeep,
                BossSpell,
                PayMoney,
                ButtonClick,
                PlayerDeath,
            };

            return idList;
        }
    }
}