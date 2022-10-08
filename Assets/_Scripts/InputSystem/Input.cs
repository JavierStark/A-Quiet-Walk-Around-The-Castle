using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
	public class Input : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool drop;
		public bool lanternInteraction;
		public float inventoryScroll;
		public float inventoryShortcuts;
		public float lean;
		public float spiderMinigameMove;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;

		public bool cursorInputForLook = true;
		private PlayerInput _playerInput;

		private void Start()
		{
			_playerInput = GetComponent<PlayerInput>();
		}

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
        {
			InteractInput(value.isPressed);
        }
		public void OnDrop(InputValue value)
		{
			DropInput(value.isPressed);
		}

		public void OnLampInteraction(InputValue value)
		{
			LanternInteractionInput(value.isPressed);
		}

		public void OnInventoryScroll(InputValue value)
		{
			InventoryScrollInput(value.Get<float>());
		}

		public void OnInventoryShortcuts(InputValue value)
		{
			InventoryShortcutsInput(value.Get<float>());
		}

		public void OnLean(InputValue value)
		{
			LeanInput(value.Get<float>());
		}

		public void OnSpiderMinigameMove(InputValue value)
		{
			SpiderMinigameMoveInput(value.Get<float>());
		}

		


		private void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		private void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		private void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		private void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}
		
		private void DropInput(bool newDropState)
		{
			drop = newDropState;
		}

		private void LanternInteractionInput(bool newLanternInteractionState)
		{
			lanternInteraction = newLanternInteractionState;
		}

		private void InventoryScrollInput(float newInventoryScrollState)
		{
			inventoryScroll = newInventoryScrollState;
		}

		private void InventoryShortcutsInput(float newInventoryShortcutsInput)
		{
			inventoryShortcuts = newInventoryShortcutsInput;
		}

		private void LeanInput(float newLeanInput)
		{
			lean = newLeanInput;
		}
		
		private void SpiderMinigameMoveInput(float newSpiderMinigameMove)
		{
			spiderMinigameMove = newSpiderMinigameMove;
		}




		
		public void ChangeToPlayerActionMap() => ChangeActionMap("Player");
		public void ChangeToSpiderMinigameActionMap() => ChangeActionMap("SpiderMinigame");
		private void ChangeActionMap(string map)
		{
			if (_playerInput.currentActionMap.name == map) return; 
			_playerInput.SwitchCurrentActionMap(map);
		}


		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}


	}
	
}