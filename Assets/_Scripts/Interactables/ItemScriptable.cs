using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Interactables
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptable")]
    public class ItemScriptable : ScriptableObject
    {
        public string objectName;
        public string description;
        public ItemType type;
        public string id;
        public Sprite icon;
        public GameObject objectGameObject;
    }

    public enum ItemType
    {
        Default,
        Key,
        LockPick,
        Throwable
    }
}