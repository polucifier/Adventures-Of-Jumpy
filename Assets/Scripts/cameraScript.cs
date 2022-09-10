using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
    }
}
