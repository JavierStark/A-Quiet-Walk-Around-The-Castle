using System;
using Interactables;
using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Trigger trigger;
        private bool _open = false;

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
        }

        private void ChangeState()
        {
            _animator.SetTrigger(_open ? Close : Open);
            _open = !_open;
        }
    }
}
