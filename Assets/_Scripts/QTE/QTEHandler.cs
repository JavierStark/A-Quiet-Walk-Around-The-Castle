using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEHandler : MonoBehaviour
{    
    [SerializeField] private InputActionMap qteActionMap;

    private PlayerInput _playerInput;
    
    //UI
    [SerializeField] private GameObject keyDisplay;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private Animator keyAnimator;
    
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        keyDisplay.SetActive(false);
    }

    private void OnEnable()
    {
        qteActionMap.Enable();
    }
    
    private void OnDisable()
    {
        qteActionMap.Disable();
    }

    public void StartQTE(QTEScriptable qte)
    {
        StartQTE(qte, () => { }, () => { });
    }

    public void StartQTE(QTEScriptable qte, Action onSuccess, Action onFail)
    {
        StartCoroutine(QTECoroutine(qte, onSuccess, onFail));
    }
    IEnumerator QTECoroutine(QTEScriptable qte, Action onSuccess, Action onFail)
    {
        _playerInput.DeactivateInput();
        keyDisplay.SetActive(true);

        bool success = true;

        foreach (var action in qte.actions)
        {
            var currentActionObject = qteActionMap[action.name];
            
            SetBindingText(currentActionObject);
            
            Debug.Log(action.name);
            for (int count = 0; count < action.amount; count++)
            {
                Debug.Log(count);
                yield return new WaitUntil(qteActionMap[action.name].WasPressedThisFrame);
                keyAnimator.SetTrigger("PressKey");
                yield return new WaitUntil(qteActionMap[action.name].WasReleasedThisFrame);
            }
        }

        _playerInput.ActivateInput();
        keyDisplay.SetActive(false);
        
        if (success) onSuccess();
        else onFail();
    }

    private void SetBindingText(InputAction action)
    {
        int bindingIndex = action.GetBindingIndex();
        string displayString = action.GetBindingDisplayString(bindingIndex, out string deviceLayoutName, out string controlPath);

        keyText.text = displayString;
    }
}
