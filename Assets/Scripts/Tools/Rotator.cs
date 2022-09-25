using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private float bounceAmplitude;

    // Start is called before the first frame update
    private void Start()
    {
        transform.localEulerAngles= new Vector3(0, Random.Range(0, 180), 0);
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
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}
