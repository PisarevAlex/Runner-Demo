using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    [SerializeField] private CounterUI counterUI;
    [field: SerializeField] public int Points { get; private set; }

    private void Start()
    {
        counterUI.RefreshCounter(Points);
    }

    private void OnTriggerEnter(Collider targetCollider)
    {
        if (targetCollider.TryGetComponent(out Coin coin))
        {
            Points += coin.Price;
            coin.Take();
            counterUI.RefreshCounter(Points);
            Destroy(coin.gameObject);
        }
    }
}
