using UnityEngine;
using System.Collections;

public class DropTarget : MonoBehaviour
{
    new Collider collider;
	void Start ()
    {
        tag = "Drop Target";
        collider = GetComponent<Collider>();
        GameManager.instance.goalScore++;
	}

    public void TurnOff ()
    {
        //some animation for the target being hit
        collider.enabled = false;
    }
}
