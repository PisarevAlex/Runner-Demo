using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject controllableObj;

    private IControllable controllable;
    private InputActions inputActions;

    public Vector2 pointerPosition;

    public Action onTouchPerformed;
    public Action onTouchCanceled;

    private void OnEnable()
    {
        controllable = controllableObj.GetComponent<IControllable>();
        if (controllable == null) Debug.LogError("Controllable Object Not Found");

        inputActions = new InputActions();

        inputActions.Player.Turn.performed += ctx => controllable?.Turn(ctx.ReadValue<Vector2>());
        inputActions.Player.Touch.performed += ctx => TouchPerformed();
        inputActions.Player.Touch.canceled += ctx => TouchCanceled();
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Turn.performed -= ctx => controllable?.Turn(ctx.ReadValue<Vector2>());
        inputActions.Player.Touch.performed -= ctx => TouchPerformed();
        inputActions.Player.Touch.canceled -= ctx => TouchCanceled();
        inputActions.Disable();
    }

    private void Update()
    {
        pointerPosition = inputActions.UI.Pointer.ReadValue<Vector2>();
    }

    private void TouchPerformed()
    {
        onTouchPerformed?.Invoke();
        controllable?.Run();
    }

    private void TouchCanceled()
    {
        onTouchCanceled?.Invoke();
        controllable?.Stop();
    }

    //Public Methods. Activation & Deactivation
    public void DeactivatePlayerControls()
    {
        controllable?.Stop();
        inputActions.Player.Disable();
    }

    public void ActivatePlayerControls()
    {
        inputActions.Player.Enable();
    }
}