using _Scripts.Interactables;
using _Scripts.Interfaces;
using Interactables;
using Player;
using UnityEngine;

namespace _Scripts.Environment
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
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
        [SerializeField] private Transform cameraTransform;
        private static readonly int Close = Animator.StringToHash("Close");
        private static readonly int Open = Animator.StringToHash("Open");
        private static readonly int OpenFail = Animator.StringToHash("OpenFail");

        private AudioSource _audioSource;
        [SerializeField] private AudioClip openDoorClip;
        [SerializeField] private AudioClip closeDoorClip;
    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
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
            _audioSource.PlayOneShot(open? closeDoorClip : openDoorClip);
            open = !open;
        }

        public void ChangeState(bool wantToOpen)
        {
            if (wantToOpen == open) return;
            ChangeState();
        }

        public void Interact(GameObject playerWhoInteract, ItemScriptable itemInHand)
        {
            if (doorType == DoorType.Trigger) return;

            var playerInventory = playerWhoInteract.GetComponent<Inventory>();
            
            switch (doorType)
            {
                case DoorType.Key:
                {
                    if (idForKey == null) break;
                    if (!itemInHand) break;
                    if (itemInHand.type != ItemType.Key) break;
                    if (idForKey != itemInHand.id) break;
                    playerInventory.DeleteItem();
                    ChangeState();
                    return;
                }

                case DoorType.LockPick:
                {
                    if (!itemInHand) break;
                    if (itemInHand.type != ItemType.LockPick) break;
                    playerInventory.DeleteItem();
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
            return cameraTransform;
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
