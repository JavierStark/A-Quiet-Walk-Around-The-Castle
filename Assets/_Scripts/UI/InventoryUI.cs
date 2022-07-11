using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Interactables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject border;
        [SerializeField] private GameObject slot;

        private Transform[] _slots;
        private ItemScriptable[] _items;
        
        private int _selectedSlot;

        public void Setup(int slots, int index, ItemScriptable[] items)
        {
            _slots = new Transform[slots];
            _items = new ItemScriptable[slots];
            _selectedSlot = index;
            for (int i = 0; i < slots; i++)
            {
                var currentSlot = Instantiate(slot, this.transform);
                if (i == _selectedSlot)
                {
                    Instantiate(border, currentSlot.transform);
                }

                _slots[i] = currentSlot.transform;
            }
        }
        
        public void SetBorder(int index)
        {
            var borderObject = _slots[_selectedSlot].GetChild(1);
            borderObject.SetParent(_slots[index]);
            borderObject.transform.localPosition = Vector3.zero ;

            _selectedSlot = index;
        }

        public void AddItem(int index, ItemScriptable item)
        {
            _items[index] = item;
            ChangeSlot(_slots[index], item);
        }

        public void DeleteItem(int index)
        {
            _items[index] = null;
            ChangeSlot(_slots[index], null);
        }

        private void ChangeSlot(Transform slotToChange, ItemScriptable item)
        {
            slotToChange.GetChild(0).GetComponent<Image>().sprite = item ? item.icon : null;
        }
    }
}