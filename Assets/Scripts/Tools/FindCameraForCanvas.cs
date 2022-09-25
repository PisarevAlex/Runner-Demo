using UnityEngine;

public class FindCameraForCanvas : MonoBehaviour
{
    public Canvas canvas;

    private void OnEnable()
    {
        canvas.worldCamera = Camera.main;
    }
}
