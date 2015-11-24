using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform toFollow;
    private Transform newTransform;

	void Start ()
    {
        newTransform = transform;
	}
    
    public float positionSpeed;
    public float rotationSpeed;

    void FixedUpdate ()
    {
        FollowPosition(positionSpeed);
        FollowRotation(rotationSpeed);
	}

    public Vector3 positionOffset;

    void FollowPosition (float speed)
    {
        Vector3 targetPosition = toFollow.position + toFollow.TransformVector(positionOffset);
        Vector3 newPosition = Vector3.Slerp(newTransform.position, targetPosition, speed * Time.deltaTime);
        newTransform.position = newPosition;
    }

    public Transform planet;

    void FollowRotation (float speed)
    {
        Vector3 forward = (planet.position - newTransform.position).normalized;
        Vector3 up = toFollow.forward;
        Quaternion targetRotation = Quaternion.LookRotation(forward, up);
        Quaternion newRotation = Quaternion.Slerp(newTransform.rotation, targetRotation, speed * Time.deltaTime);
        newTransform.rotation = newRotation;
    }
}
