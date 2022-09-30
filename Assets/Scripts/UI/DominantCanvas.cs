using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class DominantCanvas : MonoBehaviour
{
    private List<Canvas> hiddenCanvases = new List<Canvas>();
    private Canvas myCanvas;

    private void OnEnable()
    {
        hiddenCanvases.Clear();
        myCanvas = GetComponent<Canvas>();

        var allCanvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            if ((canvas.enabled) && (canvas != myCanvas))
            {
                hiddenCanvases.Add(canvas);
                canvas.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        ReturnHiddenCanvases();
    }

    private void OnDestroy()
    {
        //ReturnHiddenCanvases();
    }

    private void ReturnHiddenCanvases()
    {
        foreach (Canvas canvas in hiddenCanvases)
        {
            canvas.enabled = true;
        }

        hiddenCanvases.Clear();
    }
}
