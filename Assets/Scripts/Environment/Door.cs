using System;
using Interactables;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Trigger trigger;
        [SerializeField] private bool open = false;
        [SerializeField] private string idForKey;

        private Animator _animator;
        private static readonly int Close = Animator.StringToHash("Close");
        private static readonly int Open = Animator.StringToHash("Open");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            trigger.OnActivateTrigger += ChangeState;
            _animator.SetTrigger(open? Open:Close);   
            _animator.ResetTrigger(Open);
            _animator.ResetTrigger(Close);
        }

        private void ChangeState()
        {
            _animator.SetTrigger(open ? Close : Open);
            open = !open;
        }

        public void Interact(GameObject playerWhoInteract)
        {
            if (idForKey == null) return;
            
            ItemScriptable itemInHand = playerWhoInteract.GetComponent<Inventory>().GetItem();
            if (!itemInHand) return;
            
            if (itemInHand.type != ItemType.Key) return;
            if (idForKey == itemInHand.keyId)
            {
                ChangeState();
            }
        }
    }
}
