using UnityEngine;

public class Coin : MonoBehaviour
{
    [field: SerializeField, Min(1)] public int Price { get; private set; }
    [SerializeField] private GameObject collectVFXPrefab;

    public void Take()
    {
        GameObject vfx = Instantiate(collectVFXPrefab);
        vfx.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
