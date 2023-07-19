using _Scripts.Interfaces;
using Interactables;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Crafting
{
    public class CraftingItemHolder : MonoBehaviour, IInteractable
    {
        [SerializeField] private Image image;
        private ItemScriptable currentItem;
        [FormerlySerializedAs("itemAdded")] public UnityEvent itemStateChanged;
    
    
        public void Interact(GameObject playerWhoInteract, ItemScriptable itemInHand)
        {
            var playerInventory = playerWhoInteract.GetComponent<Inventory>();
            if (!itemInHand)
            {
                if (!currentItem) return;
                if (!playerInventory.AddItem(currentItem)) return;
                Consume();
                if(itemStateChanged != null)
                    itemStateChanged.Invoke();
                return;
            }

            if (currentItem) return;
            //Add item
            currentItem = itemInHand;
            if(itemStateChanged != null)
                itemStateChanged.Invoke();
            image.sprite = itemInHand.icon;
            image.color = Color.white;
            playerInventory.DeleteItem();
        }

        public void Consume()
        {
            currentItem = null;
            image.sprite = null;
            image.color = Color.clear;
        }

        public ItemScriptable GetScriptable()
        {
            return currentItem;
        }
    }
}
