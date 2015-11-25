using UnityEngine;
using System.Collections;

public class Droppable : MonoBehaviour
{
    private Rigidbody newRigiBody;

    void Start()
    {
        newRigiBody = GetComponent<Rigidbody>();
        newRigiBody.useGravity = false;
        AddRandomTorque();
    }

    private float maxTorque = 1000;

    void AddRandomTorque ()
    {
        Vector3 torque = new Vector3();
        torque.x = Random.Range(-maxTorque, maxTorque);
        torque.y = Random.Range(-maxTorque, maxTorque);
        torque.z = Random.Range(-maxTorque, maxTorque);
        newRigiBody.AddTorque(torque);
    }

    void FixedUpdate()
    {
        PlanetObject.instance.Attract(transform, newRigiBody, true, false);
    }
}
