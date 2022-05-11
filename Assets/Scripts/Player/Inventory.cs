using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [Range(1,10)]public float mouseSensitivity = 25;
        
        private const int InventorySpaces = 4;
        [SerializeField] private ItemScriptable[] _items = new ItemScriptable[InventorySpaces];
        private int _index = 0;
        
        private Input _input;
        
        private float _scroll;
        private float _lastNumber = 0;

        private void Start()
        {
            _input = GetComponent<Input>();
        }

        private void Update()
        {
            ScrollCheck();
            IndexChangeByScroll();
            IndexChangeByShortcuts();
            Debug.Log(_index);
        }

        private void ScrollCheck()
        {
            if (_input.inventoryScroll > 0) _scroll += 1;
            else if (_input.inventoryScroll < 0) _scroll -= 1;
        }

        private void IndexChangeByScroll()
        {
            _index += (int)(_scroll/mouseSensitivity);
            _scroll %= mouseSensitivity;

            if (_index >= InventorySpaces) _index = _index - (InventorySpaces - 1) -1;
            else if (_index < 0) _index = InventorySpaces + _index;
        }

        private void IndexChangeByShortcuts()
        {
            float numberPressed = _input.inventoryShortcuts;
            if (numberPressed != 0 && numberPressed != _lastNumber)
            {
                _index = (int)numberPressed - 1;
            }
        }

        public bool AddItem(ItemScriptable item)
        {
            if (_items[_index] == null)
            {
                _items[_index] = item;

                return true;
            }
            return false;
        }

        public ItemScriptable GetItem()
        {
            return _items[_index];
        }

        public ItemScriptable DeleteItem()
        {
            ItemScriptable itemToDelete = _items[_index];

            _items[_index] = null;

            return itemToDelete;
        }
    }
}