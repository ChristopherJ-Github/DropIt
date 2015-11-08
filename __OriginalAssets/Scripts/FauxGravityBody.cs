using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public bool attract = true;

	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.useGravity = false;
    }
	
	void FixedUpdate ()
    {
        FauxGravityAttractor.instance.Attract(transform, _rigidbody, attract);
	}
}
