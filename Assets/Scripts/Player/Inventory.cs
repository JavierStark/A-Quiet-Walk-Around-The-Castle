using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemScriptable[] _items = new ItemScriptable[4];
        private int _index = 0;

        void Start()
        {

        }

        void Update()
        {

        }

        public bool AddItem(ItemScriptable item)
        {
            if (_index < _items.Length)
            {
                _items[_index] = item;

                _index++;
                while (_index < _items.Length && _items[_index] != null) _index++;

                return true;
            }
            return false;
        }

        public ItemScriptable GetItem(int index)
        {
            if (index < _items.Length && index >= 0) return _items[index];
            else return null;
        }

        public ItemScriptable DeleteItem(int index)
        {
            ItemScriptable itemToDelete = _items[index];

            _items[index] = null;
            _index = index;

            return itemToDelete;
        }
    }
}