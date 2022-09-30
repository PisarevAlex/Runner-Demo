using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCreator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject cloneDotPrefab;
    [SerializeField] private float dotsDistance;

    private List<GameObject> cloneDots = new();

    [Header("References")]
    [SerializeField] private DrawPanel drawPanel;
    [SerializeField] private Pointer pointer;
    [SerializeField] private Transform dotsParent;
    [SerializeField] private CrowdController crowdController;
    [SerializeField] List<Vector2> coordinates;

    private float lastActivetedLength;

    public bool IsShapeCompleted()
    {
        if (crowdController.Clones.Count <= cloneDots.Count) return true;
        else return false;
    }

    public void CheckDot(float lineLength)
    {
        if ((lineLength - lastActivetedLength > dotsDistance) || cloneDots.Count == 0)
        {
            GameObject cloneDot = Instantiate(cloneDotPrefab);
            cloneDots.Add(cloneDot);

            RectTransform rect = cloneDot.GetComponent<RectTransform>();
            rect.SetParent(dotsParent, false);
            rect.localPosition = pointer.transform.localPosition;

            Vector2 proportion = drawPanel.GetProportionPosition(Camera.main.WorldToScreenPoint(rect.transform.position));
            proportion.x = Mathf.Round(proportion.x * 100.0f) * 0.01f;
            proportion.y = Mathf.Round(proportion.y * 100.0f) * 0.01f;
            coordinates.Add(proportion);

            lastActivetedLength = lineLength;

            if (crowdController.Clones.Count <= cloneDots.Count)
            {
                crowdController.RefreshTargets(coordinates);
                pointer.onTouchEnd();
            }
        }
    }

    public void DestroyAllDots()
    {
        lastActivetedLength = 0f;
        coordinates.Clear();

        foreach (GameObject dot in cloneDots)
        {
            Destroy(dot.gameObject);
        }
        cloneDots.Clear();
    }
}
