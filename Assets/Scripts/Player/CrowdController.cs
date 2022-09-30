using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour, IControllable
{
    [field: Header("Parameters")]
    [field: SerializeField] public bool IsRunning { get; private set; }

    [field: Header("Animation Parameters")]
    [field: SerializeField, Range(0,0.5f)] public float SpawnAnimationSpeed { get; private set; }
    [field: SerializeField, Range(1,2)] public float ExtraScalePower { get; private set; }

    [Header("Prefabs")]
    public GameObject targetPrefab;

    [Header("References")]
    [SerializeField] private BoxCollider crowdAreaBox;

    [field: SerializeField] public List<CloneController> Clones { get; private set; }
    public List<Transform> targets;

    private void OnEnable()
    {
        crowdAreaBox.enabled = false;
    }

    private void Update()
    {
        if (IsRunning) SetMiddlePosition();
    }

    public void RefreshTargets(List<Vector2> coordinates)
    {
        foreach (Transform target in targets)
        {
            Destroy(target.gameObject);
        }
        targets.Clear();


        float minX = -crowdAreaBox.size.x / 2;
        float maxX = crowdAreaBox.size.x / 2;
        float minY = -crowdAreaBox.size.y / 2;
        float maxY = crowdAreaBox.size.y / 2;

        foreach (Vector2 coordinate in coordinates)
        {
            var targetInstance = Instantiate(targetPrefab);
            targetInstance.transform.SetParent(transform);
            targets.Add(targetInstance.transform);

            float targetX = Mathf.Lerp(minX,maxX, coordinate.x);
            float targetY = Mathf.Lerp(minY, maxY, coordinate.y);

            targetInstance.transform.localPosition = new Vector3(targetX,0,targetY);
        }

        for (int i = 0; i < Clones.Count; i++)
        {
            Clones[i].PlaySpawnAnimation();
            Clones[i].myTarget = targets[i];
            Clones[i].transform.position = targets[i].position;
        }
    }

    public void Run()
    {
        if (IsRunning) return;

        IsRunning = true;
    }

    public void Stop()
    {
     
    }

    public void SetMiddlePosition()
    {
        float positionZ = 0;
        foreach (CloneController cloneController in Clones)
        {
            if (cloneController == null) continue;
            positionZ += cloneController.transform.position.z;
        }

        positionZ = positionZ / Clones.Count;

        transform.position = new Vector3(transform.position.x, transform.position.y, positionZ);
    }

    public void AddCloneInCrowd(CloneController cloneController)
    {
        cloneController.enabled = true;
        cloneController.AddMeInCrowd(this);
        Clones.Add(cloneController);
    }

    public void RemoveClone(CloneController cloneController)
    {
        if (Clones.Contains(cloneController))
        {
            Clones.Remove(cloneController);
        }
    }

    public void Finish()
    {
        IsRunning = false;
        foreach (CloneController cloneController in Clones)
        {

        }
    }

    public void Turn(Vector2 moveDirection)
    {
 
    }
}
