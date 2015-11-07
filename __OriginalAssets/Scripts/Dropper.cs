using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour
{
    private GameObject dropable;
    private ScoreManager scoreManager;

	void Start ()
    {
        dropable = PlayerManager.instance.dropable.prefab;
        scoreManager = GetComponent<ScoreManager>();
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
        DropableCollision dropableCollision = dropable.GetComponent<DropableCollision>();
        dropableCollision.scoreManager = scoreManager;
        dropable.transform.position = transform.position;
    }
}
