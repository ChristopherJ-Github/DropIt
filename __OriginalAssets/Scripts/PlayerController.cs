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
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _rigidBody.MovePosition(_rigidBody.position + transform.TransformDirection(moveDirection * moveSpeed * Time.deltaTime));
    }
}
