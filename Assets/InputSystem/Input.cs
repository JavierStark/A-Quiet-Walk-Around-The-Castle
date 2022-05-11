using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class Input : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;
		public float inventoryScroll;
		public float inventoryShortcuts;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

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

		public void OnInventoryScroll(InputValue value)
		{
			InventoryScrollInput(value.Get<float>());
		}

		public void OnInventoryShortcuts(InputValue value)
		{
			InventoryShortcutsInput(value.Get<float>());
		}

        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void InteractInput(bool newPressedState)
		{
			interact = newPressedState;
		}

		private void InventoryScrollInput(float newInventoryScrollState)
		{
			inventoryScroll = newInventoryScrollState;
		}

		private void InventoryShortcutsInput(float newInventoryShortcutsInput)
		{
			inventoryShortcuts = newInventoryShortcutsInput;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}