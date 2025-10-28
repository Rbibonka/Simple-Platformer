using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private float smoothing = 0.1f;

    private Vector3 offset;
    private Transform target;
    private float fixedZ;

    private bool isFollow;

    public void Initialize(Transform target)
    {
        this.target = target;
        fixedZ = transform.position.z;
    }

    public void StartFollow()
    {
        isFollow = true;
    }

    public void StopFollow()
    {
        isFollow = false;
    }

    private void LateUpdate()
    {
        if (!isFollow)
        {
            return;
        }

        Vector3 targetPosition = target.position + offset;
        float step = smoothing * Time.deltaTime;

        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, step);
        newPosition.z = fixedZ;

        transform.position = newPosition;
    }

    public void CameraToTargetImmediately()
    {
        transform.position = target.position;
        var newPosition = transform.position;
        newPosition.z = fixedZ;

        transform.position = newPosition;
    }
}