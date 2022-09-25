using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ButtonMotion : MonoBehaviour
{
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float scaleStrength;

    private Vector3 defaultScale;
    private RectTransform rect;

    // Start is called before the first frame update
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        defaultScale = rect.localScale;
    }

    private void Update()
    {
        SinScale(scaleSpeed, defaultScale, scaleStrength);
    }

    private void SinScale(float speed, Vector3 scale, float strength)
    {
        rect.localScale = scale + Mathf.Sin(Time.time * speed) * new Vector3(strength,strength,strength);
    }
}
