using UnityEngine;
using System.Collections;

public class PlayerDamager : Damager
{
    void OnCollisionEnter(Collision collision)
    {
        string _tag = collision.collider.tag;
        Vector3 collisionPoint = collision.contacts[0].point;
        // if it's a character it'll have a HealthManager that'll do some stuff and then delete the projectile
        if (_tag != "Character")
        {
            Destroy(gameObject);
        }
    }
}
