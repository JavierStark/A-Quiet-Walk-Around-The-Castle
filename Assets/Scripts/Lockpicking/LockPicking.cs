using System;
using System.Diagnostics.CodeAnalysis;
using Environment;
using Interactables;
using Lockpicking;
using LockPicking;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = InputSystem.Input;

namespace LockPicking
{
    public class LockPicking : MonoBehaviour
    {
        //[SerializeField] private Lock lockObject;
        [SerializeField] private SpiderMinigame _spiderMinigame;
        [SerializeField] private bool changeStateEDITOR;
        private bool _active;
        private Input _input;
        private Door _door;

        private void Start()
        {
            _input = GetComponent<Input>();
        }

        private void Update()
        {
            // if (changeStateEDITOR)
            // {
            //     if(_active) Deactivate();
            //     else Activate();
            // }
            // changeStateEDITOR = false;

            
            
            
            // if (_active)
            // {
            //     RotateLockPick();
            //     OpenLock();
            // }
        }

        public void Activate(float difficulty, Door door)
        {
            _spiderMinigame.StartMinigame();

            // if (_active) return;
            // _door = door;
            // _active = true;
            // _input.ChangeToLockPickingActionMap();
            // lockObject.gameObject.SetActive(true);
            // lockObject.Setup(this, difficulty);
        }
        // public void Deactivate()
        // {
        //     if (!_active) return;
        //     _door = null;
        //     _active = false;
        //     _input.ChangeToPlayerActionMap();
        //     lockObject.gameObject.SetActive(false);
        // }
        //
        // public void OpenDoor()
        // {
        //     _door.ChangeState();
        //     Deactivate();
        // }
        //
        // private void RotateLockPick()
        // {
        //     lockObject.RotateLockPick(_input.rotateLockPick);
        // }
        //
        // private void OpenLock()
        // {
        //     lockObject.OpenLock(_input.openLock);
        // }
    }
}
