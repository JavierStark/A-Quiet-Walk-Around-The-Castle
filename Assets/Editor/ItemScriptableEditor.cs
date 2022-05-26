using Interactables;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(ItemScriptable))]
    public class ItemScriptableEditor : UnityEditor.Editor
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