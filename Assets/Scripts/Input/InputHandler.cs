using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject controllableObj;

    private IControllable controllable;
    private InputActions inputActions;

    private void OnEnable()
    {
        controllable = controllableObj.GetComponent<IControllable>();
        if (controllable == null) Debug.LogError("Controllable Object Not Found");

        inputActions = new InputActions();
        inputActions.Player.Turn.performed += ctx => controllable.Turn(ctx.ReadValue<Vector2>());
        inputActions.Player.Run.performed += ctx => controllable.Run();
        inputActions.Player.Run.canceled += ctx => controllable.Stop();
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Turn.performed -= ctx => controllable.Turn(ctx.ReadValue<Vector2>());
        inputActions.Player.Run.performed -= ctx => controllable.Run();
        inputActions.Player.Run.canceled -= ctx => controllable.Stop();
        inputActions.Player.Disable();
    }

    //Public Methods. Activation & Deactivation
    public void DeactivateControls()
    {
        controllable.Stop();
        inputActions.Player.Disable();
    }

    public void ActivateControls()
    {
        inputActions.Player.Enable();
    }
}