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
        public DoorType doorType;
        //Trigger
        [HideInInspector] public Trigger trigger;
        //Key
        [HideInInspector] public string idForKey;
        //LockPick
        [HideInInspector] public int lockPickingDifficulty;

        private Animator _animator;
        [SerializeField]private Transform _cameraTransform;
        private static readonly int Close = Animator.StringToHash("Close");
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int OpenFail = Animator.StringToHash("OpenFail");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _cameraTransform = transform.parent.GetChild(0).GetChild(1);
        }

        private void Start()
        {
            _animator.SetTrigger(open? Open:Close);
            _animator.ResetTrigger(Open);
            _animator.ResetTrigger(Close);
            
            if(doorType == DoorType.Trigger) trigger.OnActivateTrigger += ChangeState;
        }

        
        public void ChangeState()
        {
            if (doorType is DoorType.Key or DoorType.LockPick) doorType = DoorType.Free;
            _animator.SetTrigger(open ? Close : Open);
            open = !open;
        }

        public void Interact(GameObject playerWhoInteract)
        {
            if (doorType == DoorType.Trigger) return;
            Debug.Log(doorType);
            switch (doorType)
            {
                case DoorType.Key:
                {
                    if (idForKey == null) break;

                    ItemScriptable itemInHand = playerWhoInteract.GetComponent<Inventory>().GetItem();
                    if (!itemInHand) break;
                    if (itemInHand.type != ItemType.Key) break;
                    
                    if (idForKey == itemInHand.keyId) ChangeState();

                    return;
                }

                case DoorType.LockPick:
                {
                    playerWhoInteract.GetComponent<LockPicking.LockPicking>().Activate(lockPickingDifficulty,this);
                    
                    return;
                }

                case DoorType.Free:
                {
                    ChangeState();
                    
                    return;
                }
            }
            
            _animator.SetTrigger(OpenFail);
        }

        public Transform GetCameraTransform()
        {
            return _cameraTransform;
        }
    }
    

    public enum DoorType
    {
        Free,
        Key,
        Trigger,
        LockPick
    }
    
   
}
