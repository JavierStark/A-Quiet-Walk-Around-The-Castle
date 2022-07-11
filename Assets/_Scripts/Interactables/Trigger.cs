using System;
using Interfaces;
using UnityEngine;

namespace Interactables
{
    public class Trigger : MonoBehaviour, IInteractable
    {
    
        public delegate void ActivateTrigger();
        public event ActivateTrigger OnActivateTrigger;

        private Animator _animator;
        private bool _active = false;
        private static readonly int Up = Animator.StringToHash("Up");
        private static readonly int Down = Animator.StringToHash("Down");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Interact(GameObject playerWhoInteract, ItemScriptable itemInHand)
        {            
            _animator.SetTrigger(_active? Up:Down);
            _active = !_active;
            if (OnActivateTrigger != null)
            {
                OnActivateTrigger();
            }
        }
    }
}
