using Interactables;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Input = InputSystem.Input;
using Random = UnityEngine.Random;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [Range(1,50)][SerializeField] private float scrollSensitivity = 1;
        [Range(0,20)][SerializeField] private float dropDistance = 2;

        private const int InventorySlots = 4;
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
            ItemSelection();

            DropItem();
        }

        private void DropItem()
        {
            if (_input.drop)
            {
                var item = DeleteItem();

                if (item)
                {
                    inventoryUI.DeleteItem(_index);
                    var t = transform;
                    var cameraTransform = transform.GetChild(0).GetChild(0).transform;

                    Vector3 dropPosition;
                    Quaternion dropRotation = item.objectGameObject.transform.rotation;

                    Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
                    RaycastHit raycastHit;


                    if (Physics.Raycast(ray, out raycastHit, dropDistance))
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        dropPosition = raycastHit.point + raycastHit.normal * 0.1f;
                    }
                    else
                    {
                        var forwardDistance = cameraTransform.forward * dropDistance;
                        dropPosition = cameraTransform.position + forwardDistance;
                    }

                    Instantiate(item.objectGameObject, dropPosition, dropRotation);
                }

            }

            _input.drop = false;
        }

        private void ItemSelection()
        {
            ScrollCheck();
            int currentIndex = _index;
            
            IndexChangeByScroll();
            IndexChangeByShortcuts();

            if(_index != currentIndex) inventoryUI.SetBorder(_index);
        }

        private void ScrollCheck()
        {
            if (_input.inventoryScroll > 0) _scroll -= 1;
            else if (_input.inventoryScroll < 0) _scroll += 1;
        }

        private void IndexChangeByScroll()
        {
            float deltaTimeMultiplier = 100;
            float mouseSensitivityPerSecond = scrollSensitivity * Time.deltaTime * deltaTimeMultiplier;
            _index += (int)(_scroll/mouseSensitivityPerSecond);
            _scroll %= mouseSensitivityPerSecond;

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
            if (!_items[_index])
            {
                _items[_index] = item;
                inventoryUI.AddItem(_index, item);
                return true;
            }
            return false;
        }

        public ItemScriptable GetItem()
        {
            return _items[_index];
        }

        private ItemScriptable DeleteItem()
        {
            ItemScriptable itemToDelete = _items[_index];

            _items[_index] = null;

            return itemToDelete;
        }
    }
}