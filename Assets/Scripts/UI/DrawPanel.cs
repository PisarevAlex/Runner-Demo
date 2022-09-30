using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DrawPanel : MonoBehaviour
{
    [SerializeField, Min(0)] private float offset;
    private RectTransform rect;
    [SerializeField, ReadOnly] private Vector2 bordersX;
    [SerializeField, ReadOnly] private Vector2 bordersY;
    [SerializeField, ReadOnly] private Vector2 touchCoordinates;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        SetBorders();
    }

    private void Update()
    {
        SetBorders();
    }

    private void SetBorders()
    {
        Vector3[] worldCorners = new Vector3[4];
        rect.GetWorldCorners(worldCorners);

        Vector3[] screenCorners = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            screenCorners[i] = Camera.main.WorldToScreenPoint(worldCorners[i]);
        }

        bordersX.x = screenCorners[1].x + offset;
        bordersX.y = screenCorners[3].x - offset;
        bordersY.x = screenCorners[3].y + offset;
        bordersY.y = screenCorners[1].y - offset;
    }

    public Vector2 ClampInBorders(Vector2 screenPosition)
    {
        screenPosition.x = Mathf.Clamp(screenPosition.x, bordersX.x, bordersX.y);
        screenPosition.y = Mathf.Clamp(screenPosition.y, bordersY.x, bordersY.y);
        return screenPosition;
    }

    public Vector2 GetProportionPosition(Vector2 screenPosition)
    {
        float proportionX = Mathf.InverseLerp(bordersX.x, bordersX.y, screenPosition.x);
        float proportionY = Mathf.InverseLerp(bordersY.x, bordersY.y, screenPosition.y);
        return new Vector2(proportionX, proportionY);
    }

    public List<Vector2> CreateCoordinatesArray(List<GameObject> gameObjects)
    {
        List<Vector2> coordinates = new();
        foreach (GameObject obj in gameObjects)
        {
            float proportionX = Mathf.InverseLerp(bordersX.x,bordersX.y,obj.GetComponent<RectTransform>().localPosition.x);
            float proportionY = Mathf.InverseLerp(bordersY.x, bordersY.y, obj.GetComponent<RectTransform>().localPosition.y);
            Vector2 coordinate = new Vector2(proportionX,proportionY);

            coordinates.Add(coordinate);
        }

        return coordinates;
    }
}
