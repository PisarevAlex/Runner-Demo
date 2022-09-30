using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    [SerializeField] private HeroParameters parameters;

    [Header("Parameters")]
    public Transform myTarget;
    [field: SerializeField, ReadOnly] public bool IsInCrowd { get; private set; }
    [field: SerializeField, ReadOnly] public bool IsFinished { get; private set; }

    [Header("Materials")]
    [SerializeField] private Material crowdMaterial;

    [Header("References")]
    [SerializeField] private CrowdController crowd;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rb;

    private Vector3 defaultScale;

    private void OnEnable()
    {
        defaultScale = transform.localScale;
        animator.speed = Random.Range(0.85f, 1.15f);
        if (TryGetComponent(out Outman outman))
        {

        }
        else
        {
            IsInCrowd = true;
        }
    }

    void Update()
    {
        if (crowd.IsRunning)
        {
            animator.SetBool(Constants.animatorBool_isRunning, true);
            rb.AddForce(Vector3.forward * parameters.acceleration, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, parameters.maxRunSpeed);
            model.forward = Vector3.forward;

            float speedRatio = Mathf.InverseLerp(0, parameters.maxRunSpeed, rb.velocity.magnitude);
            animator.speed = speedRatio;
        }
        else
        {
            rb.velocity -= parameters.deceleration * rb.velocity;
            animator.SetBool(Constants.animatorBool_isRunning, false);
        }
    }

    public void PlaySpawnAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnAnimationRoutine());
    }

    private IEnumerator SpawnAnimationRoutine()
    {
        float animationTime = Random.Range(crowd.SpawnAnimationSpeed - 0.2f, crowd.SpawnAnimationSpeed + 0.2f);
        Vector3 extraScale = defaultScale * crowd.ExtraScalePower;

        for (float i = 0; i < animationTime; i += Time.deltaTime)
        {
            float progress = Mathf.InverseLerp(0, animationTime, i);

            transform.localScale = Vector3.Lerp(Vector3.zero, extraScale, progress);
            yield return null;
        }

        transform.localScale = extraScale;

        if (extraScale != defaultScale)
        {
            for (float i = 0; i < animationTime / 2; i += Time.deltaTime)
            {
                float progress = Mathf.InverseLerp(0, animationTime / 2, i);

                transform.localScale = Vector3.Lerp(extraScale, defaultScale, progress);
                yield return null;
            }
        }

        transform.localScale = defaultScale;
    }

    public void AddMeInCrowd(CrowdController crowdController)
    {
        crowd = crowdController;
        meshRenderer.material = crowdMaterial;
        IsInCrowd = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Obstacle obstacle))
        {
            if (crowd != null) crowd.RemoveClone(this);
            KillMe();
        }
    }

    public void KillMe()
    {
        Destroy(gameObject);
    }
}
