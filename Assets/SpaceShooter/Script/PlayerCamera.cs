using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target_object;
    public float follow_tightness;
    Vector3 wanted_position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void FixedUpdate()
    {
        wanted_position = target_object.position;
        wanted_position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, wanted_position, Time.deltaTime * follow_tightness);
    }
}
