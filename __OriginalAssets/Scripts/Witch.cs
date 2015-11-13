using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour
{
	void Start ()
    {
        InvokeRepeating("Attack", 2, 2);
	}

    public Transform spawnPoint;
    public GameObject projectile;
    public float shootForce;
	
	void Attack ()
    {
        Vector3 position = spawnPoint.position;
        Vector3 direction = PlayerManager.instance.currentCharacter.transform.position - spawnPoint.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject projectile = Instantiate(this.projectile, position, rotation) as GameObject;
        Rigidbody _rigidbody = projectile.GetComponent<Rigidbody>();
        _rigidbody.AddForce(direction * shootForce);
    }
}
