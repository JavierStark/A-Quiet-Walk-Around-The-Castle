using System;
using UnityEngine;

namespace Lockpicking
{
    [RequireComponent(typeof(Animator))]
    public class Pin : MonoBehaviour
    {

        private Animator _animator;
        private Pipe _pipe;
        private static readonly int Triggered = Animator.StringToHash("Triggered");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Setup(Pipe pipe)
        {
            _pipe = pipe;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _animator.SetBool(Triggered, true);
            _pipe.PinTriggered();
        }
    }
}
