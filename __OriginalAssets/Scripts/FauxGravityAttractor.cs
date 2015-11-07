using UnityEngine;
using System.Collections;

public class FauxGravityAttractor : Singleton<FauxGravityAttractor>
{
    public float gravity;

    public void Attract(Transform body, Rigidbody _rigibody)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector4 bodyUp = body.up;
        _rigibody.AddForce(gravityUp * -gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
