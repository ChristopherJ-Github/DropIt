using UnityEngine;
using System.Collections;

public class WitchProjectile : MonoBehaviour
{
	void Start ()
    {
        RandomizeColor();
        StartCoroutine(DestructionTimer());
	}

    public Gradient colors;
    public ParticleSystem newParticleSystem;

    void RandomizeColor ()
    {
        Color color = colors.Evaluate(Random.value);
        newParticleSystem.startColor = color;
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
