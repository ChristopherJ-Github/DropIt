using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAttractor attractor;
    private Rigidbody _rigidbody;

	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.useGravity = false;
    }
	
	void FixedUpdate ()
    {
        attractor.Attract(transform, _rigidbody);
	}
}
