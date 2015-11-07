using UnityEngine;
using System.Collections;

public class DropTarget : MonoBehaviour
{
	void Start ()
    {
        tag = "Drop Target";
        GameManager.instance.goalScore++;
	}
}
