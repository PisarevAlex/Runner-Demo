using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject killFXPrefab;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out CloneController cloneController))
        {
            GameObject killFX = Instantiate(killFXPrefab);
            killFX.transform.position = cloneController.transform.position;

            FindObjectOfType<CrowdController>().RemoveClone(GetComponent<CloneController>());
        }
    }
}
