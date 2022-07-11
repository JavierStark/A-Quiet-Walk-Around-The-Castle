using System;
using System.Collections;
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
        [SerializeField] private SpiderMinigame spiderMinigame;
        [SerializeField] private bool changeStateEditor;
        [SerializeField] private float waitBetweenMinigameAndOpen = 2;
        private bool _active;
        private Input _input;
        private Door _door;

        private void Start()
        {
            _input = GetComponent<Input>();
        }

        public void Activate(int difficulty, Door door)
        {
            if (_active) return;
            _door = door;
            _active = true;
            _input.ChangeToSpiderMinigameActionMap();

            spiderMinigame.StartMinigame(difficulty, _door,  this);
        }

        public void Deactivate()
        {
            if (!_active) return;
            _door = null;
            _active = false;
            _input.ChangeToPlayerActionMap();
        }

        public void OpenDoor()
        {
            StartCoroutine(OpenDoorCoroutine());
        }

        private IEnumerator OpenDoorCoroutine()
        {
            yield return new WaitForSeconds(waitBetweenMinigameAndOpen);
            _door.ChangeState();
            Deactivate();
        }
    }
}