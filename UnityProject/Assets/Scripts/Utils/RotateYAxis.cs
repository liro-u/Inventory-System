using UnityEngine;

public class RotateYAxis : MonoBehaviour
{
    public float rotationSpeed = 10f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate around the y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
