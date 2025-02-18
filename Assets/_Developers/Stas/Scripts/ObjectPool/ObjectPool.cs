using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _itemsCount;

        private List<GameObject> _items;

        public int CurrentItemIndex { get; private set; } = 0;
        public IReadOnlyList<GameObject> Items => _items;

        private void Awake()
        {
            if (_items == null)
                Initialize();
        }

        public GameObject GetItem()
        {
            Debug.Log(_items.Find(item => item.activeSelf == false));

            return _items.Find(item => item.activeSelf == false);
        }

        public void ReturnItem(GameObject Item)
        {
            Item.SetActive(false);
            _items.Add(Item);
        }

        private void Initialize()
        {
            _items = new List<GameObject>();

            for (int i = 0; i < _itemsCount; i++)
            {
                var item = Instantiate(_prefab);

                item.SetActive(false);
                item.transform.SetParent(transform, false);

                _items.Add(item);
            }
        }
    }
}