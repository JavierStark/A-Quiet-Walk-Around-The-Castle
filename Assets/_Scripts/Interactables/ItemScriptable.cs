using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Interactables
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptable")]
    public class ItemScriptable : ScriptableObject, IComparable
    {
        public string objectName;
        public string description;
        public ItemType type;
        public string id;
        public Sprite icon;
        public GameObject objectGameObject;
        public int CompareTo(object obj)
        {
            string otherName = (obj as ItemScriptable)?.objectName;

            return String.Compare(
                objectName, otherName, 
                comparisonType: StringComparison.OrdinalIgnoreCase);
        }
    }

    public enum ItemType
    {
        Default,
        Key,
        LockPick,
        Throwable
    }
}