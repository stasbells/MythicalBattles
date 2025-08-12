using System;
using System.Collections.Generic;
using YG;

namespace MythicalBattles.Assets.Scripts.Utils
{
    public static class LanguagesDictionary
    {
        private static readonly Dictionary<string, string> S_ruDomainDictionary = new ()
            {
                { "Attack speed", "Скорость атаки" },
                { "Health", "Здоровья" },
                { "Damage", "Урон" },
                { "Simple Boots", "Простые Сапоги" },
                { "Common Boots", "Обычные Сапоги" },
                { "Rare Boots", "Редкие Сапоги" },
                { "Epic Boots", "Эпические Сапоги" },
                { "Legendary Boots", "Легендарные Сапоги" },
                { "Simple Armor", "Простая Броня" },
                { "Common Armor", "Обычная Броня" },
                { "Rare Armor", "Редкая Броня" },
                { "Epic Armor", "Эпическая Броня" },
                { "Legendary Armor", "Легендарная Броня" },
                { "Simple Helmet", "Простой Шлем" },
                { "Common Helmet", "Обычный Шлем" },
                { "Rare Helmet", "Редкий Шлем" },
                { "Epic Helmet", "Эпический Шлем" },
                { "Legendary Helmet", "Легендарный Шлем" },
                { "Simple Ring", "Простое Кольцо" },
                { "Common Ring", "Обычное Кольцо" },
                { "Rare Ring", "Редкое Кольцо" },
                { "Epic Ring", "Эпическое Кольцо" },
                { "Legendary Ring", "Легендарное Кольцо" },
                { "Simple Necklace", "Простое Ожерелье" },
                { "Common Necklace", "Обычное Ожерелье" },
                { "Rare Necklace", "Редкое Ожерелье" },
                { "Epic Necklace", "Эпическое Ожерелье" },
                { "Legendary Necklace", "Легендарное Ожерелье" },
                { "Simple Bow", "Простой Лук" },
                { "Common Bow", "Обычный Лук" },
                { "Rare Bow", "Редкий Лук" },
                { "Epic Bow", "Эпический Лук" },
                { "Legendary Bow", "Легендарный Лук" },
                { "Wave", "Волна" },
                { "Next wave in", "Следующая волна через" },
            };

        private static readonly Dictionary<string, string> S_enDomainDictionary = new ()
            {
                { "Attack speed", "Attack speed" },
                { "Health", "Health" },
                { "Damage", "Damage" },
                { "Simple Boots", "Simple Boots" },
                { "Common Boots", "Common Boots" },
                { "Rare Boots", "Rare Boots" },
                { "Epic Boots", "Epic Boots" },
                { "Legendary Boots", "Legendary Boots" },
                { "Simple Armor", "Simple Armor" },
                { "Common Armor", "Common Armor" },
                { "Rare Armor", "Rare Armor" },
                { "Epic Armor", "Epic Armor" },
                { "Legendary Armor", "Legendary Armor" },
                { "Simple Helmet", "Simple Helmet" },
                { "Common Helmet", "Common Helmet" },
                { "Rare Helmet", "Rare Helmet" },
                { "Epic Helmet", "Epic Helmet" },
                { "Legendary Helmet", "Legendary Helmet" },
                { "Simple Ring", "Simple Ring" },
                { "Common Ring", "Common Ring" },
                { "Rare Ring", "Rare Ring" },
                { "Epic Ring", "Epic Ring" },
                { "Legendary Ring", "Legendary Ring" },
                { "Simple Necklace", "Simple Necklace" },
                { "Common Necklace", "Common Necklace" },
                { "Rare Necklace", "Rare Necklace" },
                { "Epic Necklace", "Epic Necklace" },
                { "Legendary Necklace", "Legendary Necklace" },
                { "Simple Bow", "Simple Bow" },
                { "Common Bow", "Common Bow" },
                { "Rare Bow", "Rare Bow" },
                { "Epic Bow", "Epic Bow" },
                { "Legendary Bow", "Legendary Bow" },
                { "Wave", "Wave" },
                { "Next wave in", "Next wave in" },
            };

        private static readonly Dictionary<string, string> S_trDomainDictionary = new ()
            {
                { "Attack speed", "Saldırı hızı" },
                { "Health", "Sağlık" },
                { "Damage", "Hasar" },
                { "Simple Boots", "Basit Botlar" },
                { "Common Boots", "Ortak Botlar" },
                { "Rare Boots", "Nadir Botlar" },
                { "Epic Boots", "Epik Botlar" },
                { "Legendary Boots", "Efsanevi Botlar" },
                { "Simple Armor", "Basit Zırh" },
                { "Common Armor", "Ortak Zırh" },
                { "Rare Armor", "Nadir Zırh" },
                { "Epic Armor", "Epik Zırh" },
                { "Legendary Armor", "Efsanevi Zırh" },
                { "Simple Helmet", "Basit Kask" },
                { "Common Helmet", "Ortak Kask" },
                { "Rare Helmet", "Nadir Kask" },
                { "Epic Helmet", "Epik Kask" },
                { "Legendary Helmet", "Efsanevi Kask" },
                { "Simple Ring", "Basit Yüzük" },
                { "Common Ring", "Ortak Yüzük" },
                { "Rare Ring", "Nadir Yüzük" },
                { "Epic Ring", "Epik Yüzük" },
                { "Legendary Ring", "Efsanevi Yüzük" },
                { "Simple Necklace", "Basit Kolye" },
                { "Common Necklace", "Ortak Kolye" },
                { "Rare Necklace", "Nadir Kolye" },
                { "Epic Necklace", "Epik Kolye" },
                { "Legendary Necklace", "Efsanevi Kolye" },
                { "Simple Bow", "Basit Yay" },
                { "Common Bow", "Ortak Yay" },
                { "Rare Bow", "Nadir Yay" },
                { "Epic Bow", "Epik Yay" },
                { "Legendary Bow", "Efsanevi Yay" },
                { "Wave", "Dalga" },
                { "Next wave in", "Sonraki dalga" },
            };

        public static string GetTranslation(string key)
        {
            string language = YG2.envir.language;

            return language.ToLower() switch
            {
                Constants.RuDomain => S_ruDomainDictionary.GetValueOrDefault(key, key),
                Constants.EnDomain => S_enDomainDictionary.GetValueOrDefault(key, key),
                Constants.TrDomain => S_trDomainDictionary.GetValueOrDefault(key, key),
                _ => throw new ArgumentException($"Unsupported language: {language}"),
            };
        }
    }
}