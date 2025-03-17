using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private int _itemsCount;
        [SerializeField] private int _projectileLayer;

        private List<Projectile> _items;
        private Transform _transform;

        public int CurrentItemIndex { get; private set; } = 0;
        public IReadOnlyList<Projectile> Items => _items;

        private void Awake()
        {
            _transform = GetComponent<Transform>();

            if (_items == null)
                Initialize();
        }

        public Projectile GetItem()
        {
            var item = _items.Find(item => item.gameObject.activeSelf == false);

            item.Transform.parent = null;

            return item;
        }

        public void ReturnItem(Projectile item)
        {
            item.Transform.position = _transform.position;
            item.Transform.parent = _transform;
            item.gameObject.SetActive(false);
        }

        private void Initialize()
        {
            _items = new List<Projectile>();

            for (int i = 0; i < _itemsCount; i++)
            {
                var item = Instantiate(_prefab, _transform);

                item.SetPool(this);
                item.gameObject.layer = _projectileLayer;
                item.gameObject.SetActive(false);

                _items.Add(item);
            }
        }
    }
}