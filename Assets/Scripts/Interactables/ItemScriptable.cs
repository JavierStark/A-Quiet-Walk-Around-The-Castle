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
        [HideInInspector] public string keyId;
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

    [CustomEditor(typeof(ItemScriptable))]
    public class ItemScriptableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            var itemScriptable = target as ItemScriptable;
            if (itemScriptable != null)
            {
                switch (itemScriptable.type)
                {
                    case ItemType.Key:
                    {
                        itemScriptable.keyId = EditorGUILayout.TextField("keyId", itemScriptable.keyId);
                        break;
                    }
                    default: break;
                }
            }
        }
    }
}