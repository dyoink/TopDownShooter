using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    Vector3 position;
    public float movement_resistance = 1f; //1 = no movement, 0.9 = some movement, 0.5 = more movement, etc, 0 = centered at origin, layer is now foreground
    private void FixedUpdate()
    {
        position = Camera.main.transform.position * movement_resistance;
        position.z = transform.position.z;
        transform.position = position;
    }
}
