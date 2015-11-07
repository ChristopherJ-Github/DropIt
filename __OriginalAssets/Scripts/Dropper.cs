using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour
{
    GameObject dropable;

	void Start ()
    {
        dropable = PlayerManager.instance.dropable.prefab;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Drop();
        }
	}

    void Drop ()
    {
        GameObject dropable = Instantiate(this.dropable) as GameObject;
        dropable.transform.position = transform.position;
    }
}
