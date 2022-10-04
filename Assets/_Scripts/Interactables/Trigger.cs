using Interactables;
using Interfaces;
using UnityEngine;

namespace _Scripts.Interactables
{
    public class Trigger : MonoBehaviour, IInteractable
    {
    
        public delegate void ActivateTriggerDelegate();
        public event ActivateTriggerDelegate OnActivateTrigger;

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
