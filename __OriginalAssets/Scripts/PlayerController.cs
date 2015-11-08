using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;

    void Start ()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public float moveSpeed;
    private Vector3 moveDirection;

    void FixedUpdate ()
    {
        Move();
    }

    void Move ()
    {
        Vector3 XZMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        XZMovement = transform.TransformDirection(XZMovement);
        Vector3 YMovement = GetYMovement (); 
        moveDirection = (XZMovement) * moveSpeed * Time.deltaTime;
        _rigidBody.MovePosition(_rigidBody.position + moveDirection);
        _rigidBody.MovePosition(_rigidBody.position + YMovement);
    }

    public float flyHeight;

    Vector3 GetYMovement ()
    {
        Vector3 currentPosition = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(currentPosition, -transform.up, out hit))
        {
            Vector3 directionFromHit = (currentPosition - hit.point).normalized;
            Vector3 goalPosition = (directionFromHit * flyHeight) + hit.point;
            Vector3 movement = goalPosition - currentPosition;
            Debug.DrawLine(currentPosition, goalPosition, Color.red);
            return movement;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
