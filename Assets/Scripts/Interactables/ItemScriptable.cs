using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptable")]
public class ItemScriptable : ScriptableObject
{
    public string objectName;
    public string description;
    public Sprite icon;
    public GameObject objectGameObject;
}
