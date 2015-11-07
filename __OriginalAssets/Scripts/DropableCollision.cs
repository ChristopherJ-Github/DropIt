using UnityEngine;
using System.Collections;

public class DropableCollision : MonoBehaviour
{
    [HideInInspector] public ScoreManager scoreManager;

    //depending on how planets are set up this may 
    //need to be changed into a collision only system
    //with just layers instead of tags
	void OnTriggerEnter (Collider otherCollider)
    {
        if (otherCollider.tag == "Drop Target")
        {
            scoreManager.score++;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            Destroy(gameObject);
        }
    }
}
