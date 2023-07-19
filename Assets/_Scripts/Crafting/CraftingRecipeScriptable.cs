using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace _Scripts.Crafting
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CraftingRecipe")]
    public class CraftingRecipeScriptable:ScriptableObject
    {
        public List<ItemScriptable> components;
        public ItemScriptable product;
    }
}