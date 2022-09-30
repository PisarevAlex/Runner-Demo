using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdCoinsCollector : MonoBehaviour
{
    [SerializeField] private CounterUI counterUI;
    [field: SerializeField] public int Points { get; private set; }

    private void Start()
    {
        counterUI.RefreshCounter(Points);
    }

    public void AddPoints(int increase)
    {
        Points += increase;
        counterUI.RefreshCounter(Points);
    }
}
