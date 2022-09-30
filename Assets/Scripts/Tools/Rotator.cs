using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector = Vector3.up;
    public float rotationSpeed;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private float bounceAmplitude;

    // Start is called before the first frame update
    private void Start()
    {
        transform.Rotate(rotationVector, Random.Range(0,360), Space.Self);
    }

    private void Update()
    {
        SinBounce(bounceSpeed, bounceAmplitude);
        RotateObj(rotationSpeed);
    }

    private void SinBounce(float speed, float amplitude)
    {
        float x = transform.localPosition.x;
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        float z = transform.localPosition.z;

        transform.localPosition = new Vector3(x, y, z);
    }

    private void RotateObj(float speed)
    {
        transform.Rotate(rotationVector, Time.deltaTime * speed, Space.Self);
    }
}
