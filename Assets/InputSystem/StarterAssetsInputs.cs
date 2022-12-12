using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool shooted;
		public bool crouch;
		public bool unequiping;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
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
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		public void OnReload(InputValue value)
		{
			ReloadInput(value.isPressed);
		}
		public void OnCrouch(InputValue value)
        {
			CrouchInput(value.isPressed);
        }
		public void OnUnequip(InputValue value)
		{
			Unequipingweapon(value.isPressed);
		}
#endif
		public void Unequipingweapon(bool newstate)
		{
			equipweapon weap = FindObjectOfType<equipweapon>();
			if (weap.isequiped)
			{
				weap.unequipingweap();
			}
			else
				return;
		}
		public void CrouchInput(bool newstate)
        {
			crouch = newstate;
        }
		public void ShootInput(bool newshootstate)
		{
			shooted = newshootstate;
			shoot();
		}
		public void ReloadInput(bool reloarstate)
		{
			equipweapon weap = FindObjectOfType<equipweapon>();
			if (weap.canreload)
			{
				StartCoroutine(weap.Reload());
			}
			else
				return;

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
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
		private void shoot()
		{

			equipweapon weap = FindObjectOfType<equipweapon>();
			if (weap.weapon != null && weap.canshoot)
			{
				if (shooted)
					weap.launch(-1);
			}

		}
	}
	
}