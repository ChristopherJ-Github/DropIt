using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform toFollow;

	void Start ()
    {
        currentPosition = toFollow.position;
	}
    
    public float positionSpeed;
    public float rotationSpeed;

    void Update ()
    {
        FollowPosition(positionSpeed);
        FollowRotation(positionSpeed);
	}

    private Vector3 currentPosition;

    void FollowPosition (float speed)
    {
        
    }

    void FollowRotation (float speed)
    {

    }
}
