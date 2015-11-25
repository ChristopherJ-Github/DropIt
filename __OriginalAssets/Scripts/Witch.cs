using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour
{
    private Vector3 initLocalEuler;
    private Transform newTransform;
    private Transform planetTransform;
    public Transform pivotTransform;

	void Start ()
    {
        InvokeRepeating("Attack", 2, 2);
        initLocalEuler = pivotTransform.localEulerAngles;
        newTransform = transform;
        planetTransform = PlanetObject.instance.transform;
	}

    public Transform spawnPoint;
    public GameObject projectile;
    public float shootForce;
	
	void Attack ()
    {
        Vector3 position = spawnPoint.position;
        Vector3 direction = PlayerManager.instance.currentCharacter.transform.position - spawnPoint.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        GameObject projectile = Instantiate(this.projectile, position, lookRotation) as GameObject;
        Rigidbody _rigidbody = projectile.GetComponent<Rigidbody>();
        _rigidbody.AddForce(direction * shootForce);
    }

    public float rotateSpeed;
    private float currentYRot;

    void Update ()
    {
        pivotTransform.LookAt(PlayerManager.instance.currentCharacter.transform);
        Vector3 localEuler = pivotTransform.localEulerAngles;
        Vector3 newEuler = initLocalEuler;
        currentYRot = Mathf.MoveTowards(currentYRot, localEuler.y, rotateSpeed * Time.deltaTime);
        newEuler.y = currentYRot;
        //newEuler.y = localEuler.y;
        pivotTransform.localRotation = Quaternion.Euler(newEuler);
    }
}
