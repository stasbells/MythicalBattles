using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MythicalBattles
{
    public class BoostsDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageBoostText;
        [SerializeField] private TMP_Text _attackSpeedBoostText;
        [SerializeField] private TMP_Text _maxHealthBoostText;
        [SerializeField] private TMP_Text _fireArrowsBoostText;
        [SerializeField] private TMP_Text _electricArrowsBoostText;
        [SerializeField] private TMP_Text _poisonArrowsBoostText;
        [SerializeField] private TMP_Text _fireCompanionBoostText;
        [SerializeField] private TMP_Text _electricCompanionBoostText;
        [SerializeField] private TMP_Text _poisonCompanionBoostText;

        private List<TMP_Text> _boostsTexts;

        private void Awake()
        {
           _boostsTexts = new List<TMP_Text>
           {
               _damageBoostText,
               _attackSpeedBoostText,
               _maxHealthBoostText,
               _fireArrowsBoostText,
               _electricArrowsBoostText,
               _poisonArrowsBoostText,
               _fireCompanionBoostText,
               _electricCompanionBoostText,
               _poisonCompanionBoostText
           };
        }

        public void Display(Boost boost)
        {
            foreach (TMP_Text boostText in _boostsTexts)
                boostText.gameObject.SetActive(false);

            switch (boost)
            {
                case DamageBoost _:
                    EnableText(_damageBoostText);
                    break;
                case AttackSpeedBoost _:
                    EnableText(_attackSpeedBoostText);
                    break;
                case MaxHealthBoost _:
                    EnableText(_maxHealthBoostText);
                    break;
                case FireCompanionBoost _:
                    EnableText(_fireCompanionBoostText);
                    break;
                case ElectricCompanionBoost _:
                    EnableText(_electricCompanionBoostText);
                    break;
                case PoisonCompanionBoost _:
                    EnableText(_poisonCompanionBoostText);
                    break;
                case FireProjectileBoost _:
                    EnableText(_fireArrowsBoostText);
                    break;
                case ElectricProjectileBoost _:
                    EnableText(_electricArrowsBoostText);
                    break;
                case PoisonProjectileBoost _:
                    EnableText(_poisonArrowsBoostText);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void EnableText(TMP_Text text)
        {
            text.gameObject.SetActive(true);
        }
    }
}
