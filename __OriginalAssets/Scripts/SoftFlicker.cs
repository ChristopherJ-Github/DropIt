using UnityEngine;

[RequireComponent(typeof(Light))]
public class SoftFlicker : MonoBehaviour
{
	public float minIntensity = 0.25f;
	public float maxIntensity = 0.5f;
	
	float random;
	
	void Start()
	{
	}
	
	void Update()
	{
		random = Random.Range(minIntensity, maxIntensity);

		GetComponent<Light>().intensity = random;
	}
}