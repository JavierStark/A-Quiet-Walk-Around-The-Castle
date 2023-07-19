using System.Collections.Generic;
using System.Linq;
using _Scripts.Interfaces;
using Interactables;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Crafting
{
    public class CraftingTable : MonoBehaviour, IInteractable
    {
        private List<CraftingItemHolder> itemHolders;

        [SerializeField] private Image productImage;
        private ItemScriptable currentProduct; 
            
        [SerializeField] private List<CraftingRecipeScriptable> recipes;

        private void Awake()
        {
            itemHolders = GetComponentsInChildren<CraftingItemHolder>().ToList();
            foreach (CraftingItemHolder itemHolder in itemHolders)
            {
                itemHolder.itemStateChanged.AddListener(ItemAdded);
            }
            
            productImage.color = Color.clear;
        }

        private void ItemAdded()
        {
            List<ItemScriptable> currentItemsScriptables = new List<ItemScriptable>();
            foreach (var itemHolder in itemHolders)
            {
                var itemScriptable = itemHolder.GetScriptable();
                if(!itemScriptable) continue;
                currentItemsScriptables.Add(itemScriptable);
            }

            CheckRecipes(currentItemsScriptables);
        }

        private void CheckRecipes(List<ItemScriptable> itemScriptables)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.components.OrderBy(t => t).SequenceEqual(itemScriptables.OrderBy(t => t)))
                {
                    Debug.Log("Hei");
                    SetProduct(recipe.product);
                    return;
                }
            }
            SetProduct(null);
        }

        private void SetProduct(ItemScriptable product)
        {
            currentProduct = product;

            productImage.color = product? Color.white:Color.clear;
            productImage.sprite = currentProduct?.icon;
        }

        public void Interact(GameObject playerWhoInteract, ItemScriptable itemInHand)
        {
            if (!currentProduct) return;
            if (!playerWhoInteract.GetComponent<Inventory>().AddItem(currentProduct)) return;

            ConsumeComponents();
            productImage.sprite = null;
            productImage.color = Color.clear;
            currentProduct = null;
        }

        private void ConsumeComponents()
        {
            foreach (var itemHolder in itemHolders)
            {
                itemHolder.Consume();
            }
        }
    }
}
