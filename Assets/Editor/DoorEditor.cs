using _Scripts.Environment;
using Interactables;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(Door))]
    public class DoorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            var door = target as Door;
            if (door == null) return;
                        
            switch (door.doorType)
            {
                case DoorType.Key:
                {
                    var idForKey = serializedObject.FindProperty("idForKey");
                    EditorGUILayout.PropertyField(idForKey);
                    break;
                }
                case DoorType.Trigger:
                {
                    var trigger = serializedObject.FindProperty("trigger");
                    EditorGUILayout.PropertyField(trigger);
                    break;
                }
                case DoorType.LockPick:
                {
                    var lockPickingDifficulty = serializedObject.FindProperty("lockPickingDifficulty");
                    EditorGUILayout.PropertyField(lockPickingDifficulty);
                    break;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}