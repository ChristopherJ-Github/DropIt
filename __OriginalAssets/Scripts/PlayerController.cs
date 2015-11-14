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
        moveDirection = (XZMovement + YMovement) * moveSpeed * Time.deltaTime;
        _rigidBody.MovePosition(_rigidBody.position + moveDirection);
        if (XZMovement != Vector3.zero)
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }

    public float flyHeight;
    public LayerMask mask;

    /// <summary>
    /// If rotated properly gets the movement required to 
    /// reach flying position.
    /// </summary>
    Vector3 GetYMovement ()
    {
        Vector3 currentPosition = transform.position;
        RaycastHit hit;
        //send a ray down towards the planet
        if (Physics.Raycast(currentPosition, -transform.up, out hit, Mathf.Infinity, mask))
        {
            //calculate flight position
            Vector3 directionFromHit = (currentPosition - hit.point).normalized;
            Vector3 goalPosition = (directionFromHit * flyHeight) + hit.point;
            //calculate the movement required to get to flight position
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
