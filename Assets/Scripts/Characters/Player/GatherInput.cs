using UnityEngine;
using UnityEngine.InputSystem;
public class GatherInput : MonoBehaviour
{
	private Controls _myControls;
    public float valueX;
    public bool jumpInput;
    public bool tryAttack;
    public float valueY;
    public bool tryToClimb;
    public bool tryToWallSlide;
    public bool tryToCrouch;
    public bool nextDialogue;
    private void Awake() {
        _myControls = new Controls();
    }

    private void OnEnable() {
        _myControls.Player.Move.performed += StartMove;
        _myControls.Player.Move.canceled += StopMove;

        _myControls.Player.Jump.performed += JumpStart;
        _myControls.Player.Jump.canceled += JumpStop;

        _myControls.Player.Attack.performed += TryToAttack;
        _myControls.Player.Attack.canceled += StopTryToAttack;

        _myControls.Player.Climb.performed += ClimbStart;
        _myControls.Player.Climb.canceled += ClimbStop;
        
        _myControls.Player.Crouch.performed += TryCrouch;
        _myControls.Player.Crouch.canceled += StopCrouch;

        _myControls.Player.ContinueDialogue.performed += NextDialoguePress;
        _myControls.Player.ContinueDialogue.canceled += NextDialogueRelease;

        _myControls.Player.Enable();
    }

    private void OnDisable() {
        _myControls.Player.Move.performed -= StartMove;
        _myControls.Player.Move.canceled -= StopMove;

        _myControls.Player.Jump.performed -= JumpStart;
        _myControls.Player.Jump.canceled -= JumpStop;

        _myControls.Player.Attack.performed -= TryToAttack;
        _myControls.Player.Attack.canceled -= StopTryToAttack;

        _myControls.Player.Climb.performed -= ClimbStart;
        _myControls.Player.Climb.canceled -= ClimbStop;

        _myControls.Player.Crouch.performed -= TryCrouch;
        _myControls.Player.Crouch.canceled -= StopCrouch;

        _myControls.Player.ContinueDialogue.performed -= NextDialoguePress;
        _myControls.Player.ContinueDialogue.canceled -= NextDialogueRelease;

        _myControls.Player.Disable();
    }

    public void DisableControls() {
        _myControls.Player.Move.performed -= StartMove;
        _myControls.Player.Move.canceled -= StopMove;

        _myControls.Player.Jump.performed -= JumpStart;
        _myControls.Player.Jump.canceled -= JumpStop;

        _myControls.Player.Attack.performed -= TryToAttack;
        _myControls.Player.Attack.canceled -= StopTryToAttack;

        _myControls.Player.Climb.performed -= ClimbStart;
        _myControls.Player.Climb.canceled -= ClimbStop;

        _myControls.Player.Crouch.performed -= TryCrouch;
        _myControls.Player.Crouch.canceled -= StopCrouch;

        _myControls.Player.Disable();
        valueX = 0.0f;
    }

    private void StartMove(InputAction.CallbackContext ctx) {
        valueX = ctx.ReadValue<float>();
        if (valueX > 0.1f) 
        {
            valueX = 1.0f;
            tryToWallSlide = true;
        }
        else if (valueX < 0.0f && valueX >= -1.0f) {
            valueX = -1.0f;
            tryToWallSlide = true;
        }
        else {
            valueX = 0.0f;
            tryToWallSlide = false;
        }
    }

    private void StopMove(InputAction.CallbackContext ctx) {
        valueX = 0.0f;
        tryToWallSlide = false;
    }

    private void JumpStart(InputAction.CallbackContext ctx) {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx) {
        jumpInput = false;
    }

    private void TryToAttack(InputAction.CallbackContext ctx) {
        tryAttack = true;
    }

    private void StopTryToAttack(InputAction.CallbackContext ctx) {
        tryAttack = false;
    }

    private void ClimbStart(InputAction.CallbackContext ctx) {
        valueY = Mathf.RoundToInt(ctx.ReadValue<float>());

        if (Mathf.Abs(valueY) > 0) {
            tryToClimb = true;
        }
    }

    private void ClimbStop(InputAction.CallbackContext ctx) {
        tryToClimb = false;
        valueY = 0;
    }

    private void TryCrouch(InputAction.CallbackContext ctx) {
        tryToCrouch = true;
    }

    private void StopCrouch(InputAction.CallbackContext ctx) {
        tryToCrouch = false;
    }

    private void NextDialoguePress(InputAction.CallbackContext ctx) {
        nextDialogue = true;
    }

    private void NextDialogueRelease(InputAction.CallbackContext ctx) {
        nextDialogue = false;
    }
}
