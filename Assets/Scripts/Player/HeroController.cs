using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeroController : MonoBehaviour, IControllable
{
    [SerializeField] private HeroParameters parameters;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private MainCamera playerCamera;

    [Header("Info")]
    [ReadOnly, SerializeField] private bool isRunning;

    public void Run()
    {
        isRunning = true;
        animator.SetBool(Constants.animatorBool_isRunning, true);
    }

    public void Stop()
    {
        isRunning = false;
        animator.SetBool(Constants.animatorBool_isRunning, false);
    }

    public void Turn(Vector2 direction)
    {
        if (!isRunning) return;

        direction.y = 0;
        direction = Vector2.ClampMagnitude(direction,parameters.maxTurnSpeed);
        rb.AddForce(direction * parameters.turnForce, ForceMode.Impulse);
        float clampedVelocityX = rb.velocity.x;
        rb.velocity = new Vector3(clampedVelocityX, rb.velocity.y, rb.velocity.z);

        float tiltStrength = Mathf.InverseLerp(0, parameters.maxTurnSpeed, Mathf.Abs(clampedVelocityX)) * Mathf.Sign(clampedVelocityX);

        float modelTiltZ = tiltStrength * parameters.maxTiltAngle;
        float modelRotationY = tiltStrength * parameters.maxRotation;
        model.localEulerAngles = new Vector3(model.localEulerAngles.x, modelRotationY, - modelTiltZ);
    }

    void Update()
    {
        if (isRunning)
        {
            rb.AddForce(Vector3.forward * parameters.acceleration, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, parameters.maxRunSpeed);
        }
        else
        {
            rb.velocity -= parameters.deceleration * rb.velocity;
        }

        float speedRatio = Mathf.InverseLerp(0, parameters.maxRunSpeed, rb.velocity.magnitude);
        if (isRunning)
        {
            animator.speed = speedRatio;
        }
        else
        {
            animator.speed = 1;
        }
    }
}
