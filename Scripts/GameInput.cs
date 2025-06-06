using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameInput : MonoBehaviour {
    public static GameInput Instance  {get; private set;}
    private PlayerInputActions _playerInputActions;
    public event EventHandler OnPlayerAttack;

    private void Awake() {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Player.Attack.started += PlayerAttackStarted; 
    }
    public Vector2 GetMovementVector() {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector3 GetMousePosition() {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        return mousePosition;
    }

    public void DisableMovement() {
        _playerInputActions.Disable();
    }
    private void PlayerAttackStarted(InputAction.CallbackContext obj) {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty); // ? == if != null
    }
}

