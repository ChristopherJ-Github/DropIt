using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour
{
    private Rigidbody newRigiBody;
    public bool attract = true;
    public bool freezeRotation = true;

	void Start ()
    {
        newRigiBody = GetComponent<Rigidbody>();
        if (freezeRotation)
        {
            newRigiBody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {

        }
        newRigiBody.useGravity = false;
    }
	
	void FixedUpdate ()
    {
        FauxGravityAttractor.instance.Attract(transform, newRigiBody, attract);
	}
}
