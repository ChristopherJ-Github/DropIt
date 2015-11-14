using UnityEngine;
using System.Collections;

public class WitchProjectile : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(DestructionTimer());
	}

    public float destroyTime;

    IEnumerator DestructionTimer ()
    {
        yield return new WaitForSeconds(destroyTime);
        DestroySelf();
    }
	
	void DestroySelf ()
    {
        Destroy(gameObject);
    }
}
