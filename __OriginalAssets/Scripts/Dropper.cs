using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour
{
    private GameObject dropable;
    private ScoreManager scoreManager;

    void Start()
    {
        dropable = PlayerManager.instance.dropable.prefab;
        scoreManager = GetComponent<ScoreManager>();
        UpdateDropSize();
    }

    private Vector3 size;

    void UpdateDropSize()
    {
        GameObject dropable = Instantiate(this.dropable) as GameObject;
        Bounds bounds = dropable.GetComponent<Collider>().bounds;
        size = bounds.extents;
        Destroy(dropable);
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
        DroppableCollision dropableCollision = dropable.GetComponent<DroppableCollision>();
        dropableCollision.scoreManager = scoreManager;
        dropable.transform.position = GetDropPosition();
    }

    public float error;

    Vector3 GetDropPosition ()
    {
        Vector3 shift = -transform.up * (size.y + PlayerManager.instance.size.y + error);
        Vector3 dropPosition = transform.position + shift;
        return dropPosition;
    }
}
