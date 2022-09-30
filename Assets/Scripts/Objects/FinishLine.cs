using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject finishFXPrefab;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out CloneController clone))
        {
            if (!clone.IsInCrowd) return;

            FindObjectOfType<CrowdController>().Finish();

            GameObject fx = GameObject.Instantiate(finishFXPrefab);
            fx.transform.position = collider.transform.position;
            FindObjectOfType<LevelManager>().Finish();
        }
    }
}
