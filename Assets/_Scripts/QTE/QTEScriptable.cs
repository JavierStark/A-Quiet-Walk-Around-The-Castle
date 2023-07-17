using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.QTE;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ScriptableObjects/QTEScriptable")]
public class QTEScriptable : ScriptableObject
{
    public List<QTEAction> actions;
}
