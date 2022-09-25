using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject finishFXPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IControllable>(out IControllable controllable))
        {
            GameObject fx = GameObject.Instantiate(finishFXPrefab);
            fx.transform.position = other.transform.position;

            FindObjectOfType<LevelManager>().Finish();
        }
    }
}
