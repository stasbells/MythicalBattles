using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private int _itemsCount;

        private List<Projectile> _items;

        public int CurrentItemIndex { get; private set; } = 0;
        public IReadOnlyList<Projectile> Items => _items;

        private void Awake()
        {
            if (_items == null)
                Initialize();
        }

        public Projectile GetItem()
        {
            var item = _items.Find(item => item.gameObject.activeSelf == false);

            return item;
        }

        public void ReturnItem(Projectile item)
        {
            item.gameObject.transform.position = transform.position;
            item.gameObject.SetActive(false);
        }

        private void Initialize()
        {
            _items = new List<Projectile>();

            for (int i = 0; i < _itemsCount; i++)
            {
                var item = Instantiate(_prefab, transform);

                item.SetPool(this);
                item.gameObject.SetActive(false);

                _items.Add(item);
            }
        }
    }
}