using UnityEngine;
using System.Collections;

public class PlanetObject : Singleton<PlanetObject>
{
    public float gravity;

    public void Attract (Transform _transform, Rigidbody _rigibody, bool attract = true, bool rotate = true)
    {
        Vector3 gravityUp = (_transform.position - transform.position).normalized;
        Vector4 bodyUp = _transform.up;
        if (attract)
        {
            _rigibody.AddForce(gravityUp * -gravity);
        }
        if (rotate)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * _transform.rotation;
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, 50 * Time.deltaTime);
        }  
    }
}
