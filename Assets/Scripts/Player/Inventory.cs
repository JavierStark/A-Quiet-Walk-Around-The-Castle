using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [Range(1,10)]public float mouseSensitivity = 25;
        
        public const int InventorySlots = 4;
        [SerializeField] private ItemScriptable[] _items = new ItemScriptable[InventorySlots];
        private int _index = 0;
        
        private Input _input;
        [SerializeField] private InventoryUI inventoryUI; 
        
        private float _scroll;
        private float _lastNumber = 0;

        private void Start()
        {
            _input = GetComponent<Input>();
            inventoryUI.Setup(InventorySlots, _index, _items);
        }

        private void Update()
        {
            ScrollCheck();
            int currentIndex = _index;
            
            IndexChangeByScroll();
            IndexChangeByShortcuts();
            
            if(_index != currentIndex) inventoryUI.SetBorder(_index);
            
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

            if (_index >= InventorySlots) _index = _index - (InventorySlots - 1) -1;
            else if (_index < 0) _index = InventorySlots + _index;
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