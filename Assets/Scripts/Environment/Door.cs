using System;
using Interactables;
using Interfaces;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Environment
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour, IInteractable
    {
        public bool open = false;
        public bool needsKey = false;
        [HideInInspector] public Trigger trigger;
        [HideInInspector] public string idForKey;

        private Animator _animator;
        private static readonly int Close = Animator.StringToHash("Close");
        private static readonly int Open = Animator.StringToHash("Open");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if(!needsKey) trigger.OnActivateTrigger += ChangeState;
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
