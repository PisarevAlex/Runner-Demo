using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Pointer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform raycastPlanePivot;
    [SerializeField] private GameObject pointerTrail;
    [SerializeField] private DrawPanel drawPanel;
    [SerializeField] private ShapeCreator shapeCreator;

    [field: Header("Info")]
    [field: SerializeField, ReadOnly] public bool IsTouching { get; private set; }
    [field: SerializeField, ReadOnly] public float SwipeLength { get; private set; }
    private Vector2 lastPointerPosition;

    private InputHandler inputHandler;
    private RectTransform rect;

    private void OnEnable()
    {
        lastPointerPosition = Vector2.zero;
        inputHandler = FindObjectOfType<InputHandler>();
        inputHandler.onTouchPerformed += onTouchStart;
        inputHandler.onTouchCanceled += onTouchEnd;
        rect = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        inputHandler.onTouchPerformed -= onTouchStart;
        inputHandler.onTouchCanceled -= onTouchEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTouching) return;

        Vector2 clampedPosition = drawPanel.ClampInBorders(inputHandler.pointerPosition);

        if (lastPointerPosition != Vector2.zero)
        {
            SwipeLength += Vector2.Distance(lastPointerPosition,clampedPosition);
        }

        lastPointerPosition = clampedPosition;

        SetPosition(clampedPosition);
        shapeCreator.CheckDot(SwipeLength);
    }

    private void SetPosition(Vector2 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        Plane hPlane = new Plane(raycastPlanePivot.forward, raycastPlanePivot.position);
        float distance = 0;
        hPlane.Raycast(ray, out distance);
        transform.position = ray.GetPoint(distance);
    }

    public void onTouchStart()
    {
        SwipeLength = 0f;

        shapeCreator.DestroyAllDots();
        IsTouching = true;
        pointerTrail.SetActive(true);
    }

    public void onTouchEnd()
    {
        lastPointerPosition = Vector2.zero;
        if (!shapeCreator.IsShapeCompleted())
        {
            shapeCreator.DestroyAllDots();
        }
        IsTouching = false;
        pointerTrail.SetActive(false);
    }
}