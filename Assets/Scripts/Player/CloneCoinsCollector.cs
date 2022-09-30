using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCoinsCollector : MonoBehaviour
{
    private CrowdCoinsCollector crowdCoinsCollector;

    private void OnEnable()
    {
        crowdCoinsCollector = FindObjectOfType<CrowdCoinsCollector>();
    }

    private void OnTriggerEnter(Collider targetCollider)
    {
        if (targetCollider.TryGetComponent(out Coin coin))
        {
            crowdCoinsCollector.AddPoints(coin.Price);
            coin.Take();
        }
    }
}
