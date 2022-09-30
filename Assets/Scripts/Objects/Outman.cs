using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CloneController))]
public class Outman : MonoBehaviour
{
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private GameObject collectFX;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out CloneController cloneController))
        {
            if (!cloneController.IsInCrowd) return;

            FindObjectOfType<CrowdController>().AddCloneInCrowd(GetComponent<CloneController>());
            Destroy(triggerCollider);

            GameObject fx = Instantiate(collectFX);
            fx.transform.position = transform.position;

            Destroy(this);
        }
    }
}
