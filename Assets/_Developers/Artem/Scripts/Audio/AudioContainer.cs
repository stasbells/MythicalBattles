using System;
using System.Collections.Generic;
using System.Linq;
using Ami.BroAudio;
using UnityEngine;

namespace MythicalBattles
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
        
        private SoundID _currentPlayingMusicID;
        
        public void Play(SoundID soundID, float volume)
        {
            if (GetSoundsIdList().Contains(soundID))
            {
                BroAudio.SetVolume(soundID, volume);    
                
                BroAudio.Play(soundID);

                return;
            }
            
            if (GetMusicIdList().Contains(soundID))
            {
                BroAudio.Stop(_currentPlayingMusicID);
                
                _currentPlayingMusicID = soundID;

                BroAudio.SetVolume(soundID, volume);   
                
                BroAudio.Play(soundID);
                
                return;
            }
            
            throw new InvalidCastException("Sound ID not found");
        }

        public void Stop(SoundID soundID)
        {
            BroAudio.Stop(soundID, _volumeDecayTime);
        }

        public void SetVolume(float volume)
        {
            BroAudio.SetVolume(_currentPlayingMusicID, volume);
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
